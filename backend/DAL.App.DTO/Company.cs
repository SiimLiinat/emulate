using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Company : DomainEntityId
    {
        [MaxLength(100)] 
        [Display(ResourceType = typeof(Resources.DAL.App.DTO.Company), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        public int DevelopedGamesCount { get; set; } = 0;
        public int PublishedGamesCount { get; set; } = 0;
        public ICollection<Platform>? Platforms { get; set; }
        
        [InverseProperty(nameof(Game.DevCompany))]
        public ICollection<Game>? DevelopedGames { get; set; }
        
        [InverseProperty(nameof(Game.PubCompany))]
        public ICollection<Game>? PublishedGames { get; set; }
    }
}