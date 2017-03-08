using Neptuo.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    public class StringValidationHandler : ValidationHandlerBase<string>
    {
        protected override void Handle(IValidationResultBuilder builder, string model)
        {
            base.Handle(builder, model);

            if (model != "Hello, World!" && model != "Hall Welt!")
                builder.AddTextMessage(null, "Message be a 'Hello, World!' kind of message.");
        }
    }
}
