using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Game : DomainEntityId
    {
        public Guid DevCompanyId { get; set; }
        public Company? DevCompany { get; set; } = default!;
        
        public Guid PubCompanyId { get; set; }
        public Company? PubCompany { get; set; }

        [MaxLength(100)] public string Name { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
        
        public string? CompatibilityType; // Most popular compatibility type
        public float? CompatibilityPercentage; // Percentage of the most popular compatibility out of all compatibility types
        public int CompatibilityRank { get; set; } // Most popular compatibility type's rating
        
        public ICollection<GameGenre>? GameGenres { get; set; }
        public ICollection<Media>? Medias { get; set; }
        public ICollection<GameOnPlatform>? GameOnPlatforms { get; set; }
    }
}