using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for CompatibilityTypes.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CompatibilityTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for CompatibilityTypes.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public CompatibilityTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CompatibilityTypes
        /// <summary>
        /// Get all types of compatibility.
        /// </summary>
        /// <returns>List of CompatibilityTypes</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.CompatibilityTypes.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: CompatibilityTypes/Details/5
        /// <summary>
        /// Get one type of compatibility. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>CompatibilityType entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibilityType = await _uow.CompatibilityTypes.FirstOrDefaultAsync(id.Value);
            if (compatibilityType == null)
            {
                return NotFound();
            }

            return View(compatibilityType);
        }

        // GET: CompatibilityTypes/Create
        /// <summary>
        /// Get type of compatibility by id for create.
        /// </summary>
        /// <returns>View of type of compatibility</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompatibilityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a type of compatibility.
        /// </summary>
        /// <param name="compatibilityType">CompatibilityType entity</param>
        /// <returns>View of type of compatibility</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompatibilityType compatibilityType)
        {
            if (ModelState.IsValid)
            {
                _uow.CompatibilityTypes.Add(compatibilityType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compatibilityType);
        }

        // GET: CompatibilityTypes/Edit/5
        /// <summary>
        /// Get type of compatibility by id for edit.
        /// </summary>
        /// <param name="id">Id of object to edit, Guid</param>
        /// <returns>View of type of compatibility</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibilityType = await _uow.CompatibilityTypes.FirstOrDefaultAsync(id.Value);
            if (compatibilityType == null)
            {
                return NotFound();
            }
            return View(compatibilityType);
        }

        // POST: CompatibilityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a type of compatibility.
        /// </summary>
        /// <param name="id">Id of object to edit, Guid</param>
        /// <param name="compatibilityType">CompatibilityType entity</param>
        /// <returns>View of type of compatibility</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CompatibilityType compatibilityType)
        {
            if (id != compatibilityType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.CompatibilityTypes.Update(compatibilityType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compatibilityType);
        }

        // GET: CompatibilityTypes/Delete/5
        /// <summary>
        /// Get type of compatibility by id for delete.
        /// </summary>
        /// <param name="id">Id of object to delete, Guid</param>
        /// <returns>View of type of compatibility</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compatibilityType = await _uow.CompatibilityTypes.FirstOrDefaultAsync(id.Value);
            if (compatibilityType == null)
            {
                return NotFound();
            }

            return View(compatibilityType);
        }

        // POST: CompatibilityTypes/Delete/5
        /// <summary>
        /// Delete type of compatibility.
        /// </summary>
        /// <param name="id">Id of type of compatibility to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.CompatibilityTypes.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
