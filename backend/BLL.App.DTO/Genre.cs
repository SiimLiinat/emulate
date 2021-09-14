﻿using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Genre : DomainEntityId
    {
        [MaxLength(50)] public string Type { get; set; } = default!;
        
        // public ICollection<GameGenre>? GameGenres { get; set; }
    }
}