using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain.Base;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Configuration : DomainEntityId, IDomainAppUser<AppUser>, IDomainAppUserId
    {
        public AppUser? AppUser { get; set; }
        public Guid AppUserId { get; set; }
        [MaxLength(200)] public string ConfigurationString { get; set; } = default!;
        public DateTime CreationDt { get; set; } = DateTime.Now;
        
    }
}