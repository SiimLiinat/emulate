using System;
using Domain.Base;

namespace Domain.App
{
    public class GameGenre : DomainEntityId
    {
        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; } = default!;
        
        public Guid GameId { get; set; }
        public Game? Game { get; set; } = default!;
    }
}