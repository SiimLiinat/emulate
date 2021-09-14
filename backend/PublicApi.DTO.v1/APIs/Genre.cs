using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class Genre
    {
        public Guid Id { get; set; }
        [MaxLength(50)] public string Type { get; set; } = default!;
    }
    
    public class GenreAdd
    {
        [MaxLength(50)] public string Type { get; set; } = default!;
    }
}