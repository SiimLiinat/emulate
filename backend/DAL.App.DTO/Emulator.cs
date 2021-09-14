using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Emulator : DomainEntityId
    {
        public Guid PlatformId { get; set; }
        public Platform? Platform { get; set; } = default!;
        [MaxLength(100)] public string Name { get; set; } = default!;
        [MaxLength(100)] public string Url { get; set; } = default!;
        public string Picture { get; set; } = default!;
        
        public ICollection<Compatibility>? Compatibilities { get; set; }
    }
}