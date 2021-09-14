using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Game : DomainEntityId
    {
        public Guid DevCompanyId { get; set; }
        public Company? DevCompany { get; set; }
        
        public Guid PubCompanyId { get; set; }
        public Company? PubCompany { get; set; }

        [MaxLength(100)] public string Name { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
        
        public ICollection<GameGenre>? GameGenres { get; set; }
        public ICollection<Media>? Medias { get; set; }
        public ICollection<Progress>? Progresses { get; set; }
        public ICollection<GameOnPlatform>? GameOnPlatforms { get; set; }
    }
}