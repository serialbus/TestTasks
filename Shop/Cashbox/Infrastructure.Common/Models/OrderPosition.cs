using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models
{
    /// <summary>
    /// Позиция в заказе - Товар и его количество
    /// </summary>
    public class OrderPosition
    {
        public int Id { get; set; }
        /// <summary>
        /// Количество товара в штуках
        /// </summary>
        public int Quantity { get; set; }
        
        #region Relatives Tables
        
        /// <summary>
        /// Номер заказа (чека)
        /// </summary>
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        /// <summary>
        /// Номер товара
        /// </summary>
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        
        #endregion
    }
}
