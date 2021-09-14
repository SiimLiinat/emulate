using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller for Roles.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Constructor for RolesController.
        /// </summary>
        /// <param name="uow">Unit of work</param>
        public RolesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Roles
        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>List of AppRoles</returns>
        public async Task<IActionResult> Index()
        {
            var res = await _uow.AppRoles.GetAllAsync();
            return View(res);
        }

        // GET: Roles/Details/5
        /// <summary>
        /// Get one role. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of role to retrieve, Guid</param>
        /// <returns>Role entity from database</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _uow.AppRoles
                .FirstOrDefaultAsync(id.Value);
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // GET: Roles/Create
        /// <summary>
        /// Get role for create.
        /// </summary>
        /// <returns>View of role</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a role.
        /// </summary>
        /// <param name="appRole">AppRole entity</param>
        /// <returns>View of role</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                _uow.AppRoles.Add(appRole);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appRole);
        }

        // GET: Roles/Edit/5
        /// <summary>
        /// Get role by id for edit.
        /// </summary>
        /// <param name="id">Id of role to edit, Guid</param>
        /// <returns>View of role</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _uow.AppRoles.FirstOrDefaultAsync(id.Value);
            if (appRole == null)
            {
                return NotFound();
            }
            return View(appRole);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a role.
        /// </summary>
        /// <param name="id">Id of role to edit, Guid</param>
        /// <param name="appRole">AppRole entity</param>
        /// <returns>View of role</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.AppRoles.Update(appRole);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appRole);
        }

        // GET: Roles/Delete/5
        /// <summary>
        /// Get role by id for delete.
        /// </summary>
        /// <param name="id">Id of role to delete, Guid</param>
        /// <returns>View of role</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _uow.AppRoles.FirstOrDefaultAsync(id.Value);
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // POST: Roles/Delete/5
        /// <summary>
        /// Delete role.
        /// </summary>
        /// <param name="id">Id of role to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.AppRoles.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
