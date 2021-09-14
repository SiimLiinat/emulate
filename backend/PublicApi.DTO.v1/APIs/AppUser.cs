using System;

namespace PublicApi.DTO.v1.APIs
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string SecurityStamp { get; set; } = default!;
        public DateTimeOffset? LockoutEnd { get; set; }
        public string? ProfilePicture { get; set; }
    }

    public class AppUserAdd
    {
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}