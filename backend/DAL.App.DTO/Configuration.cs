using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Configuration : DomainEntityId, IDomainAppUser<AppUser>, IDomainAppUserId
    {
        public AppUser? AppUser { get; set; }
        public Guid AppUserId { get; set; }
        
        [MaxLength(200)] public string ConfigurationString { get; set; } = default!;
        public DateTime CreationDt { get; set; } = DateTime.Now;

        public ICollection<Progress>? Progresses { get; set; }
        
    }
}