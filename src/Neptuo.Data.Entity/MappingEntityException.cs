using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    public class MappingEntityException : DataException
    {
        public MappingEntityException(Type businessType, Type entityType)
            : base(String.Format("Input must be of type '{0}' when mapping to '{1}'", entityType.Name, businessType.Name))
        { }
    }
}
