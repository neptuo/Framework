using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Models
{
    public class UserModel : GuidedObject
    {
        public int Version { get; private set; }
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public UserModel(string userName, string password)
        {
            Ensure.NotNullOrEmpty(userName, "userName");
            Ensure.NotNullOrEmpty(password, "password");
            UserName = userName;
            Password = password;
            Version = 1;
        }

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
