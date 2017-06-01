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

        public Order() { IsClosed = false; }

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
        /// Дата и время закрытия 
        /// </summary>
        public DateTime ClosingDate { get; set; }
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
        public List<OrderPosition> OrderPosition { get; set; }

        #endregion
    }
}
