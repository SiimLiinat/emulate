using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Games
{
    /// <summary>
    /// Game viewmodel for create and edit
    /// </summary>
    public class GameCreateEditViewModel
    {
        /// <summary>
        /// DAL.App.DTO.Game
        /// </summary>
        public Game Game { get; set; } = default!;
        
        /// <summary>
        /// Developing companies select list
        /// </summary>
        public SelectList? DevCompanySelectList { get; set; }
        
        /// <summary>
        /// Publishing companies select list
        /// </summary>
        public SelectList? PubCompanySelectList { get; set; }
    }
}