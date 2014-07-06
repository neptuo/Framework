using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    /// <summary>
    /// Implementation of <see cref="IModelValidationBuilder"/> and <see cref="IValidationResult"/>.
    /// Instance of this class can act like both of these.
    /// </summary>
    public class ModelValidationBuilder : IValidationResult, IModelValidationBuilder
    {
        /// <summary>
        /// List of validator messages.
        /// </summary>
        protected List<IValidationMessage> MessageList { get; private set; }

        public bool IsValid
        {
            get { return MessageList.Count == 0; }
        }

        public IEnumerable<IValidationMessage> Messages
        {
            get { return MessageList; }
        }

        public ModelValidationBuilder()
        {
            MessageList = new List<IValidationMessage>();
        }

        public IModelValidationBuilder AddMessage(IValidationMessage message)
        {
            Guard.NotNull(message, "message");
            MessageList.Add(message);
            return this;
        }

        public IModelValidationBuilder AddMessages(IEnumerable<IValidationMessage> messages)
        {
            Guard.NotNull(messages, "messages");
            MessageList.AddRange(messages);
            return this;
        }

        public IValidationResult ToResult()
        {
            return this;
        }
    }
}
