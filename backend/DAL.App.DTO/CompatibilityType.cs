using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class CompatibilityType : DomainEntityId
    {
        [MaxLength(50)] public string Type { get; set; } = default!;
        [MaxLength(200)] public string? Description { get; set; }
        [Range(0, 10)] public int Rating { get; set; }
        
        public ICollection<Compatibility>? Compatibilities { get; set; }
        public ICollection<Progress>? Progresses { get; set; }
    }
}