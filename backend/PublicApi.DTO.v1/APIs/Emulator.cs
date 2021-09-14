using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class Emulator
    {
        public Guid Id { get; set; }
        public Guid PlatformId { get; set; }
        [MaxLength(100)] public string Name { get; set; } = default!;
        [MaxLength(100)] public string Url { get; set; } = default!;
        public string PlatformName { get; set; } = default!;
        public string Picture { get; set; } = default!;
        public int ProgramCount { get; set; }
    }
    
    public class EmulatorAdd
    {
        public Guid PlatformId { get; set; }
        [MaxLength(100)] public string Name { get; set; } = default!;
        [MaxLength(100)] public string Url { get; set; } = default!;
        public string Picture { get; set; } = default!;
    }
}