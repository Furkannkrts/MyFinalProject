 using Business.Concrete;
using Core.Utilities.Results;
using DataAccsess.Concrete.EntityFramework;
using DataAccsess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID
    //O->Open Closed Prencible//yeni bir özellik ekliyorsam mevcut kodum değişmemeli.
    //Entity Framework->ORM--Object relational mapping
    //override->üzerine yazmak
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            //Ioc
           // CategoryTest();

        }
        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());//category  manager burda ıcategorydal istiyor
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);

            }
        }
        
        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var result = productManager.GetProductDetails();
            if(result.Success==true) 
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else 
            {
                Console.WriteLine(result.Message);
            }

        }
    }
}
