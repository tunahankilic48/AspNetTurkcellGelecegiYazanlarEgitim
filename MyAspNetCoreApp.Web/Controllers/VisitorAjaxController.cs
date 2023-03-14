using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class VisitorAjaxController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public VisitorAjaxController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SaveVisitor(VisitorViewModel visitor)
        {
            visitor.Created = DateTime.Now;
            _context.Visitors.Add(_mapper.Map<Visitor>(visitor));

            _context.SaveChanges();


            TempData["result"] = "Yorum Kaydedilmiştir.";
            return Json(new { IsSuccess = "true" });
        }

        public IActionResult VisitorCommentList()
        {
            var visitors = _context.Visitors.ToList();

            var visitorViewModels = _mapper.Map<List<VisitorViewModel>>(visitors);

            return View(visitorViewModels);
        }
    }
}
