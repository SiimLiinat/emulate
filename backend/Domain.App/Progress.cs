using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Progress : DomainEntityId, IDomainAppUser<AppUser>, IDomainAppUserId
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public Guid GameId { get; set; }
        public Game? Game { get; set; }
        
        public Guid? ConfigurationId { get; set; }
        public Configuration? Configuration { get; set; }
        
        public Guid? CompatibilityTypeId { get; set; }
        public CompatibilityType? CompatibilityType { get; set; }

        public bool Public { get; set; } = false;
        
        public int Time { get; set; }
        [Range(0, 10)] public int Score { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; }
        [MaxLength(1000)] public string? Review { get; set; }
        
    }
}