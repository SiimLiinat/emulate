using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Progresses
{
    /// <summary>
    /// Progress viewmodel for create and edit
    /// </summary>
    public class ProgressCreateEditViewModel
    {
        /// <summary>
        /// DAL.App.DTO.Progress
        /// </summary>
        public Progress Progress { get; set; } = default!;
        
        /// <summary>
        /// CompatibilityType select list
        /// </summary>
        public SelectList? CompatibilityTypeSelectList { get; set; }
        
        /// <summary>
        /// Configuration select list
        /// </summary>
        public SelectList? ConfigurationSelectList { get; set; }
        
        /// <summary>
        /// Game select list
        /// </summary>
        public SelectList? GameSelectList { get; set; }
    }
}