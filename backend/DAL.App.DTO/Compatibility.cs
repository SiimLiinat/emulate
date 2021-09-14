using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Compatibility : DomainEntityId
    {
        public Guid CompatibilityTypeId { get; set; }
        public CompatibilityType? CompatibilityType { get; set; }
        public Guid GameOnPlatformId { get; set; }
        public GameOnPlatform? GameOnPlatform { get; set; }
        
        public Guid EmulatorId { get; set; }
        public Emulator? Emulator { get; set; }

        public DateTime Date { get; set; }
        
    }
}