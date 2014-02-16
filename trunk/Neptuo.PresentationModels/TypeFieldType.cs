using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
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

        public override string ToString()
        {
            return String.Format("(FieldType:{0})", Type.FullName);
        }

        //private bool? isNullable;
        //public bool IsNullable
        //{
        //    get
        //    {
        //        if (isNullable == null)
        //        {
        //            if (Type.IsValueType || Type.IsEnum || Type.IsPrimitive)
        //                isNullable = false;
        //            else
        //                isNullable = true;
        //        }
        //        return isNullable.Value;
        //    }
        //}

        //public bool CanAssign(object value)
        //{
        //    if (value == null)
        //        return IsNullable;

        //    Type valueType = value.GetType();
        //    return Type.IsAssignableFrom(valueType);
        //}
    }
}
