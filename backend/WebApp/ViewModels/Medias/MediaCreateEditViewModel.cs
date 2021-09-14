using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Medias
{
    /// <summary>
    /// Media viewmodel for create and edit
    /// </summary>
    public class MediaCreateEditViewModel
    {
        /// <summary>
        /// DAL.App.DTO.Media
        /// </summary>
        public Media Media { get; set; } = default!;
        
        /// <summary>
        /// Game select list
        /// </summary>
        public SelectList? GameSelectList { get; set; }
        
        /// <summary>
        /// MediaType select list
        /// </summary>
        public SelectList? MediaTypeSelectList { get; set; }
    }
}