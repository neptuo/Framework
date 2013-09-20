using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    public class StateMachine
    {
        public StateMachineResult Process(string content)
        {
            Token result = new Token();

            State state = new StartState();
            state.Extension = result;

            int lastIndex = 0;
            foreach (char input in content)
            {
                lastIndex++;
                state = state.Process(input);
                if (state.GetType() == typeof(DoneState))
                    break;

                if (state.GetType() == typeof(ErrorState))
                    throw new StateMachineException();

            }

            return new StateMachineResult
            {
                Extension = result,
                LastIndex = lastIndex
            };
        }
    }

    public abstract class State
    {
        public Token Extension { get; set; }

        public string Stack { get; set; }

        public Dictionary<string, string> Context { get; set; }

        public State()
        {
            Context = new Dictionary<string, string>();
        }

        public abstract State Process(char input);

        #region Factory

        public T Create<T>(string name1 = null, string value1 = null, string name2 = null, string value2 = null)
            where T : State, new()
        {
            T result = new T();
            result.Extension = Extension;
            result.Stack = "";

            if (name1 != null)
                result.Context.Add(name1, value1);

            if (name2 != null)
                result.Context.Add(name2, value2);

            return result;
        }

        #endregion
    }


    public class StartState : State
    {
        public override State Process(char input)
        {
            if (input == '{')
                return Create<FullnameState>();

            Stack += input;
            return this;
        }
    }

    public class FullnameState : State
    {
        public override State Process(char input)
        {
            if (input == ' ')
            {
                Extension.Fullname = Stack;
                return Create<FirstAttributeState>();
            }
            else if (input == '}')
            {
                Extension.Fullname = Stack;
                return Create<DoneState>();
            }

            Stack += input;
            return this;
        }
    }

    public class FirstAttributeState : State
    {
        private int innerExtensions = 0;

        public override State Process(char input)
        {
            if (input == ',')
            {
                Extension.DefaultAttributeValue = Stack;
                return Create<AttributeNameState>();
            }
            else if (input == '=')
            {
                return Create<AttributeValueState>("attribute", Stack);
            }
            else if (input == '}')
            {
                if (innerExtensions == 0)
                {
                    Extension.DefaultAttributeValue = Stack;
                    return Create<DoneState>();
                }
                else
                {
                    innerExtensions--;
                }
            }
            else if (input == '{')
            {
                innerExtensions++;
            }

            Stack += input;
            return this;
        }
    }

    public class AttributeNameState : State
    {
        public override State Process(char input)
        {
            if (input == '=')
                return Create<AttributeValueState>("attribute", Stack);

            if (input == ' ' && String.IsNullOrEmpty(Stack))
                return this;

            if (input == '{' || input == '}' || input == ',' || input == ' ')
                return Create<ErrorState>();

            Stack += input;
            return this;
        }
    }

    public class AttributeValueState : State
    {
        private int innerExtensions = 0;

        public override State Process(char input)
        {
            if (input == ',')
            {
                Extension.Attributes.Add(new TokenAttribute
                {
                    Name = Context["attribute"],
                    Value = Stack
                });
                return Create<AttributeNameState>();
            }
            else if (input == '}')
            {
                if (innerExtensions == 0)
                {
                    Extension.Attributes.Add(new TokenAttribute
                    {
                        Name = Context["attribute"],
                        Value = Stack
                    });
                    return Create<DoneState>();
                }
                else
                {
                    innerExtensions--;
                }
            }
            else if (input == '{')
            {
                innerExtensions++;
            }

            Stack += input;
            return this;
        }
    }




    public class DoneState : State
    {
        public override State Process(char input)
        {
            Stack += input;
            return this;
        }
    }

    public class ErrorState : State
    {
        public override State Process(char input)
        {
            Stack += input;
            return this;
        }
    }
}
