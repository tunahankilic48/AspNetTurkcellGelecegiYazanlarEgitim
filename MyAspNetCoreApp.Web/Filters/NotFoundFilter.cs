﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Filters
{
    public class NotFoundFilter:LogFilter
    {
        private readonly AppDbContext _context;
        public NotFoundFilter(AppDbContext context)
        {
            _context = context;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idValue = context.ActionArguments.Values.First();
            var id = (int)idValue;

            var hasProduct = _context.Products.Any(x=>x.Id == id);

            if (hasProduct == false)
            {
                context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel() { Errors = new List<string>() { $"{id} numaralı Id'ye Sahip Ürün veritabanında bulunamamıştır."} });
            }
        }
    }
}
