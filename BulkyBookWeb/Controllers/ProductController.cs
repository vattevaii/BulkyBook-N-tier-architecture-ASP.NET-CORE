using BulkyBook.DataAccess.Infrastructure.IRepository;
using BulkyBook.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class ProductController: Controller
    {
        private IUnitOfWork _work;
        private readonly IWebHostEnvironment _env;
        public ProductController(IUnitOfWork work, IWebHostEnvironment env)
        {
            _work = work;
            _env = env;
        }

        public IActionResult Index()
        {
            var Products = _work.Product.GetAll();
            return View(Products);
        }
        public IActionResult GetAll() {
            var Products = new List<ProductAddModel>() {
                new ProductAddModel()
                {
                    Name="Car",Category="Vechile",Description="This car is best for insulation.",DiscountedPrice=133000,MarkedPrice=150000,Stock=0
                },
                new ProductAddModel()
                {
                    Name="Shampoo",Category="Makeup",Description="Dandruff free shampoo",DiscountedPrice=300,MarkedPrice=350,Stock=100
                }
            };
            return View(Products);
        }
        public IActionResult Create()
        {
            return View(_work.Category.GetAll());
        }

        public async Task<IActionResult> Image()

        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Image([FromForm]IFormFile? file)
        {
            String? fileName = null;
            Image? image = null;
            if (file != null)
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                String path = Path.Combine(_env.WebRootPath, "images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                image = new Image()
                {
                    imageLoc = Path.Combine("images", file.FileName),
                };
                _work.Images.Add(image);
                _work.Save();
                TempData["notify"] = "Image Created";
                return RedirectToAction("Index");
            }
            TempData["notify"] = "Image Null";
            return View();
        }
        [HttpPost]
            public async Task<IActionResult> Create(ProductAddModel productAddModel, IFormFile? file)
            {
                String? fileName = null;
                Image? image = null;
                if (file != null)
                {
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                    String path = Path.Combine(_env.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    image = new Image()
                    {
                        imageLoc = Path.Combine("images", file.FileName),
                    };
                    _work.Images.Add(image);
                    _work.Save();
                }
                Category category = _work.Category.GetT(c => c.Name == productAddModel.Category);
            Product product = new Product()
            {
                CategoryId = category.Id,
                Name = productAddModel.Name,
                Description = productAddModel.Description,
                DiscountedPrice = productAddModel.DiscountedPrice,
                MarkedPrice = productAddModel.MarkedPrice,
                Stock = productAddModel.Stock,
                //ImageId = image?.Id
            };
            _work.Product.Add(product);
            _work.Save();
            return RedirectToAction("GetAll");
        }
    }
}
