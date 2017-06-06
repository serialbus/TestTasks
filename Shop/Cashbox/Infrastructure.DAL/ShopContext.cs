using Infrastructure.Common.Models;
using Infrastructure.Common.Models.DAL;
using Infrastructure.Common.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    [DbConfigurationType(typeof(DbContextConfiguration))]
    [Export(typeof(IRepository))]
    public class ShopContext : DbContext, IRepository
    {
        #region Constructors

        public ShopContext(): base("DbConnection")
        {
        }

        #endregion

        #region Fields And Properties

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPosition> OrderPositions { get; set; }


        IEnumerable<Product> IRepository.Products => Products;
        IEnumerable<Order> IRepository.Orders => Orders.Include(p => p.OrderPositions);

        #endregion

        #region Methods
        public Order CreateOrder()
        {
            var order = new Order();
            Orders.Add(order);
            SaveChanges();
            return order;
        }
        public Product CreateProduct()
        {
            var product = new Product();
            Products.Add(product);
            SaveChanges();
            return product;
        }
        public void DeleteProduct(Product product)
        {
            Products.Remove(product);
            SaveChanges();
        }

        void IRepository.SaveChanges()
        {
            SaveChanges();
        }

        #endregion
    }
}
