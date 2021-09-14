using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.DTO;
using Extensions.Base;
using WebApp.ViewModels.Medias;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Medias.
    /// </summary>
    public class MediasController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for Medias.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public MediasController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Medias
        /// <summary>
        /// Get all medias.
        /// </summary>
        /// <returns>List of Medias</returns>
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Medias.GetAllAsync(User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Medias/Details/5
        /// <summary>
        /// Get one media. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of media to retrieve, Guid</param>
        /// <returns>Media entity from database</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _uow.Medias.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // GET: Medias/Create
        /// <summary>
        /// Get media for create.
        /// </summary>
        /// <returns>View of media</returns>
        public async Task<IActionResult> Create()
        {
            var vm = new MediaCreateEditViewModel();
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name));
            vm.MediaTypeSelectList = new SelectList(await _uow.MediaTypes.GetAllAsync(), nameof(MediaType.Id), nameof(MediaType.Type));
            return View(vm);
        }

        // POST: Medias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a media.
        /// </summary>
        /// <param name="vm">Media viewmodel</param>
        /// <returns>View of media</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MediaCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Medias.Add(vm.Media);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Media.GameId);
            vm.MediaTypeSelectList = new SelectList(await _uow.MediaTypes.GetAllAsync(), nameof(MediaType.Id), nameof(MediaType.Type), vm.Media.MediaTypeId);
            return View(vm);
        }

        // GET: Medias/Edit/5
        /// <summary>
        /// Get media by id for edit.
        /// </summary>
        /// <param name="id">Id of media to edit, Guid</param>
        /// <returns>View of media</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _uow.Medias.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (media == null)
            {
                return NotFound();
            }

            var vm = new MediaCreateEditViewModel();
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Media.GameId);
            vm.MediaTypeSelectList = new SelectList(await _uow.MediaTypes.GetAllAsync(), nameof(MediaType.Id), nameof(MediaType.Type), vm.Media.MediaTypeId);
            return View(vm);
        }

        // POST: Medias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a media.
        /// </summary>
        /// <param name="id">Id of media to edit, Guid</param>
        /// <param name="vm">Media viewmodel</param>
        /// <returns>View of media</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MediaCreateEditViewModel vm)
        {
            if (id != vm.Media.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Medias.Update(vm.Media);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.GameSelectList = new SelectList(await _uow.Games.GetAllAsync(), nameof(Game.Id), nameof(Game.Name), vm.Media.GameId);
            vm.MediaTypeSelectList = new SelectList(await _uow.MediaTypes.GetAllAsync(), nameof(MediaType.Id), nameof(MediaType.Type), vm.Media.MediaTypeId);

            return View(vm);
        }

        // GET: Medias/Delete/5
        /// <summary>
        /// Get media by id for delete.
        /// </summary>
        /// <param name="id">Id of media to delete, Guid</param>
        /// <returns>View of media</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _uow.Medias.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Medias/Delete/5
        /// <summary>
        /// Delete media.
        /// </summary>
        /// <param name="id">Id of media to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Medias.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
