using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.GameOnPlatforms;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for GamesOnPlatform.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class GamesOnPlatformController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for GamesOnPlatform.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public GamesOnPlatformController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: GamesOnPlatform
        /// <summary>
        /// Get all games on platforms.
        /// </summary>
        /// <returns>List of GameOnPlatforms</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.GameOnPlatforms.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: GamesOnPlatform/Details/5
        /// <summary>
        /// Get one game on platform. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of game on platform to retrieve, Guid</param>
        /// <returns>GameOnPlatform entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOnPlatform = await _uow.GameOnPlatforms.FirstOrDefaultAsync(id.Value);
            if (gameOnPlatform == null)
            {
                return NotFound();
            }

            return View(gameOnPlatform);
        }

        // GET: GamesOnPlatform/Create
        /// <summary>
        /// Get game on platform for create.
        /// </summary>
        /// <returns>View of game on platform</returns>
        public async Task<IActionResult> Create()
        {
            var vm = new GameOnPlatformCreateEditViewModel();
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name));
            vm.PlatformSelectList = new SelectList(await _uow.Platforms.GetAllAsync(), nameof(Platform.Id), nameof(Platform.Code));
            return View(vm);
        }

        // POST: GamesOnPlatform/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a game on platform.
        /// </summary>
        /// <param name="vm">GameOnPlatform viewmodel</param>
        /// <returns>View of game on platform</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameOnPlatformCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.GameOnPlatforms.Add(vm.GameOnPlatform);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.GameOnPlatform.GameId);
            vm.PlatformSelectList = new SelectList(await _uow.Platforms.GetAllAsync(), nameof(Platform.Id), nameof(Platform.Code), vm.GameOnPlatform.PlatformId);
            return View(vm);
        }

        // GET: GamesOnPlatform/Edit/5
        /// <summary>
        /// Get game on platform by id for edit.
        /// </summary>
        /// <param name="id">Id of game on platform to edit, Guid</param>
        /// <returns>View of game on platform</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOnPlatform = await _uow.GameOnPlatforms.FirstOrDefaultAsync(id.Value);
            if (gameOnPlatform == null)
            {
                return NotFound();
            }

            var vm = new GameOnPlatformCreateEditViewModel();
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.GameOnPlatform.GameId);
            vm.PlatformSelectList = new SelectList(await _uow.Platforms.GetAllAsync(), nameof(Platform.Id), nameof(Platform.Code), vm.GameOnPlatform.PlatformId);

            return View(vm);
        }

        // POST: GamesOnPlatform/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a game on platform.
        /// </summary>
        /// <param name="id">Id of game on platform to edit, Guid</param>
        /// <param name="vm">GameOnPlatform viewmodel</param>
        /// <returns>View of game on platform</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameOnPlatformCreateEditViewModel vm)
        {
            if (id != vm.GameOnPlatform.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.GameOnPlatforms.Update(vm.GameOnPlatform);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.GameOnPlatform.GameId);
            vm.PlatformSelectList = new SelectList(await _uow.Platforms.GetAllAsync(), nameof(Platform.Id), nameof(Platform.Code), vm.GameOnPlatform.PlatformId);
            return View(vm);
        }

        // GET: GamesOnPlatform/Delete/5
        /// <summary>
        /// Get game on platform by id for delete.
        /// </summary>
        /// <param name="id">Id of game on platform to delete, Guid</param>
        /// <returns>View of game on platform</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameOnPlatform = await _uow.GameOnPlatforms.FirstOrDefaultAsync(id.Value);
            if (gameOnPlatform == null)
            {
                return NotFound();
            }

            return View(gameOnPlatform);
        }

        // POST: GamesOnPlatform/Delete/5
        /// <summary>
        /// Delete game on platform.
        /// </summary>
        /// <param name="id">Id of game on platform to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.GameOnPlatforms.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
