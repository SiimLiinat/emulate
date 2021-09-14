using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Emulators
{
    /// <summary>
    /// Emulator viewmodel for create and edit
    /// </summary>
    public class EmulatorCreateEditViewModel
    {
        /// <summary>
        /// DAL.App.DTO.Emulator
        /// </summary>
        public DAL.App.DTO.Emulator Emulator { get; set; } = default!;
        
        /// <summary>
        /// Platform select list
        /// </summary>
        public SelectList? PlatformSelectList { get; set; }
    }
}