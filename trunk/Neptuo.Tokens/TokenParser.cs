using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    public class TokenParser
    {
        public TokenParserConfiguration Configuration { get; private set; }
        public event EventHandler<TokenEventArgs> OnParsedToken;

        public TokenParser()
        {
            Configuration = new TokenParserConfiguration();
        }

        public bool Parse(string content)
        {
            if (OnParsedToken == null)
                throw new InvalidOperationException("OnParsedItem is null.");

            List<TokenStateMachine.Result> results = new List<TokenStateMachine.Result>();

            TokenStateMachine stateMachine = new TokenStateMachine(GetStateMachineConfiguration());
            stateMachine.OnEnterConcreteState<TokenDoneState>((sender, e) => results.Add(e.State.GetResult()));

            if (stateMachine.Process(content).GetType() != typeof(TokenErrorState))
            {
                foreach (TokenStateMachine.Result result in results)
                    OnParsedToken(this, new TokenEventArgs(content, result.Token, result.StartIndex, result.LastIndex));

                return true;
            }
            return false;
        }

        private TokenStateMachine.Configuration GetStateMachineConfiguration()
        {
            return new TokenStateMachine.Configuration
            {
                AllowAttributes = Configuration.AllowAttributes,
                AllowEscapeSequence = Configuration.AllowEscapeSequence,
                AllowDefaultAttribute = Configuration.AllowDefaultAttribute,
                AllowMultipleTokens = Configuration.AllowMultipleTokens,
                AllowTextContent = Configuration.AllowTextContent
            };
        }
    }
}
