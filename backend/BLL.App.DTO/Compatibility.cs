using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Compatibility : DomainEntityId
    {
        public Guid CompatibilityTypeId { get; set; }
        public CompatibilityType? CompatibilityType { get; set; } = default!;
        
        public Guid GameOnPlatformId { get; set; }
        public GameOnPlatform? GameOnPlatform { get; set; } = default!;
        
        public Guid EmulatorId { get; set; }
        public Emulator? Emulator { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}