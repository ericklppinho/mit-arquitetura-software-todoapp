using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;
using ToDo.Web.Mvc.Models;

namespace ToDo.Web.Mvc.Controllers
{

    public class ItemController : Controller
    {

        protected IItemRepository repository;

        public ItemController(IItemRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var items = await repository.GetAllAsync();

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Description")] CreateItemModel createItemModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item(createItemModel.Description);

                await repository.AddAsync(item);

                return RedirectToAction(nameof(Index));
            }

            return View(createItemModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item item = await repository.GetAsync(Guid.Parse(id));

            if (item == null)
            {
                return NotFound();
            }

            UpdateItemModel updateItemModel = new UpdateItemModel();
            updateItemModel.Description = item.Description;
            updateItemModel.Done = item.Done;

            return View(updateItemModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string? id, [Bind("Description", "Done")] UpdateItemModel updateItemModel)
        {

            if (id != null && ModelState.IsValid)
            {
                var item = new Item(Guid.Parse(id), updateItemModel.Description, updateItemModel.Done);

                await repository.UpdateAsync(item);


                return RedirectToAction(nameof(Index));
            }

            return View(updateItemModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Guid Id = Guid.Parse(id);

            Item item = await repository.GetAsync(Id);

            if (item == null)
            {
                return NotFound();
            }

            await repository.DeleteAsync(Id);

            return RedirectToAction(nameof(Index));
        }

    }

}

