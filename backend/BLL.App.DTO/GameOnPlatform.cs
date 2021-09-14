using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class GameOnPlatform : DomainEntityId
    {
        public Guid GameId { get; set; }
        public Game? Game { get; set; } = default!;
        
        public Guid PlatformId { get; set; }
        public Platform? Platform { get; set; } = default!;

        public DateTime ReleaseDate { get; set; } = default!;
    }
}