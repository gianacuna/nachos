using System;
using nachos.io.model.core;

namespace nachos.io.model
{
    public class User : ModelCore
    {
        public String UserName { get; set; }
        public String PasswordSalt { get; set; }
        public String PasswordHash { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String EmailAddress { get; set; }
        public String MobileNumber { get; set; }
        public Guid UserType { get; set; }

        public String Name
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        public User()
        {
        }
    }
}
