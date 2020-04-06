using System;
using nachos.io.model.core;

namespace nachos.io.model
{
    public class Contact : ModelCore
    {
        public String DisplayLabel { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public int CountryCode { get; set; }
        public float PhoneNumber { get; set; }

        public Contact()
        {

        }
    }
}
