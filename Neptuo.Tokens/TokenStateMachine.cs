using Neptuo.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Tokens
{
    internal class TokenStateMachine : StringStateMachine<TokenState>
    {
        public TokenStateMachine(Configuration configuration)
            : base(new TokenStartState(configuration))
        { }

        internal class Configuration
        {
            public bool AllowTextContent { get; set; }
            public bool AllowMultipleTokens { get; set; }
            public bool AllowAttributes { get; set; }
            public bool AllowDefaultAttribute { get; set; }
            public bool AllowEscapeSequence { get; set; }
        }

        internal class Result
        {
            public Token Token { get; set; }

            public int StartIndex { get; set; }
            public int LastIndex { get; set; }

            public Result()
            {
                Token = new Token();
            }
        }
    }

    internal abstract class TokenState : StringState<TokenStateMachine.Result, TokenState>
    {
        protected TokenStateMachine.Configuration Configuration { get; set; }

        protected override TNewState Move<TNewState>()
        {
            TNewState newState = base.Move<TNewState>();
            newState.Configuration = Configuration;
            return newState;
        }
    }


    internal class TokenStartState : TokenState
    {
        public TokenStartState()
        { }

        public TokenStartState(TokenStateMachine.Configuration configuration)
        {
            Context = new TokenStateMachine.Result();
            Configuration = configuration;
        }

        public override TokenState Accept(char input, int position)
        {
            if (input == '{')
                return Move<TokenFullnameState>();

            if (!Configuration.AllowTextContent)
                return Move<TokenErrorState>();

            Text.Append(input);
            return this;
        }
    }

    internal class TokenFullnameState : TokenState
    {
        public override TokenState Accept(char input, int position)
        {
            if (input == '{')
            {
                if (Text.Length == 0)
                {
                    if (Configuration.AllowEscapeSequence)
                        return Move<TokenStartState>();
                    else
                        return Move<TokenErrorState>();
                }

                return Move<TokenErrorState>();
            }

            if (input == ' ')
            {
                if (!Configuration.AllowAttributes)
                    return Move<TokenErrorState>();

                Context.Token.Fullname = Text.ToString();
                return Move<TokenDefaultAttributesState>();
            }

            if (input == '}')
            {
                Context.LastIndex = position;
                Context.Token.Fullname = Text.ToString();
                return Move<TokenDoneState>();
            }
            
            if (Text.Length == 0)
                Context.StartIndex = position - 1;

            Text.Append(input);
            return this;
        }
    }

    internal class TokenDefaultAttributesState : TokenState
    {
        private int innerExtensions = 0;

        public override TokenState Accept(char input, int position)
        {
            if (input == ',')
            {
                if (!Configuration.AllowDefaultAttribute)
                    return Move<TokenErrorState>();

                Context.Token.DefaultAttributes.Add(Text.ToString());
                return Move<TokenAttributeNameState>();
            }
            
            if (input == '=')
            {
                Context.Token.Attributes.Add(new TokenAttribute(Text.ToString()));
                return Move<TokenAttributeValueState>();
            }
            
            if (input == '}')
            {
                if (!Configuration.AllowDefaultAttribute)
                    return Move<TokenErrorState>();

                if (innerExtensions == 0)
                {
                    Context.LastIndex = position;
                    Context.Token.DefaultAttributes.Add(Text.ToString());
                    return Move<TokenDoneState>();
                }
                else
                {
                    innerExtensions--;
                }
            }
            
            if (input == '{')
                innerExtensions++;

            Text.Append(input);
            return this;
        }
    }

    internal class TokenAttributeNameState : TokenState
    {
        public override TokenState Accept(char input, int position)
        {
            if (input == '=')
            {
                Context.Token.Attributes.Add(new TokenAttribute(Text.ToString()));
                return Move<TokenAttributeValueState>();
            }

            if (input == ' ' && Text.Length == 0)
                return this;

            if (input == '{' || input == '}' || input == ',' || input == ' ')
                return Move<TokenErrorState>();

            Text.Append(input);
            return this;
        }
    }

    internal class TokenAttributeValueState : TokenState
    {
        private int innerExtensions = 0;

        public override TokenState Accept(char input, int position)
        {
            if (input == ',')
            {
                Context.Token.Attributes.LastOrDefault().Value = Text.ToString();
                return Move<TokenAttributeNameState>();
            }
            
            if (input == '}')
            {
                if (innerExtensions == 0)
                {
                    Context.LastIndex = position;
                    Context.Token.Attributes.LastOrDefault().Value = Text.ToString();
                    return Move<TokenDoneState>();
                }
                else
                {
                    innerExtensions--;
                }
            }
            
            if (input == '{')
                innerExtensions++;

            Text.Append(input);
            return this;
        }
    }




    internal class TokenDoneState : TokenStartState
    {
        protected override TokenStateMachine.Result GetContextForNewState()
        {
            return new TokenStateMachine.Result();
        }

        public override TokenState Accept(char input, int position)
        {
            if (!Configuration.AllowMultipleTokens && input == '{')
                return Move<TokenErrorState>();

            TokenState newState = base.Accept(input, position);
            return newState;
        }

        public TokenStateMachine.Result GetResult()
        {
            return Context;
        }
    }

    internal class TokenErrorState : TokenState
    {
        public override TokenState Accept(char input, int position)
        {
            Text.Append(input);
            return this;
        }
    }

}
