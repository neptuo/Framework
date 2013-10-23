using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.PresentationModels
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MatchPropertyAttribute : Attribute
    {
        public string TargetProperty { get; private set; }

        public MatchPropertyAttribute(string targetProperty)
        {
            if (targetProperty == null)
                throw new ArgumentNullException("targetProperty");

            TargetProperty = targetProperty;
        }
    }
}
