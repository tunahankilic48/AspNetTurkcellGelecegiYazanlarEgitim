using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{

    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _productRepository = new ProductRepository();
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;

        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(x=>x.Category).Select(x=> new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                CategoryName = x.Category.Name,
                Stock = x.Stock,
                Color = x.Color,
                Expire =    x.Expire,
                ImagePath = x.ImagePath,
                IsActive = x.IsActive,
                PublishDate = x.PublishDate,
            }).ToList();


            //var products = _context.Products.ToList();
            return View(_mapper.Map<List<ProductViewModel>>(products));
        }
        [Route("[controller]/[action]/{page}/{pagesize}")]
        public IActionResult Pages(int page, int pageSize)
        {


            var products = _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("[controller]/[action]/{id}", Name = "product")]
        public IActionResult GetById(int id)
        {
            var product = _context.Products.Find(id);
            return View(_mapper.Map<ProductViewModel>(product));
        }
        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla silindi";
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1},
                {"3 Ay",3},
                {"6 Ay",6},
                {"12 Ay",12},
            };

            ViewBag.Categories = new SelectList(_context.Category, "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(newProduct);
                if (newProduct.Image != null && newProduct.Image.Length > 0)
                {
                    var root = _fileProvider.GetDirectoryContents("wwwroot");
                    var images = root.First(x => x.Name == "Images");

                    var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);

                    var path = Path.Combine(images.PhysicalPath, randomImageName);


                    using var stream = new FileStream(path, FileMode.Create);
                    newProduct.Image.CopyTo(stream);
                    product.ImagePath = randomImageName;

                }



                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["status"] = "Ürün başarıyla eklendi";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1},
                {"3 Ay",3},
                {"6 Ay",6},
                {"12 Ay",12},
            };
                ViewBag.Categories = new SelectList(_context.Category, "Id", "Name");
                return View();
            }
        }
        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Update(int id)
        {
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1},
                {"3 Ay",3},
                {"6 Ay",6},
                {"12 Ay",12},
            };
            var product = _context.Products.Find(id);
            ViewBag.Categories = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(_mapper.Map<ProductUpdateViewModel>(product));
        }
        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel updatedProduct)
        {
            if (ModelState.IsValid)
            {


                if (updatedProduct.Image != null && updatedProduct.Image.Length > 0)
                {
                    var root = _fileProvider.GetDirectoryContents("wwwroot");
                    var images = root.First(x => x.Name == "Images");

                    var randomImageName = Guid.NewGuid() + Path.GetExtension(updatedProduct.Image.FileName);

                    var path = Path.Combine(images.PhysicalPath, randomImageName);


                    using var stream = new FileStream(path, FileMode.Create);
                    updatedProduct.Image.CopyTo(stream);
                    updatedProduct.ImagePath = randomImageName;

                }
                _context.Products.Update(_mapper.Map<Product>(updatedProduct));
                _context.SaveChanges();
                TempData["status"] = "Ürün başarıyla güncellendi";
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 Ay",1},
                {"3 Ay",3},
                {"6 Ay",6},
                {"12 Ay",12},
            };
                ViewBag.Categories = new SelectList(_context.Category, "Id", "Name", updatedProduct.CategoryId);
                return View(updatedProduct);
            }

        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());

            if (anyProduct)
            {
                return Json("Ürün veri tabanınında bulunmaktadır");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
