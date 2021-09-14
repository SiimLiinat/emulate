using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class MediaType : DomainEntityId
    {
        [MaxLength(50)] public string Type { get; set; } = default!;
        [MaxLength(200)] public string? Description { get; set; }
        
        public ICollection<Media>? Medias { get; set; }
    }
}