using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Infrastructure.IRepository;
using BulkyBook.Entities;

namespace BulkyBookWeb.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return _unitOfWork.Category != null ?
                        View(_unitOfWork.Category.GetAll()) :
                        Problem("Entity set 'DatabaseContext.Categories'  is null.");
        }
        // Get by Id
        public async Task<ActionResult<Category>> Details(int id)
        {
            var data = _unitOfWork.Category.GetT(c => c.Id == id);
            if (data != null) return View(data);
            else return Problem($"${id} doesn't exist");
        }
        // POST: Categories
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["notify"] = $"our new category ${category.Id} {category.Name}";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Category.GetT(c => c.Id == id);
            if(category != null)
            {
                _unitOfWork.Category.Delete(category);
                _unitOfWork.Save();
                TempData["notify"] = $"{category.Name} deleted";
            }
            else
            {
                TempData["notify"] = $"Category ${category.Id} not found";
            }
            return RedirectToAction("Index");
        }
        private bool CategoryExists(int id)
        {
          return (_unitOfWork.Category?.Exists(c => c.Id == id)).GetValueOrDefault();
        }
    }
}
