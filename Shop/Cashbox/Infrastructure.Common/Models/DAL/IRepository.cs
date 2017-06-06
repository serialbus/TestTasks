using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models.DAL
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Order> Orders { get; }

        Order CreateOrder();
        Product CreateProduct();
        void DeleteProduct(Product product);

        void SaveChanges();
    }
}
