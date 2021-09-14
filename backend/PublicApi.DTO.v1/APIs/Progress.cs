using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class Progress
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid GameId { get; set; }
        public Guid? ConfigurationId { get; set; }
        public Guid? CompatibilityTypeId { get; set; }
        public bool Public { get; set; } = false;
        public float Time { get; set; }
        public int Score { get; set; }
        public string CreatedAt { get; set; } = default!;
        public string? EditedAt { get; set; }
        [MaxLength(1000)] public string? Review { get; set; }

        public string AppUserName { get; set; } = default!;
        public string? AppUserProfile { get; set; }
        public string GameName { get; set; } = default!;
        public string? ConfigurationString { get; set; }
        public string? CompatibilityTypeType { get; set; }
        public int? CompatibilityTypeRank { get; set; }
    }
    
    public class ProgressAdd
    {
        public Guid AppUserId { get; set; }
        public Guid GameId { get; set; }
        public Guid? ConfigurationId { get; set; }
        public Guid? CompatibilityTypeId { get; set; }
        public bool Public { get; set; } = false;
        public int Time { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        [MaxLength(1000)] public string? Review { get; set; }
    }
}