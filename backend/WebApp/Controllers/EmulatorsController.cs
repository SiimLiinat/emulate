using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Emulators;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Emulators.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class EmulatorsController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for Emulators.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public EmulatorsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Emulators
        /// <summary>
        /// Get all emulators.
        /// </summary>
        /// <returns>List of Emulators</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Emulators.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Emulators/Details/5
        /// <summary>
        /// Get one emulator. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Company entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emulator = await _uow.Emulators.FirstOrDefaultAsync(id.Value);
            if (emulator == null)
            {
                return NotFound();
            }

            return View(emulator);
        }

        // GET: Emulators/Create
        /// <summary>
        /// Get emulator by id for create.
        /// </summary>
        /// <returns>View of emulator</returns>
        public async Task<IActionResult> Create()
        {
            var vm = new EmulatorCreateEditViewModel();
            vm.PlatformSelectList = new SelectList(await _uow.Platforms.GetAllAsync(), 
                nameof(Platform.Id), nameof(Platform.Code));
            return View(vm);
        }

        // POST: Emulators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a company.
        /// </summary>
        /// <param name="vm">Emulator viewmodel</param>
        /// <returns>View of emulator</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmulatorCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Emulators.Add(vm.Emulator);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.PlatformSelectList = new SelectList(await _uow.Platforms.GetAllAsync(), 
                nameof(Platform.Id), nameof(Platform.Code), vm.Emulator.PlatformId);
            return View(vm);
        }

        // GET: Emulators/Edit/5
        /// <summary>
        /// Get emulator by id for edit.
        /// </summary>
        /// <param name="id">Id of emulator to edit, Guid</param>
        /// <returns>View of emulator</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emulator = await _uow.Emulators.FirstOrDefaultAsync(id.Value);
            if (emulator == null)
            {
                return NotFound();
            }
            var vm = new EmulatorCreateEditViewModel();
            vm.PlatformSelectList = new SelectList(await _uow.Platforms.GetAllAsync(), 
                nameof(Platform.Id), nameof(Platform.Code), vm.Emulator.PlatformId);
            return View(vm);
        }

        // POST: Emulators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit an emulator.
        /// </summary>
        /// <param name="id">Id of emulator to edit, Guid</param>
        /// <param name="vm">Emulator viewmodel</param>
        /// <returns>View of emulator</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EmulatorCreateEditViewModel vm)
        {
            if (id != vm.Emulator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Emulators.Update(vm.Emulator);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.PlatformSelectList = new SelectList(await _uow.Platforms.GetAllAsync(), 
                nameof(Platform.Id), nameof(Platform.Code), vm.Emulator.PlatformId);
            return View(vm);
        }

        // GET: Emulators/Delete/5
        /// <summary>
        /// Get emulator by id for delete.
        /// </summary>
        /// <param name="id">Id of object to delete, Guid</param>
        /// <returns>View of emulator</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emulator = await _uow.Emulators.FirstOrDefaultAsync(id.Value);
            if (emulator == null)
            {
                return NotFound();
            }

            return View(emulator);
        }

        // POST: Emulators/Delete/5
        /// <summary>
        /// Delete an emulator.
        /// </summary>
        /// <param name="id">Id of object to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Emulators.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
