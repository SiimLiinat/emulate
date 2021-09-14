using System;
using System.Collections.Generic;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GameOnPlatform : DomainEntityId
    {
        public Guid GameId { get; set; }
        public Game? Game { get; set; } = default!;
        
        public Guid PlatformId { get; set; }
        public Platform? Platform { get; set; } = default!;

        public DateTime ReleaseDate { get; set; } = default!;
        
        public ICollection<Compatibility>? Compatibilities { get; set; }
    }
}