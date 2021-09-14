using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class PlatformType
    {
        public Guid Id { get; set; }
        [MaxLength(100)] public string Type { get; set; } = default!;
        public int PlatformCount { get; set; }
    }
    
    public class PlatformTypeAdd
    {
        [MaxLength(100)] public string Type { get; set; } = default!;
    }
}