using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class GameOnPlatform : DomainEntityId
    {
        public Guid GameId { get; set; }
        public Game? Game { get; set; }
        
        public Guid PlatformId { get; set; }
        public Platform? Platform { get; set; }

        public DateTime ReleaseDate { get; set; }
        
        public ICollection<Compatibility>? Compatibilities { get; set; }
    }
}