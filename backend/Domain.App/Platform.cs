using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Platform : DomainEntityId
    {
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; } = default!;

        public Guid PlatformTypeId { get; set; }
        public PlatformType? PlatformType { get; set; } = default!;

        [MaxLength(50)] public string Name { get; set; } = default!;
        [MaxLength(10)] public string Code { get; set; } = default!;
        
        public ICollection<Emulator>? Emulators { get; set; }
        public ICollection<GameOnPlatform>? GameOnPlatforms { get; set; }
    }
}