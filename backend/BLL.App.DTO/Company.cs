using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Company : DomainEntityId
    {
        [MaxLength(100)] public string Name { get; set; } = default!;

        public int PublishedGamesCount { get; set; }
        public int DevelopedGamesCount { get; set; }
    }
}