using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CompatibilityType : DomainEntityId
    {
        [MaxLength(50)] public string Type { get; set; } = default!;
        [MaxLength(200)] public string? Description { get; set; }
        [Range(0, 10)] public int Rating { get; set; }
    }
}