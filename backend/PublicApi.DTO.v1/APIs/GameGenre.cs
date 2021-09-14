using System;

namespace PublicApi.DTO.v1.APIs
{
    public class GameGenre
    {
        public Guid Id { get; set; }
        public Guid GenreId { get; set; }
        public Guid GameId { get; set; }
        
        public string GameName { get; set; } = default!;
        public string GenreType { get; set; } = default!;
    }
    
    public class GameGenreAdd
    {
        public Guid GameId { get; set; }
        public Guid GenreId { get; set; }
    }
}