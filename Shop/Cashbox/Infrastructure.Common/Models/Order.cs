using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models
{
    /// <summary>
    /// Заказ (чек) при покупке товара
    /// </summary>
    public class Order
    {
        #region Constructor

        public Order()
        {
            StartTransaction = DateTime.Now;
            OrderPositions = new List<OrderPosition>();
            IsClosed = false;
        }

        #endregion

        #region Fields And Properties
        /// <summary>
        /// Уникальный номер чека 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Чек закрыт
        /// </summary>
        public bool IsClosed { get; set; }
        /// <summary>
        /// Дата и время открытия (создания) чека
        /// </summary>
        public DateTime? StartTransaction { get; set; }
        /// <summary>
        /// Дата и время закрытия чека
        /// </summary>
        public DateTime? EndTransaction { get; set; }
        /// <summary>
        /// Наличные принятые от покупателя
        /// </summary>
        public decimal Cash { get; set; }
        /// <summary>
        /// Сдача денег 
        /// </summary>
        public decimal Change { get; set; }
        /// <summary>
        /// Позиции заказа
        /// </summary>
        public ICollection<OrderPosition> OrderPositions { get; set; }

        #endregion
    }
}
