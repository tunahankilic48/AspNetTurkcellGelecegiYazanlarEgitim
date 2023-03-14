using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;
using System.Diagnostics;

namespace MyAspNetCoreApp.Web.Controllers
{
    [LogFilter]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var productsPartial = _mapper.Map<List<ProductPartialViewModel>>(products);
            ProductListPartialViewModel productListPartial = new ProductListPartialViewModel();
            foreach (var product in productsPartial)
            {
                productListPartial.Products.Add(product);
            }
            return View(productListPartial);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Visitor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveVisitor(VisitorViewModel visitor)
        {
            visitor.Created = DateTime.Now;
            _context.Visitors.Add(_mapper.Map<Visitor>(visitor));
            
            _context.SaveChanges();
            TempData["result"] = "Yorum Kaydedilmiştir.";
            return RedirectToAction("Visitor");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(errorViewModel);
        }
    }
}