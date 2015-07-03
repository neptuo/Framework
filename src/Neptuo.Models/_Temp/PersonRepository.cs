using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models
{
    class PersonRepository : IRepository<PersonModel, Int32Key>
    {
        public PersonModel Find(Int32Key key)
        {
            throw new NotImplementedException();
        }

        public void Save(PersonModel model)
        {
            throw new NotImplementedException();
        }
    }

    class PersonModel : IModel<Int32Key>
    {
        public Int32Key Key { get; set; }
    }

    class OrganizationRepository : IRepository<OrganizationModel, Int32Key>
    {
        public void Save(OrganizationModel model)
        {
            throw new NotImplementedException();
        }

        public OrganizationModel Find(Int32Key key)
        {
            throw new NotImplementedException();
        }
    }

    class OrganizationModel : IDomainModel<Int32Key>
    {
        public Int32Key Key { get; private set; }
    }

}
