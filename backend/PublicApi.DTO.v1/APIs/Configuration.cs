using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class Configuration
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        [MaxLength(200)] public string ConfigurationString { get; set; } = default!;
        public string CreationDt { get; set; } = default!;
    }
    
    public class ConfigurationAdd
    {
        public Guid AppUserId { get; set; }
        [MaxLength(200)] public string ConfigurationString { get; set; } = default!;
        public DateTime CreationDt { get; set; } = DateTime.Now;
    }
}