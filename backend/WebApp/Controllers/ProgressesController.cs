using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.DTO;
using Extensions.Base;
using WebApp.ViewModels.Progresses;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Progresses.
    /// </summary>
    public class ProgressesController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for Progresses.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public ProgressesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Progresses
        /// <summary>
        /// Get all progresses.
        /// </summary>
        /// <returns>List of Progresses</returns>
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Progresses.GetAllAsync(User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Progresses/Details/5
        /// <summary>
        /// Get one progress. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of progress to retrieve, Guid</param>
        /// <returns>Progress entity from database</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _uow.Progresses.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        // GET: Progresses/Create
        /// <summary>
        /// Get progress for create.
        /// </summary>
        /// <returns>View of progress</returns>
        public async Task<IActionResult> Create()
        {
            var vm = new ProgressCreateEditViewModel();
            vm.CompatibilityTypeSelectList = new SelectList(await _uow.CompatibilityTypes.GetAllAsync(), 
                nameof(CompatibilityType.Id), nameof(CompatibilityType.Type));
            vm.ConfigurationSelectList = new SelectList(await _uow.Configurations.GetAllAsync(), 
                nameof(Configuration.Id), nameof(Configuration.ConfigurationString));
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), 
                nameof(Game.Id), nameof(Game.Name));
            return View(vm);
        }

        // POST: Progresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a progress.
        /// </summary>
        /// <param name="vm">Progress viewmodel</param>
        /// <returns>View of progress</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgressCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Progresses.Add(vm.Progress);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.CompatibilityTypeSelectList = new SelectList(await _uow.CompatibilityTypes.GetAllAsync(), 
                nameof(CompatibilityType.Id), nameof(CompatibilityType.Type), vm.Progress.CompatibilityTypeId);
            vm.ConfigurationSelectList = new SelectList(await _uow.Configurations.GetAllAsync(), 
                nameof(Configuration.Id), nameof(Configuration.ConfigurationString), vm.Progress.ConfigurationId);
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), 
                nameof(Game.Id), nameof(Game.Name), vm.Progress.GameId);
            return View(vm);
        }

        // GET: Progresses/Edit/5
        /// <summary>
        /// Get progress by id for edit.
        /// </summary>
        /// <param name="id">Id of progress to edit, Guid</param>
        /// <returns>View of progress</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _uow.Progresses.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (progress == null)
            {
                return NotFound();
            }
            var vm = new ProgressCreateEditViewModel();
            vm.CompatibilityTypeSelectList = new SelectList(await _uow.CompatibilityTypes.GetAllAsync(), 
                nameof(CompatibilityType.Id), nameof(CompatibilityType.Type), vm.Progress.CompatibilityTypeId);
            vm.ConfigurationSelectList = new SelectList(await _uow.Configurations.GetAllAsync(), 
                nameof(Configuration.Id), nameof(Configuration.ConfigurationString), vm.Progress.ConfigurationId);
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), 
                nameof(Game.Id), nameof(Game.Name), vm.Progress.GameId);
            return View(vm);
        }

        // POST: Progresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a progress.
        /// </summary>
        /// <param name="id">Id of progress to edit, Guid</param>
        /// <param name="vm">Progress viewmodel</param>
        /// <returns>View of progress</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProgressCreateEditViewModel vm)
        {
            if (id != vm.Progress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Progresses.Update(vm.Progress);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.CompatibilityTypeSelectList = new SelectList(await _uow.CompatibilityTypes.GetAllAsync(), 
                nameof(CompatibilityType.Id), nameof(CompatibilityType.Type), vm.Progress.CompatibilityTypeId);
            vm.ConfigurationSelectList = new SelectList(await _uow.Configurations.GetAllAsync(), 
                nameof(Configuration.Id), nameof(Configuration.ConfigurationString), vm.Progress.ConfigurationId);
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), 
                nameof(Game.Id), nameof(Game.Name), vm.Progress.GameId);
            return View(vm);
        }

        // GET: Progresses/Delete/5
        /// <summary>
        /// Get progress by id for delete.
        /// </summary>
        /// <param name="id">Id of progress to delete, Guid</param>
        /// <returns>View of progress</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _uow.Progresses.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        // POST: Progresses/Delete/5
        /// <summary>
        /// Delete progress.
        /// </summary>
        /// <param name="id">Id of progress to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Progresses.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
