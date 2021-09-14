using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class Platform
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid PlatformTypeId { get; set; }
        [MaxLength(50)] public string Name { get; set; } = default!;
        [MaxLength(10)] public string Code { get; set; } = default!;

        public string CompanyName { get; set; } = default!;
        public string PlatformTypeType { get; set; } = default!;
    }
    
    public class PlatformAdd
    {
        public Guid CompanyId { get; set; }
        public Guid PlatformTypeId { get; set; }
        [MaxLength(50)] public string Name { get; set; } = default!;
        [MaxLength(10)] public string Code { get; set; } = default!;
    }
}