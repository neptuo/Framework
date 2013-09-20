using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    public class TokenParser
    {
        public OnParsedToken OnParsedToken { get; set; }

        public bool Parse(string content)
        {
            if (OnParsedToken == null)
                throw new InvalidOperationException("OnParsedItem is null.");

            if (content.StartsWith("{"))
            {
                try
                {
                    StateMachine machine = new StateMachine();
                    StateMachineResult result = machine.Process(content);

                    if (result.Extension == null)
                        return false;

                    TokenEventArgs args = new TokenEventArgs(content, result.Extension, 0, result.LastIndex);
                    OnParsedToken(args);

                    return true;
                }
                catch (StateMachineException)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
