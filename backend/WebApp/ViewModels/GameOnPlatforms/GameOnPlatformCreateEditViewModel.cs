using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GameOnPlatforms
{
    /// <summary>
    /// GameOnPlatform viewmodel for create and edit
    /// </summary>
    public class GameOnPlatformCreateEditViewModel
    {
        /// <summary>
        /// DAL.App.DTO.GameOnPlatform
        /// </summary>
        public GameOnPlatform GameOnPlatform { get; set; } = default!;
        
        /// <summary>
        /// Game select list
        /// </summary>
        public SelectList? GameSelectList { get; set; }
        
        /// <summary>
        /// Platform select list
        /// </summary>
        public SelectList? PlatformSelectList { get; set; }
    }
}