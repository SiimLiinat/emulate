using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Platforms;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Platforms.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class PlatformsController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for Platforms.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public PlatformsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Platforms
        /// <summary>
        /// Get all platforms.
        /// </summary>
        /// <returns>List of Platforms</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Platforms.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Platforms/Details/5
        /// <summary>
        /// Get one platform. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of platform to retrieve, Guid</param>
        /// <returns>Platform entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _uow.Platforms.FirstOrDefaultAsync(id.Value);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // GET: Platforms/Create
        /// <summary>
        /// Get platform for create.
        /// </summary>
        /// <returns>View of platform</returns>
        public async Task<IActionResult> Create()
        {
            var vm = new PlatformCreateEditViewModel();
            vm.CompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Company.Id), nameof(Company.Name));
            vm.PlatformTypeSelectList = new SelectList(await _uow.PlatformTypes.GetAllAsync(), nameof(PlatformType.Id), nameof(PlatformType.Type));
            return View(vm);
        }

        // POST: Platforms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a platform.
        /// </summary>
        /// <param name="vm">Platform viewmodel</param>
        /// <returns>View of platform</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatformCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Platforms.Add(vm.Platform);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.CompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Company.Id), nameof(Company.Name), vm.Platform.CompanyId);
            vm.PlatformTypeSelectList = new SelectList(await _uow.PlatformTypes.GetAllAsync(), nameof(PlatformType.Id), nameof(PlatformType.Type), vm.Platform.PlatformTypeId);
            return View(vm);
        }

        // GET: Platforms/Edit/5
        /// <summary>
        /// Get platform by id for edit.
        /// </summary>
        /// <param name="id">Id of platform to edit, Guid</param>
        /// <returns>View of platform</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _uow.Platforms.FirstOrDefaultAsync(id.Value);
            if (platform == null)
            {
                return NotFound();
            }

            var vm = new PlatformCreateEditViewModel();
            vm.CompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Company.Id), nameof(Company.Name), vm.Platform.CompanyId);
            vm.PlatformTypeSelectList = new SelectList(await _uow.PlatformTypes.GetAllAsync(), nameof(PlatformType.Id), nameof(PlatformType.Type), vm.Platform.PlatformTypeId);
            return View(vm);
        }

        // POST: Platforms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a platform.
        /// </summary>
        /// <param name="id">Id of platform to edit, Guid</param>
        /// <param name="vm">Platform viewmodel</param>
        /// <returns>View of platform</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PlatformCreateEditViewModel vm)
        {
            if (id != vm.Platform.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Platforms.Update(vm.Platform);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.CompanySelectList = new SelectList(await _uow.Companies.GetAllAsync(), nameof(Company.Id), nameof(Company.Name), vm.Platform.CompanyId);
            vm.PlatformTypeSelectList = new SelectList(await _uow.PlatformTypes.GetAllAsync(), nameof(PlatformType.Id), nameof(PlatformType.Type), vm.Platform.PlatformTypeId);
            return View(vm);
        }

        // GET: Platforms/Delete/5
        /// <summary>
        /// Get platform for delete.
        /// </summary>
        /// <param name="id">Id of platform to delete, Guid</param>
        /// <returns>View of company</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _uow.Platforms.FirstOrDefaultAsync(id.Value);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // POST: Platforms/Delete/5
        /// <summary>
        /// Delete platform.
        /// </summary>
        /// <param name="id">Id of platform to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Platforms.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
