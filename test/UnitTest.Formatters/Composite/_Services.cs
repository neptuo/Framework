using Neptuo;
using Neptuo.Formatters;
using Neptuo.Formatters.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Formatters.Composite
{
    [CompositeType("Test.UserModel")]
    public class UserModel
    {
        [CompositeVersion]
        public int Version { get; private set; }

        [CompositeProperty(1, Version = 2)]
        public int Id { get; private set; }

        [CompositeProperty(1)]
        [CompositeProperty(2, Version = 2)]
        public string UserName { get; private set; }

        [CompositeProperty(2)]
        [CompositeProperty(3, Version = 2)]
        public string Password { get; private set; }

        [CompositeConstructor]
        public UserModel(string userName, string password)
        {
            Ensure.NotNullOrEmpty(userName, "userName");
            Ensure.NotNullOrEmpty(password, "password");
            UserName = userName;
            Password = password;
            Version = 1;
        }

        [CompositeConstructor(Version = 2)]
        public UserModel(int id, string userName, string password)
        {
            Ensure.Positive(id, "id");
            Ensure.NotNullOrEmpty(userName, "userName");
            Ensure.NotNullOrEmpty(password, "password");
            Id = id;
            UserName = userName;
            Password = password;
            Version = 2;
        }
    }
}
