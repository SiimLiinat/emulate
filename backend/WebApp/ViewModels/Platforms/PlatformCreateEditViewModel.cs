using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Platforms
{
    /// <summary>
    /// Platform viewmodel for create and edit
    /// </summary>
    public class PlatformCreateEditViewModel
    {
        /// <summary>
        /// DAL.App.DTO.Platform
        /// </summary>
        public Platform Platform { get; set; } = default!;
        
        /// <summary>
        /// Company select list
        /// </summary>
        public SelectList? CompanySelectList { get; set; }
        
        /// <summary>
        /// PlatformType select list
        /// </summary>
        public SelectList? PlatformTypeSelectList { get; set; }
    }
}