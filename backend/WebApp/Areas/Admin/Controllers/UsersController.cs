using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller for Users.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Constructor for UsersController.
        /// </summary>
        /// <param name="uow">Unit of work</param>
        public UsersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Users
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of AppUsers</returns>
        public async Task<IActionResult> Index()
        {
            var res = await _uow.AppUsers.GetAllAsync();
            return View(res);
        }

        // GET: Users/Details/5
        /// <summary>
        /// Get one user. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of user to retrieve, Guid</param>
        /// <returns>User entity from database</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _uow.AppUsers
                .FirstOrDefaultAsync(id.Value);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: Users/Create
        /// <summary>
        /// Get user for create.
        /// </summary>
        /// <returns>View of user</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a user.
        /// </summary>
        /// <param name="appUser">AppUser entity</param>
        /// <returns>View of user</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                _uow.AppUsers.Add(appUser);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: Users/Edit/5
        /// <summary>
        /// Get user by id for edit.
        /// </summary>
        /// <param name="id">Id of user to edit, Guid</param>
        /// <returns>View of user</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _uow.AppUsers.FirstOrDefaultAsync(id.Value);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a user.
        /// </summary>
        /// <param name="id">Id of user to edit, Guid</param>
        /// <param name="appUser">AppUser entity</param>
        /// <returns>View of user</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.AppUsers.Update(appUser);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: Users/Delete/5
        /// <summary>
        /// Get user by id for delete.
        /// </summary>
        /// <param name="id">Id of user to delete, Guid</param>
        /// <returns>View of user</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _uow.AppUsers.FirstOrDefaultAsync(id.Value);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: Users/Delete/5
        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">Id of user to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.AppUsers.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
