using Infrastructure.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
	public interface IOrdersService
	{
		IEnumerable<Order> Orders { get; }
		void AddOrder(Order newOrder);
		Order CreateNewOreder();
		IEnumerable<Order> GetScheduleByDate(Guid officeUid, DateTime date);
		void SetOrderStatus(int orderNumber, bool status);
		IEnumerable<Order> GetOrdersByCustomer(Guid customerUid);
	}
}
