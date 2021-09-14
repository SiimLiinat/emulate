using System;
using System.Collections;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        /*
         [StringLength(128, MinimumLength = 1)]
        public string FirstName { get; set; } = default!;
        [StringLength(128, MinimumLength = 1)]
        public string LastName { get; set; } = default!;
        */
        
        public string? ProfilePicture { get; set; }
        
        public ICollection<Configuration>? Configurations { get; set; }
        public ICollection<Media>? Medias { get; set; }
        public ICollection<Progress>? Progresses { get; set; }
        public ICollection<AppUserRole>? AppUserRoles { get; set; }
    }
}