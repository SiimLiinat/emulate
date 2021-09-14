using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Genre : DomainEntityId
    {
        [MaxLength(50)] public string Type { get; set; } = default!;
        
        public ICollection<GameGenre>? GameGenres { get; set; }
    }
}