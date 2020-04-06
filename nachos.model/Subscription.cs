using System;
using nachos.io.model.core;

namespace nachos.model
{
    public class Subscription : ModelCore
    {
        public Guid OwnerId { get; set; }

        public Guid SubscriptionTypeId { get; set; }
        public DateTime DateContractStart { get; set; }
        public DateTime? DateContractEnd { get; set; }

        public Subscription()
        {

        }
    }
}
