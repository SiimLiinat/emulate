using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.GameGenres;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for GameGenres.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class GameGenresController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        
        /// <summary>
        /// Controller for GameGenres.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public GameGenresController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: GameGenres
        /// <summary>
        /// Get all genres of games.
        /// </summary>
        /// <returns>List of GameGenres</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.GameGenres.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: GameGenres/Details/5
        /// <summary>
        /// Get one genre of games. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of genre of games to retrieve, Guid</param>
        /// <returns>GameGenre entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _uow.GameGenres.FirstOrDefaultAsync(id.Value);
            if (gameGenre == null)
            {
                return NotFound();
            }

            return View(gameGenre);
        }

        // GET: GameGenres/Create
        /// <summary>
        /// Get genre of games for create.
        /// </summary>
        /// <returns>View of genre of games</returns>
        public async Task<IActionResult> Create()
        {
            var vm = new GameGenreCreateEditViewModel();
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name));
            vm.GenreSelectList = new SelectList(await _uow.Genres.GetAllAsync(), nameof(Genre.Id), nameof(Genre.Type));
            return View(vm);
        }

        // POST: GameGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a genre of games.
        /// </summary>
        /// <param name="vm">GameGenre viewmodel</param>
        /// <returns>View of genre of games</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameGenreCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.GameGenres.Add(vm.GameGenre);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.GameGenre.GameId);
            vm.GenreSelectList = new SelectList(await _uow.Genres.GetAllAsync(), nameof(Genre.Id), nameof(Genre.Type), vm.GameGenre.GenreId);
            return View(vm);
        }

        // GET: GameGenres/Edit/5
        /// <summary>
        /// Get genre of games by id for edit.
        /// </summary>
        /// <param name="id">Id of genre of games to edit, Guid</param>
        /// <returns>View of genre of games</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _uow.GameGenres.FirstOrDefaultAsync(id.Value);
            if (gameGenre == null)
            {
                return NotFound();
            }
            var vm = new GameGenreCreateEditViewModel();
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.GameGenre.GameId);
            vm.GenreSelectList = new SelectList(await _uow.Genres.GetAllAsync(), nameof(Genre.Id), nameof(Genre.Type), vm.GameGenre.GenreId);
            return View(vm);
        }

        // POST: GameGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a genre of games.
        /// </summary>
        /// <param name="id">Id of genre of games to edit, Guid</param>
        /// <param name="vm">GameGenre viewmodel</param>
        /// <returns>View of genre of games</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameGenreCreateEditViewModel vm)
        {
            if (id != vm.GameGenre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.GameGenres.Update(vm.GameGenre);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.GameGenre.GameId);
            vm.GenreSelectList = new SelectList(await _uow.Genres.GetAllAsync(), nameof(Genre.Id), nameof(Genre.Type), vm.GameGenre.GenreId);
            return View(vm);
        }

        // GET: GameGenres/Delete/5
        /// <summary>
        /// Get genre of games by id for delete.
        /// </summary>
        /// <param name="id">Id of genre of games to delete, Guid</param>
        /// <returns>View of genre of games</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameGenre = await _uow.GameGenres.FirstOrDefaultAsync(id.Value);
            if (gameGenre == null)
            {
                return NotFound();
            }

            return View(gameGenre);
        }

        // POST: GameGenres/Delete/5
        /// <summary>
        /// Delete genre of games.
        /// </summary>
        /// <param name="id">Id of genre of games to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.GameGenres.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
