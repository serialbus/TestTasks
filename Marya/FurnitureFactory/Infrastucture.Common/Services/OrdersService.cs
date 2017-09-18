using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Models;

namespace Infrastructure.Common.Services
{
	public class OrdersService : IOrdersService
	{
		public OrdersService()
		{
			_NumbersService = new NumbersGeneratorService();
		}

		#region Fields And Properties

		private static List<Order> _Orders = new List<Order>();
		private readonly INumbersGeneratorService _NumbersService;

		public IEnumerable<Order> Orders
		{
			get { return _Orders; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Добавляет заказ замера
		/// </summary>
		public void AddOrder(Order order)
		{
			if (order.OrderNumber == 0)
				throw new ArgumentException("Невозможно добавить заказ. Недопустимый номер заказа",
					"order.OrderNumber");
	
			if (_Orders.Exists(o => o.OrderNumber == order.OrderNumber))
				throw new InvalidOperationException(String.Format(
					"Невозможно добавить заказ. Заказ замера с номером {0} уже существует", order.OrderNumber));

			if (order.CustomerId == Guid.Empty)
				throw new ArgumentException(String.Format(
					"Невозможно добавить заказ. Не выбран заказчик"), "order.CustomerId");

			if (order.Done == true)
				throw new ArgumentException(String.Format(
					"Невозможно добавить заказ со статусом - выполнен"), "order.Done");

			_Orders.Add(order);
			EventsService.Events.GetEvent<OrderWasAddedEvent>().Publish(order);
		}

		public Order CreateNewOreder()
		{
			var number = _NumbersService.GenerateOrderNumber();
			return new Order { OrderNumber = number };
		}

		/// <summary>
		/// Возвращает все заказы замеров по указанной дате.
		/// </summary>
		/// <param name="officeUid">Офис</param>
		/// <param name="date"></param>
		/// <returns></returns>
		public IEnumerable<Order> GetScheduleByDate(Guid officeUid, DateTime date)
		{
			// Если выходной день, возвращаем пустое расписание
			if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
				return Enumerable.Empty<Order>();

			// Получаем все работы по данной дате
			return _Orders.Where(order => order.ExecutorId == officeUid && 
										  order.ExecutionTime.HasValue && 
										  order.ExecutionTime.Value.Date == date.Date).ToArray();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerUid"></param>
		/// <returns></returns>
		public IEnumerable<Order> GetOrdersByCustomer(Guid customerUid)
		{
			return _Orders.Where(order => order.CustomerId == customerUid).ToArray();
		}
		/// <summary>
		/// Устанавливает статус выполения заказа
		/// </summary>
		/// <param name="orderNumber">Номер заказа</param>
		/// <param name="status">True - заказ выполнен</param>
		public void SetOrderStatus(int orderNumber, bool status)
		{
			var order = _Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
			if (order != null)
				order.Done = status;
		}

		#endregion
	}
}
