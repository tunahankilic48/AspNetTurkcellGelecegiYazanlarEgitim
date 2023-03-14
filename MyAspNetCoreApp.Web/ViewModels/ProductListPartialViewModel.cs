namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductListPartialViewModel
    {
        public ProductListPartialViewModel()
        {
            Products = new List<ProductPartialViewModel>();
        }
        public List<ProductPartialViewModel> Products{ get; set; } 
    }
}
