using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GameGenre : DomainEntityId
    {
        public Guid GenreId { get; set; }
        public Genre? Genre { get; set; } = default!;
        
        public Guid GameId { get; set; }
        public Game? Game { get; set; } = default!;
    }
}