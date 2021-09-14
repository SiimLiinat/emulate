using System;

namespace PublicApi.DTO.v1.APIs
{
    public class AppUserRole
    {
        public Guid UserId { get; set; } = default!;
        public Guid RoleId { get; set; } = default!;
    }
}