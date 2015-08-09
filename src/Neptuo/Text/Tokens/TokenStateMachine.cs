using Neptuo.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Tokens
{
    /// <summary>
    /// Internal token parser realized as state machine.
    /// </summary>
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
            public bool AllowDefaultAttributes { get; set; }
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
        protected bool HasToken { get; set; }

        protected bool WasLineStart { get; set; }
        protected int LineNumber { get; set; }
        protected int ColumnIndex { get; set; }

        protected int CurrentLineNumber { get; set; }
        protected int CurrentColumnIndex { get; set; }

        protected override TNewState Move<TNewState>()
        {
            TNewState newState = base.Move<TNewState>();
            newState.Configuration = Configuration;
            newState.HasToken = HasToken;

            newState.CurrentLineNumber = CurrentLineNumber;
            newState.CurrentColumnIndex = CurrentColumnIndex;
            newState.LineNumber = LineNumber;
            newState.ColumnIndex = ColumnIndex;
            
            return newState;
        }

        // \r\n
        protected void UpdateCurrentLineInfo(char input, int position)
        {
            if (WasLineStart && input == '\n')
            {
                WasLineStart = false;
                CurrentLineNumber++;
                CurrentColumnIndex = -1;
                return;
            }

            if (!WasLineStart && input == '\r')
            {
                WasLineStart = true;
                return;
            }

            CurrentColumnIndex++;
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
            {
                LineNumber = CurrentLineNumber;
                ColumnIndex = CurrentColumnIndex;
                return Move<TokenFullnameState>();
            }

            if (!Configuration.AllowTextContent)
                return Move<TokenErrorState>();

            Text.Append(input);
            return this;
        }
    }

    /// <summary>
    /// After escape start sequence '{{'.
    /// After finding '}}' moves to start state.
    /// </summary>
    internal class TokenEscapeState : TokenState
    {
        private bool hasFirstEscape;

        public override TokenState Accept(char input, int position)
        {
            if(input == '}')
            {
                if (hasFirstEscape)
                    return Move<TokenStartState>();

                hasFirstEscape = true;
            }

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
                        return Move<TokenEscapeState>();
                }

                return Move<TokenErrorState>();
            }

            if (HasToken && !Configuration.AllowMultipleTokens)
                return Move<TokenErrorState>();

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
            if (input == ' ' && Text.Length == 0)
                return this;

            if (innerExtensions == 0)
            {
                if (input == ',')
                {
                    if (!Configuration.AllowDefaultAttributes)
                        return Move<TokenErrorState>();

                    Context.Token.AddDefaultAttribute(Text.ToString());
                    return Move<TokenDefaultAttributesState>();
                }

                if (input == '=')
                {
                    Context.Token.AddAttribute(new TokenAttribute(Text.ToString()));
                    return Move<TokenAttributeValueState>();
                }
            }
            
            if (input == '}')
            {
                if (!Configuration.AllowDefaultAttributes)
                    return Move<TokenErrorState>();

                if (innerExtensions == 0)
                {
                    Context.LastIndex = position;
                    Context.Token.AddDefaultAttribute(Text.ToString());
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

    /// <summary>
    /// When processing 
    /// </summary>
    internal class TokenAttributeNameState : TokenState
    {
        public override TokenState Accept(char input, int position)
        {
            if (input == '=')
            {
                Context.Token.AddAttribute(new TokenAttribute(Text.ToString()));
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

    /// <summary>
    /// When processing named attribute value.
    /// </summary>
    internal class TokenAttributeValueState : TokenState
    {
        private int innerExtensions = 0;

        public override TokenState Accept(char input, int position)
        {
            if (innerExtensions == 0)
            {
                if (input == ',')
                {
                    Context.Token.Attributes.LastOrDefault().Value = Text.ToString();
                    return Move<TokenAttributeNameState>();
                }
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



    /// <summary>
    /// When processing input finished successfully.
    /// </summary>
    internal class TokenDoneState : TokenStartState
    {
        protected override TokenStateMachine.Result GetContextForNewState()
        {
            return new TokenStateMachine.Result();
        }

        public override TokenState Accept(char input, int position)
        {
            HasToken = true;
            TokenState newState = base.Accept(input, position);
            return newState;
        }

        public TokenStateMachine.Result GetResult()
        {
            return Context;
        }
    }

    /// <summary>
    /// When input was not valid to token.
    /// </summary>
    internal class TokenErrorState : TokenState
    {
        public override TokenState Accept(char input, int position)
        {
            Text.Append(input);
            return this;
        }
    }

}
