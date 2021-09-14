using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Media : DomainEntityId, IDomainAppUser<AppUser>
    {
        public Guid MediaTypeId { get; set; }
        public MediaType? MediaType { get; set; }
        
        public Guid? GameId { get; set; }
        public Game? Game { get; set; }
        
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        [MaxLength(500)] public string Url { get; set; } = default!;
    }
}