using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class PlatformType : DomainEntityId
    {
        [MaxLength(100)] public string Type { get; set; } = default!;
        public int PlatformsCount { get; set; } = 0;
        public ICollection<Platform>? Platforms { get; set; }
    }
}