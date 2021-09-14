using System;

namespace PublicApi.DTO.v1.APIs
{
    public class AppRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string NormalizedName { get; set; } = default!;
    }

    public class AppRoleAdd
    {
        public string Name { get; set; } = default!;
    }
}