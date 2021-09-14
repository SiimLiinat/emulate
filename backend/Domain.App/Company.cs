using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class Company : DomainEntityId
    {
        [MaxLength(100)] public string Name { get; set; } = default!;
        
        public ICollection<Platform>? Platforms { get; set; }
        [InverseProperty(nameof(Game.DevCompany))]
        
        public ICollection<Game>? DevelopedGames { get; set; }
        [InverseProperty(nameof(Game.PubCompany))]
        public ICollection<Game>? PublishedGames { get; set; }
    }
}