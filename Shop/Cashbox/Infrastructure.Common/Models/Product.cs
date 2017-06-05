using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models
{
    /// <summary>
    /// Товар на складе
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Код товара
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Цена за штуку. руб
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Количество товара на складе
        /// </summary>
        public int Qauntity { get; set; }
    }
}
