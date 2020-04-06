using System;
namespace nachos.io.model.core
{
    public abstract class ModelCore
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid? AddedBy { get; set; }
        public Guid? EditedBy { get; set; }
    }
}