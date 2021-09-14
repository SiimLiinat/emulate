using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class CompatibilityType
    {
        public Guid Id { get; set; }
        [MaxLength(50)] public string Type { get; set; } = default!;
        [MaxLength(200)] public string? Description { get; set; }
        [Range(0, 10)] public int Rating { get; set; }
    }
    
    public class CompatibilityTypeAdd
    {
        [MaxLength(50)] public string Type { get; set; } = default!;
        [MaxLength(200)] public string? Description { get; set; }
        [Range(0, 10)] public int Rating { get; set; }
    }
}