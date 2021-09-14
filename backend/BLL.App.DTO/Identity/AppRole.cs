using System;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO.Identity
{
    public class AppRole : IdentityRole<Guid>, IDomainEntityId
    {
        /*
        [StringLength(128, MinimumLength = 1)]
        public string DisplayName { get; set; } = default!;
        */
    }
}