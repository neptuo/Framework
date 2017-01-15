using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Raised when the serialization/deserialization for passed value is not supported.
    /// </summary>
    public class NotSupportedValueException : CompositeException
    {
        /// <summary>
        /// Gets a type where the value is used.
        /// </summary>
        public Type CompositeType { get; private set; }

        /// <summary>
        /// Gets a name of the property where the value is used.
        /// </summary>
        public string ValuePropertyName { get; private set; }

        /// <summary>
        /// Gets a type of the value which is unnable to serialize/deserialize.
        /// </summary>
        public Type ValuePropertyType { get; private set; }

        /// <summary>
        /// Gets a value which is unnable to serialize/deserialize.
        /// </summary>
        /// <remarks>
        /// When raised during deserialization, this property is always <c>null</c>.
        /// </remarks>
        public object Value { get; private set; }

        /// <summary>
        /// Create new instance.
        /// This constructor is used when serializing a value to a composite storage.
        /// </summary>
        /// <param name="compositeType">A type where the value is used.</param>
        /// <param name="valuePropertyName">A name of the property where the value is used.</param>
        /// <param name="valuePropertyType">A type of the value which is unnable to serialize/deserialize.</param>
        /// <param name="value">A value which is unnable to serialize/deserialize.</param>
        public NotSupportedValueException(Type compositeType, string valuePropertyName, Type valuePropertyType, object value)
            : base(GetMessage(compositeType, valuePropertyName, valuePropertyType, value))
        {
            ValuePropertyType = valuePropertyType;
        }

        /// <summary>
        /// Create new instance.
        /// This constructor is used when deserializing a value from a composite storage.
        /// </summary>
        /// <param name="compositeType">A type where the value is used.</param>
        /// <param name="valuePropertyName">A name of the property where the value is used.</param>
        /// <param name="valuePropertyType">A type of the value which is unnable to serialize/deserialize.</param>
        public NotSupportedValueException(Type compositeType, string valuePropertyName, Type valuePropertyType)
            : base(GetMessage(compositeType, valuePropertyName, valuePropertyType, null))
        {
            ValuePropertyType = valuePropertyType;
        }

        private static string GetMessage(Type compositeType, string valuePropertyName, Type valuePropertyType, object value)
        {
            string message = GetMessage(compositeType, valuePropertyName, valuePropertyType);
            message += String.Format(" Current value is '{0}'.", value);

            return message;
        }

        private static string GetMessage(Type compositeType, string valuePropertyName, Type valuePropertyType)
        {
            return String.Format(
                "Unnable to serialize or deserialize value of type '{0}' of property '{1}' ('{2}').",
                valuePropertyType,
                valuePropertyName,
                compositeType
            );
        }
    }
}
