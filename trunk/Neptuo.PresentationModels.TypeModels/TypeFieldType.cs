using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public class TypeFieldType : IFieldType
    {
        public Type Type { get; private set; }

        public TypeFieldType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            Type = type;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            TypeFieldType fieldType = obj as TypeFieldType;
            if (fieldType == null)
                return false;

            return fieldType.Type == Type;
        }
    }
}
