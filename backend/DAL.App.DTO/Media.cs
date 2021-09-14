using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Media : DomainEntityId, IDomainAppUser<AppUser>
    {
        public Guid MediaTypeId { get; set; }
        public MediaType? MediaType { get; set; } = default!;
        
        public Guid? GameId { get; set; }
        public Game? Game { get; set; }
        
        public AppUser? AppUser { get; set; }
        public Guid? AppUserId { get; set; }
        [MaxLength(500)] public string Url { get; set; } = default!;
        
    }
}