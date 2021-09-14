using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.GameGenres
{
    /// <summary>
    /// GameGenre viewmodel for create and edit
    /// </summary>
    public class GameGenreCreateEditViewModel
    {
        /// <summary>
        /// DAL.App.DTO.GameGenre
        /// </summary>
        public GameGenre GameGenre { get; set; } = default!;
        
        /// <summary>
        /// Game select list
        /// </summary>
        public SelectList? GameSelectList { get; set; }
        
        /// <summary>
        /// Genre select list
        /// </summary>
        public SelectList? GenreSelectList { get; set; }
    }
}