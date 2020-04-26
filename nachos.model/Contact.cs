using System;
using nachos.io.model.core;

namespace nachos.io.model
{
    public class Contact : ModelCore
    {
        public String Name { get; set; }
        public String MobileNumber { get; set; }
        public String AccessToken { get; set; }
        public DateTime? DateRegistered { get; set; } = null;

        public Guid? ContactGroup { get; set; }

        public Contact()
        {
        }
    }
}
