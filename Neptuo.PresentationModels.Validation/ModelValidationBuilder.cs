using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public class ModelValidationBuilder : IModelValidationResult, IModelValidationBuilder
    {
        protected List<IValidationMessage> MessageList { get; private set; }

        public bool IsValid { get; protected set; }
        public IEnumerable<IValidationMessage> Messages
        {
            get { return MessageList; }
        }

        public ModelValidationBuilder()
        {
            MessageList = new List<IValidationMessage>();
        }

        public void AddMessage(IValidationMessage message)
        {
            MessageList.Add(message);
        }

        public void AddMessages(IEnumerable<IValidationMessage> messages)
        {
            MessageList.AddRange(messages);
        }

        public IModelValidationResult ToResult()
        {
            return this;
        }
    }
}
