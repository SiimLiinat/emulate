using System;

namespace PublicApi.DTO.v1.APIs
{
    public class Compatibility
    {
        public Guid Id { get; set; }
        
        public Guid CompatibilityTypeId { get; set; }
        public Guid GameOnPlatformId { get; set; }
        public Guid EmulatorId { get; set; }
        public string Date { get; set; } = default!;

        public string GameName { get; set; } = default!; // name of the game
        public string PlatformName { get; set; } = default!;
        public string EmulatorName { get; set; } = default!; // name of the emulator
        public string CompatibilityTypeType { get; set; } = default!; // name of the emulator
        public int CompatibilityTypeRank { get; set; } = default!;
    }
    
    public class CompatibilityAdd
    {
        public Guid CompatibilityTypeId { get; set; }
        public Guid GameOnPlatformId { get; set; }
        public Guid EmulatorId { get; set; }
        
        public DateTime Date { get; set; }
        
    }
}