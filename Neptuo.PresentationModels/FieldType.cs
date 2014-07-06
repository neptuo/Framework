using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Field definition from standart type.
    /// </summary>
    public class FieldType : IFieldType
    {
        /// <summary>
        /// Source field type.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="type"/>.
        /// </summary>
        /// <param name="type">Source field type.</param>
        public FieldType(Type type)
        {
            Guard.NotNull(type, "type");
            Type = type;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        /// <summary>
        /// Compares instance using comparing source types.
        /// </summary>
        /// <param name="obj">Other value.</param>
        /// <returns>true if the <paramref name="obj"/> is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            FieldType fieldType = obj as FieldType;
            if (fieldType == null)
                return false;

            return fieldType.Type == Type;
        }

        public override string ToString()
        {
            return String.Format("(FieldType:{0}:{1})", Type.FullName, GetGenericArgumentFullName());
        }

        /// <summary>
        /// Gets source type generic argument name if source type is generic; empty string otherwise.
        /// </summary>
        /// <returns>Source type generic argument name if source type is generic; empty string otherwise.</returns>
        private string GetGenericArgumentFullName()
        {
            if (Type.IsGenericType)
                return Type.GetGenericArguments()[0].FullName;

            return String.Empty;
        }

        /// <summary>
        /// Creates new instance with <paramref name="type"/>.
        /// </summary>
        /// <param name="type">Source field type.</param>
        public static FieldType FromType(Type type)
        {
            return new FieldType(type);
        }
    }
}
