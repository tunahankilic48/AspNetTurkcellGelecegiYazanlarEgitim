

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductUpdateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz!")]
        [StringLength(30,ErrorMessage ="İsim alanına en fazla 50 karakter girilebilir.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz!")]
        [StringLength(300, MinimumLength = 20, ErrorMessage = "Açıklama alanına minimum 20 maksimum 300 karakter girilebilir.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Fiyat alanı boş bırakılamaz!")]
        [RegularExpression(@"^(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$", ErrorMessage ="###.## formatın giriş yapınız.")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Stok alanı boş bırakılamaz!")]
        [Range(1,200, ErrorMessage ="Stok Miktarı 0-200 arasında bir değer olmalıdır.")]
        public int? Stock { get; set; }
        [Required(ErrorMessage = "Renk alanı boş bırakılamaz!")]
        public string Color { get; set; }

        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Yayınlanma süresi alanı boş bırakılamaz!")]
        public int? Expire { get; set; }
        [Required(ErrorMessage = "Yayın tarihi alanı boş bırakılamaz!")]
        public DateTime? PublishDate { get; set; }
        [ValidateNever]
        public IFormFile? Image { get; set; }
        [ValidateNever]
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }


    }
}
