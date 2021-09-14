using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Games;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Games.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class GamesController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for Games.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public GamesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Games
        /// <summary>
        /// Get all games.
        /// </summary>
        /// <returns>List of Games</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Games.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Games/Details/5
        /// <summary>
        /// Get one game. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of game to retrieve, Guid</param>
        /// <returns>Game entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _uow.Games.FirstOrDefaultAsync(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        /// <summary>
        /// Get game for create.
        /// </summary>
        /// <returns>View of game</returns>
        public async Task<IActionResult> Create()
        {
            var vm = new GameCreateEditViewModel();
            vm.DevCompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Game.Id), nameof(Game.Name));
            vm.PubCompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Game.Id), nameof(Game.Name));
            return View(vm);
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a game.
        /// </summary>
        /// <param name="vm">Game viewmodel</param>
        /// <returns>View of game</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Games.Add(vm.Game);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.DevCompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Game.DevCompanyId);
            vm.PubCompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Game.PubCompanyId);
            return View(vm);
        }

        // GET: Games/Edit/5
        /// <summary>
        /// Get game by id for delete.
        /// </summary>
        /// <param name="id">Id of game to delete, Guid</param>
        /// <returns>View of game</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _uow.Games.FirstOrDefaultAsync(id.Value);
            if (game == null)
            {
                return NotFound();
            }
            
            var vm = new GameCreateEditViewModel();
            vm.Game = game;
            vm.DevCompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Game.DevCompanyId);
            vm.PubCompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Game.PubCompanyId);
            return View(vm);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a game.
        /// </summary>
        /// <param name="id">Id of game to edit, Guid</param>
        /// <param name="vm">Game viewmodel</param>
        /// <returns>View of game</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameCreateEditViewModel vm)
        {
            if (id != vm.Game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Games.Update(vm.Game);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.DevCompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Game.DevCompanyId);
            vm.PubCompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Game.PubCompanyId);
            return View(vm);
        }

        // GET: Games/Delete/5
        /// <summary>
        /// Get game by id for delete.
        /// </summary>
        /// <param name="id">Id of game to delete, Guid</param>
        /// <returns>View of game</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _uow.Games.FirstOrDefaultAsync(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        /// <summary>
        /// Delete game.
        /// </summary>
        /// <param name="id">Id of game to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Games.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
