using System;
namespace nachos.io.model.core
{
    public abstract class ModelCore
    {
        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; } = null;
        public DateTime? DateUpdated { get; set; } = null;
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Boolean IsActive { get; set; }

        public ModelCore()
        {
        }
        
    }
}