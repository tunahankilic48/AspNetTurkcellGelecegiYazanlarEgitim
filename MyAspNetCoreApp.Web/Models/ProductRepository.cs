namespace MyAspNetCoreApp.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>()
        {
            new() { Id = 1, Name = "Kalem 1", Price = 100, Stock = 200 },
            new () { Id = 2, Name = "Kalem 2", Price = 200, Stock = 300 },
            new () { Id = 3, Name = "Kalem 3", Price = 300, Stock = 400 }
        };

        public List<Product> GetAll() => _products;
        public void Add(Product newProduct) => _products.Add(newProduct);
        public void Remove(int id)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == id);
            if (hasProduct == null)
            {
                throw new Exception($"Bu Id({id}'ye sahip ürün bulunmamaktadır.");
            }
            _products.Remove(hasProduct);
        }
        public void Uptade(Product uptadeProduct)
        {
            var hasProduct = _products.FirstOrDefault(x => x.Id == uptadeProduct.Id);
            if (hasProduct == null)
            {
                throw new Exception($"Bu Id({uptadeProduct.Id}'ye sahip ürün bulunmamaktadır.");
            }

            hasProduct.Name = uptadeProduct.Name;
            hasProduct.Price = uptadeProduct.Price;
            hasProduct.Stock = uptadeProduct.Stock;
            var index = _products.FindIndex(x => x.Id == uptadeProduct.Id);

            _products[index] = hasProduct;

        }
    }
}
