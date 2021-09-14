using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        public string? ProfilePicture { get; set; }
        public ICollection<Configuration>? Configurations { get; set; }
        public ICollection<Media>? Medias { get; set; }
        public ICollection<Progress>? Progresses { get; set; }
    }
}