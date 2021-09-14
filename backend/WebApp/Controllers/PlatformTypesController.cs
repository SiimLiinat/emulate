using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for PlatformTypes.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class PlatformTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for PlatformTypes.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public PlatformTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PlatformTypes
        /// <summary>
        /// Get all types of platform.
        /// </summary>
        /// <returns>List of platformTypes</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.PlatformTypes.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: PlatformTypes/Details/5
        /// <summary>
        /// Get one type of platform. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>PlatformType entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformType = await _uow.PlatformTypes.FirstOrDefaultAsync(id.Value);
            if (platformType == null)
            {
                return NotFound();
            }

            return View(platformType);
        }

        // GET: PlatformTypes/Create
        /// <summary>
        /// Get type of platform by id for create.
        /// </summary>
        /// <returns>View of type of platform</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlatformTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a type of platform.
        /// </summary>
        /// <param name="platformType">PlatformType entity</param>
        /// <returns>View of type of platform</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatformType platformType)
        {
            if (ModelState.IsValid)
            {
                _uow.PlatformTypes.Add(platformType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(platformType);
        }

        // GET: PlatformTypes/Edit/5
        /// <summary>
        /// Get type of platform by id for edit.
        /// </summary>
        /// <param name="id">Id of object to edit, Guid</param>
        /// <returns>View of type of platform</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformType = await _uow.PlatformTypes.FirstOrDefaultAsync(id.Value);
            if (platformType == null)
            {
                return NotFound();
            }
            return View(platformType);
        }

        // POST: PlatformTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a type of platform.
        /// </summary>
        /// <param name="id">Id of object to edit, Guid</param>
        /// <param name="platformType">PlatformType entity</param>
        /// <returns>View of type of platform</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PlatformType platformType)
        {
            if (id != platformType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PlatformTypes.Update(platformType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(platformType);
        }

        // GET: PlatformTypes/Delete/5
        /// <summary>
        /// Get type of platform by id for delete.
        /// </summary>
        /// <param name="id">Id of object to delete, Guid</param>
        /// <returns>View of type of platform</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platformType = await _uow.PlatformTypes.FirstOrDefaultAsync(id.Value);
            if (platformType == null)
            {
                return NotFound();
            }

            return View(platformType);
        }

        // POST: PlatformTypes/Delete/5
        /// <summary>
        /// Delete type of platform.
        /// </summary>
        /// <param name="id">Id of type of platform to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PlatformTypes.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
