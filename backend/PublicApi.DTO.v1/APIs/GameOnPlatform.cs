using System;

namespace PublicApi.DTO.v1.APIs
{
    public class GameOnPlatform
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlatformId { get; set; }
        public string ReleaseDate { get; set; } = default!;
        
        public string GameName { get; set; } = default!;
        public string PlatformName { get; set; } = default!;
    }
    
    public class GameOnPlatformAdd
    {
        public Guid GameId { get; set; }
        public Guid PlatformId { get; set; }
        public DateTime ReleaseDate { get; set; } = default!;
    }
}