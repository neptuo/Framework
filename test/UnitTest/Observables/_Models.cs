using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables
{
    public class MainModel : ObservableObject
    {
        private static readonly string fullNamePropertyName = TypeHelper.PropertyName<MainModel>(m => m.FullName);

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(fullNamePropertyName);
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(fullNamePropertyName);
                }
            }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
