using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Compatibilities;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Compatibilities.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CompatibilitiesController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for Compatibilities.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public CompatibilitiesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Compatibilities
        /// <summary>
        /// Get all compatibilities.
        /// </summary>
        /// <returns>List of Compatibilities</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Compatibilities.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Compatibilities/Details/5
        /// <summary>
        /// Get one compatibility. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Compatibility entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibility = await _uow.Compatibilities.FirstOrDefaultAsync(id.Value);
            if (compatibility == null)
            {
                return NotFound();
            }

            return View(compatibility);
        }

        // GET: Compatibilities/Create
        /// <summary>
        /// Get compatibility for create.
        /// </summary>
        /// <returns>View of compatibility</returns>
        public async Task<IActionResult> Create()
        {
            var vm = new CompatibilityCreateEditViewModel();
            vm.CompatibilityTypeSelectList = new SelectList(await _uow.CompatibilityTypes.GetAllAsync(), nameof(CompatibilityType.Id), nameof(CompatibilityType.Type));
            vm.EmulatorSelectList = new SelectList(await _uow.Emulators.GetAllAsync(), nameof(Domain.App.Emulator.Id), nameof(Domain.App.Emulator.Name));
            vm.GameOnPlatformSelectList = new SelectList(await _uow.GameOnPlatforms.GetAllAsync(), nameof(GameOnPlatform.Id), nameof(GameOnPlatform.Id));
            return View(vm);
        }

        // POST: Compatibilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a compatibility.
        /// </summary>
        /// <param name="vm">Compatibility viewmodel</param>
        /// <returns>View of compatibility</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompatibilityCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Compatibilities.Add(vm.Compatibility);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.CompatibilityTypeSelectList = new SelectList(await _uow.CompatibilityTypes.GetAllAsync(), 
                nameof(CompatibilityType.Id), nameof(CompatibilityType.Type), vm.Compatibility.CompatibilityTypeId);
            vm.EmulatorSelectList = new SelectList(await _uow.Emulators.GetAllAsync(), 
                nameof(Domain.App.Emulator.Id), nameof(Domain.App.Emulator.Name), vm.Compatibility.EmulatorId);
            vm.GameOnPlatformSelectList = new SelectList(await _uow.GameOnPlatforms.GetAllAsync(), 
                nameof(GameOnPlatform.Id), nameof(GameOnPlatform.Id), vm.Compatibility.GameOnPlatformId);
            return View(vm);
        }

        // GET: Compatibilities/Edit/5
        /// <summary>
        /// Get compatibility by id for edit.
        /// </summary>
        /// <param name="id">Id of compatibility to edit, Guid</param>
        /// <returns>View of compatibility</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibility = await _uow.Compatibilities.FirstOrDefaultAsync(id.Value);
            if (compatibility == null)
            {
                return NotFound();
            }
            var vm = new CompatibilityCreateEditViewModel();
            vm.CompatibilityTypeSelectList = new SelectList(await _uow.CompatibilityTypes.GetAllAsync(), 
                nameof(CompatibilityType.Id), nameof(CompatibilityType.Type), vm.Compatibility.CompatibilityTypeId);
            vm.EmulatorSelectList = new SelectList(await _uow.Emulators.GetAllAsync(), 
                nameof(Domain.App.Emulator.Id), nameof(Domain.App.Emulator.Name), vm.Compatibility.EmulatorId);
            vm.GameOnPlatformSelectList = new SelectList(await _uow.GameOnPlatforms.GetAllAsync(), 
                nameof(GameOnPlatform.Id), nameof(GameOnPlatform.Id), vm.Compatibility.GameOnPlatformId);
            return View(vm);
        }

        // POST: Compatibilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a compatibility.
        /// </summary>
        /// <param name="id">Id of object to edit, Guid</param>
        /// <param name="vm">Compatibility viewmodel</param>
        /// <returns>View of compatibility</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CompatibilityCreateEditViewModel vm)
        {
            if (id != vm.Compatibility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Compatibilities.Update(vm.Compatibility);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.CompatibilityTypeSelectList = new SelectList(await _uow.CompatibilityTypes.GetAllAsync(), 
                nameof(CompatibilityType.Id), nameof(CompatibilityType.Type), vm.Compatibility.CompatibilityTypeId);
            vm.EmulatorSelectList = new SelectList(await _uow.Emulators.GetAllAsync(), 
                nameof(Domain.App.Emulator.Id), nameof(Domain.App.Emulator.Name), vm.Compatibility.EmulatorId);
            vm.GameOnPlatformSelectList = new SelectList(await _uow.GameOnPlatforms.GetAllAsync(), 
                nameof(GameOnPlatform.Id), nameof(GameOnPlatform.Id), vm.Compatibility.GameOnPlatformId);
            return View(vm);
        }

        // GET: Compatibilities/Delete/5
        /// <summary>
        /// Get compatibility by id for delete.
        /// </summary>
        /// <param name="id">Id of compatibility to delete, Guid</param>
        /// <returns>View of compatibility</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibility = await _uow.Compatibilities.FirstOrDefaultAsync(id.Value);
            if (compatibility == null)
            {
                return NotFound();
            }

            return View(compatibility);
        }

        // POST: Compatibilities/Delete/5
        /// <summary>
        /// Delete compatibility.
        /// </summary>
        /// <param name="id">Id of object to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Compatibilities.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
