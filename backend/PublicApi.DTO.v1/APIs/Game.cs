using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;

namespace PublicApi.DTO.v1.APIs
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid DevCompanyId { get; set; }
        public Guid PubCompanyId { get; set; }
        [MaxLength(100)] public string Name { get; set; } = default!;
        public string ReleaseDate { get; set; } = default!;

        public string DevCompanyName { get; set; } = default!;
        public string PubCompanyName { get; set; } = default!;
        public string? CompatibilityType { get; set; } // Most popular compatibility type
        public int CompatibilityRank { get; set; } // Most popular compatibility type
        public float? CompatibilityPercentage { get; set; } // Percentage of the most popular compatibility out of all compatibility types
        public string[] Platforms { get; set; } = new List<string>().ToArray();
        public string[] Genres { get; set; } = new List<string>().ToArray();
        public string? CoverArt { get; set; }
    }
    
    public class GameAdd
    {
        public Guid DevCompanyId { get; set; }
        public Guid PubCompanyId { get; set; }
        [MaxLength(100)] public string Name { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
    }
}