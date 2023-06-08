using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Concrete.EntityFramework
{
    //db tabloları ile proje classlarını bağlamak
    public class NorthwindContext:DbContext
    {
        //bu metot benim projem hangi veri tabanıyla ilişkili onu belirtiğimiz yer
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
        }
        //nesneleri bağlama 
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaim { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Order> Orders { get; set; }//ilişkilendirdik


    }
  
}
