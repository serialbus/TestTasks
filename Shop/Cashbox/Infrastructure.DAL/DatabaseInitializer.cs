using Infrastructure.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
            //base.Seed(context);

            context.Products.AddRange(new Product[]
            {
                new Product { Name = "Карандаш", Price = 8.5M, Qauntity = 755 },
                new Product { Name = "Ручка", Price = 14.3M, Qauntity = 346 },
                new Product { Name = "Тетрадь", Price = 30, Qauntity = 1000 }
            });
            context.SaveChanges();
        }
    }
}
