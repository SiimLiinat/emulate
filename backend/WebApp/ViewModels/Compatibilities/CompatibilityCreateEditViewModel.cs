using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Compatibilities
{
    /// <summary>
    /// Compatibility viewmodel for create and edit
    /// </summary>
    public class CompatibilityCreateEditViewModel
    {
        /// <summary>
        /// DAL.App.DTO.Compatibility
        /// </summary>
        public Compatibility Compatibility { get; set; } = default!;
        
        /// <summary>
        /// CompatibilityType select list
        /// </summary>
        public SelectList? CompatibilityTypeSelectList { get; set; }
        
        /// <summary>
        /// Emulator select list
        /// </summary>
        public SelectList? EmulatorSelectList { get; set; }
        
        /// <summary>
        /// GameOnPlatform select list
        /// </summary>
        public SelectList? GameOnPlatformSelectList { get; set; }
    }
}