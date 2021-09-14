using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using DAL.App.DTO;
using Extensions.Base;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Configurations.
    /// </summary>
    public class ConfigurationsController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for Configurations.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public ConfigurationsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Configurations
        /// <summary>
        /// Get all configurations.
        /// </summary>
        /// <returns>List of Configurations</returns>
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Configurations.GetAllAsync(User.GetUserId()!.Value);
            return View(res);
        }

        // GET: Configurations/Details/5
        /// <summary>
        /// Get one configuration. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Configuration entity from database</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _uow.Configurations.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // GET: Configurations/Create
        /// <summary>
        /// Get type of configuration by id for create.
        /// </summary>
        /// <returns>View of configuration</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configurations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a configuration.
        /// </summary>
        /// <param name="configuration">Configuration entity</param>
        /// <returns>View of configuration</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                configuration.AppUserId = User.GetUserId()!.Value;
                _uow.Configurations.Add(configuration);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuration);
        }

        // GET: Configurations/Edit/5
        /// <summary>
        /// Get type of configuration by id for edit.
        /// </summary>
        /// <param name="id">Id of configuration to edit, Guid</param>
        /// <returns>View of configuration</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _uow.Configurations.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (configuration == null)
            {
                return NotFound();
            }
            return View(configuration);
        }

        // POST: Configurations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a configuration.
        /// </summary>
        /// <param name="id">Id of object to edit, Guid</param>
        /// <param name="configuration">Configuration entity</param>
        /// <returns>View of configuration</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Configuration configuration)
        {
            if (id != configuration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Configurations.Update(configuration);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuration);
        }

        // GET: Configurations/Delete/5
        /// <summary>
        /// Get type of configuration by id for delete.
        /// </summary>
        /// <param name="id">Id of object to delete, Guid</param>
        /// <returns>View of configuration</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuration = await _uow.Configurations.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (configuration == null)
            {
                return NotFound();
            }

            return View(configuration);
        }

        // POST: Configurations/Delete/5
        /// <summary>
        /// Delete configuration.
        /// </summary>
        /// <param name="id">Id of object to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Configurations.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
