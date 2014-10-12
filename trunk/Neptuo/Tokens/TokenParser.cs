using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    /// <summary>
    /// Parser for token syntax.
    /// Supported features can be se in <see cref="TokenParser.Configuration"/>.
    /// </summary>
    public class TokenParser
    {
        /// <summary>
        /// Feature configuration.
        /// </summary>
        public TokenParserConfiguration Configuration { get; private set; }

        /// <summary>
        /// Event fired when token is found in source text.
        /// </summary>
        public event EventHandler<TokenEventArgs> OnParsedToken;

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public TokenParser()
        {
            Configuration = new TokenParserConfiguration();
        }

        /// <summary>
        /// Processes <paramref name="content"/> and fires <see cref="TokenParser.OnParsedToken"/> when token is found.
        /// </summary>
        /// <param name="content">Source text.</param>
        /// <returns><c>true</c> if parsing was successfull (so <paramref name="content"/> contained token(s) or only plain value).</returns>
        public bool Parse(string content)
        {
            if (OnParsedToken == null)
                throw Guard.Exception.InvalidOperation("OnParsedItem event is null, so there isn't callback for parsed tokens.");

            List<TokenStateMachine.Result> results = new List<TokenStateMachine.Result>();

            TokenStateMachine stateMachine = new TokenStateMachine(GetStateMachineConfiguration());
            stateMachine.OnEnterConcreteState<TokenDoneState>((sender, e) => results.Add(e.State.GetResult()));

            TokenState finalState = stateMachine.Process(content);
            if (IsSuccessState(finalState))
            {
                foreach (TokenStateMachine.Result result in results)
                    OnParsedToken(this, new TokenEventArgs(content, result.Token, result.StartIndex, result.LastIndex + 1));

                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if <paramref name="finalState"/> is acceptable in current context (and configuration).
        /// </summary>
        /// <param name="finalState">Returned state from state machine.</param>
        /// <returns><c>true</c> if <paramref name="finalState"/> is acceptable; <c>false</c> otherwise.</returns>
        private bool IsSuccessState(TokenState finalState)
        {
            if (finalState.GetType() == typeof(TokenDoneState))
                return true;

            if (Configuration.AllowTextContent && finalState.GetType() == typeof(TokenStartState))
                return true;

            return false;
        }

        /// <summary>
        /// Converts parser configuration to state machine configuration.
        /// </summary>
        private TokenStateMachine.Configuration GetStateMachineConfiguration()
        {
            return new TokenStateMachine.Configuration
            {
                AllowAttributes = Configuration.AllowAttributes,
                AllowEscapeSequence = Configuration.AllowEscapeSequence,
                AllowDefaultAttributes = Configuration.AllowDefaultAttributes,
                AllowMultipleTokens = Configuration.AllowMultipleTokens,
                AllowTextContent = Configuration.AllowTextContent
            };
        }
    }
}
