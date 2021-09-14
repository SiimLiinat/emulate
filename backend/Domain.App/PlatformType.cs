using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class PlatformType : DomainEntityId
    {
        [MaxLength(100)] public string Type { get; set; } = default!;
        
        public ICollection<Platform>? Platforms { get; set; }
    }
}