using System;
using System.Collections.Generic;
using nachos.io.model.core;

namespace nachos.io.model
{
    public class ContactGroup : ModelCore
    {
        public Guid OwnerId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public List<Contact> Contacts { get; set; }

        public ContactGroup()
        {
        }
    }
}
