using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class PlatformType : DomainEntityId
    {
        [MaxLength(100)] public string Type { get; set; } = default!;

        public int PlatformsCount { get; set; }
    }
}