using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class MediaType
    {
        public Guid Id { get; set; }
        [MaxLength(50)] public string Type { get; set; } = default!;
        [MaxLength(200)] public string? Description { get; set; }
    }
    
    public class MediaTypeAdd
    {
        [MaxLength(50)] public string Type { get; set; } = default!;
        [MaxLength(200)] public string? Description { get; set; }
    }
}