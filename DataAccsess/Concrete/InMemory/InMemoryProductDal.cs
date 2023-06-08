using DataAccsess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;//global değişken tanımladık class içinde metod dışında
        public InMemoryProductDal()//void vs döndürmüyor ve direkt class ismiyle bu sebeple conctructor
        {
            _products = new List<Product>
            {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitsInStock=15,UnitPrice=15},
               new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitsInStock=80,UnitPrice=500},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitsInStock=100,UnitPrice=1500},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitsInStock=150,UnitPrice=100},
                new Product{ProductId=5,CategoryId=2,ProductName="Mouse",UnitsInStock=800,UnitPrice=75}
            };

        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {   
            //LINQ -Language Integrated Query
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);//singleOrDefault yukarıda ki foreachin aynısını yapar.
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllCategory(int categoryId)
        {
            return _products.Where(p=>p.CategoryId==categoryId).ToList(); 
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //gönderdiğim ürün id'sine sahip olan listedeki ürünü bul demek
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock= product.UnitsInStock;
        }
    }
}
