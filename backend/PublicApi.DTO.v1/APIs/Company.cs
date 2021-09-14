using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.APIs
{
    public class Company
    {
        public Guid Id { get; set; }
        [MaxLength(100)] public string Name { get; set; } = default!;

        public int PublishedCount { get; set; }
        public int DevelopedCount { get; set; }
    }
    
    public class CompanyAdd
    {
        [MaxLength(100)] public string Name { get; set; } = default!;
    }
}