using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models
{
	public class Order
	{
		public Order()
		{
			CustomerId = Guid.Empty;
			Done = false;
			ExecutionTime = null;
		}
		/// <summary>
		/// Номер заказа
		/// </summary>
		public int OrderNumber { get; set; }
		/// <summary>
		/// Заказчик
		/// </summary>
		public Guid CustomerId { get; set; }
		/// <summary>
		/// Испольнитель (конкретный офис)
		/// </summary>
		public Guid ExecutorId { get; set; }
		/// <summary>
		/// Дата и время замера
		/// </summary>
		public DateTime? ExecutionTime { get; set; }
		/// <summary>
		/// Отметка о выполнении задания
		/// </summary>
		public bool Done { get; set; }
	}
}
