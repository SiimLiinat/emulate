using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for MediaTypes.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class MediaTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for MediaTypes.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public MediaTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: MediaTypes
        /// <summary>
        /// Get all types of media.
        /// </summary>
        /// <returns>List of MediaTypes</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.MediaTypes.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: MediaTypes/Details/5
        /// <summary>
        /// Get one type of media. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>MediaType entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _uow.MediaTypes.FirstOrDefaultAsync(id.Value);
            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // GET: MediaTypes/Create
        /// <summary>
        /// Get type of media by id for create.
        /// </summary>
        /// <returns>View of type of media</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: MediaTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a type of media.
        /// </summary>
        /// <param name="mediaType">MediaType entity</param>
        /// <returns>View of type of media</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                _uow.MediaTypes.Add(mediaType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaType);
        }

        // GET: MediaTypes/Edit/5
        /// <summary>
        /// Get type of media by id for edit.
        /// </summary>
        /// <param name="id">Id of object to edit, Guid</param>
        /// <returns>View of type of media</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _uow.MediaTypes.FirstOrDefaultAsync(id.Value);
            if (mediaType == null)
            {
                return NotFound();
            }
            return View(mediaType);
        }

        // POST: MediaTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a type of media.
        /// </summary>
        /// <param name="id">Id of object to edit, Guid</param>
        /// <param name="mediaType">MediaType entity</param>
        /// <returns>View of type of media</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MediaType mediaType)
        {
            if (id != mediaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.MediaTypes.Update(mediaType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaType);
        }

        // GET: MediaTypes/Delete/5
        /// <summary>
        /// Get type of media by id for delete.
        /// </summary>
        /// <param name="id">Id of object to delete, Guid</param>
        /// <returns>View of type of media</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _uow.MediaTypes.FirstOrDefaultAsync(id.Value);
            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // POST: MediaTypes/Delete/5
        /// <summary>
        /// Delete type of media.
        /// </summary>
        /// <param name="id">Id of type of media to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.MediaTypes.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
