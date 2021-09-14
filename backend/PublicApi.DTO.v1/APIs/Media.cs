using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class Media
    {
        public Guid Id { get; set; }
        public Guid MediaTypeId { get; set; }
        public Guid? GameId { get; set; }
        public Guid AppUserId { get; set; }
        [MaxLength(500)] public string Url { get; set; } = default!;
        public string MediaTypeType { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
    
    public class MediaAdd
    {
        public Guid MediaTypeId { get; set; }
        public Guid? GameId { get; set; }
        public Guid? AppUserId { get; set; }
        [MaxLength(500)] public string Url { get; set; } = default!;
    }
}