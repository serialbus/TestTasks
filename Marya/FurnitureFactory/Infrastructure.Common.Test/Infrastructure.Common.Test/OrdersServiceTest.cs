using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Common.Services;
using System.Linq;

namespace Infrastructure.Common.Test.Infrastructure.Common.Test
{
	/// <summary>
	/// Тесты для сервиса OrdersService
	/// </summary>
	[TestClass]
	public class OrdersServiceTest
	{
		/// <summary>
		/// Проверяет последовательность номеров заказов
		/// создаваемых сервисом
		/// </summary>
		[TestMethod]
		public void CheckSequenceSerialNumbers()
		{
			//Arrange
			var service = new OrdersService();
			
			//Act
			var order1 = service.CreateNewOreder();
			var order2 = service.CreateNewOreder();
			var order3 = service.CreateNewOreder();

			//Assert
			Assert.AreEqual(1, order1.OrderNumber);
			Assert.AreEqual(2, order2.OrderNumber);
			Assert.AreEqual(3, order3.OrderNumber);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAddingOrderWithWrongOrderNumber()
		{
			//Arrange
			var service = new OrdersService();
			var order = service.CreateNewOreder();
			order.OrderNumber = 0;
			order.CustomerId = Guid.NewGuid();
			order.ExecutorId = Guid.NewGuid();

			//Act
			service.AddOrder(order);

			//Assert
			// Exception!
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAddingOrderWithWrongCustomerId()
		{
			//Arrange
			var service = new OrdersService();
			var order = service.CreateNewOreder();
			order.ExecutorId = Guid.NewGuid();

			//Act
			service.AddOrder(order);

			//Assert
			// Exception!
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestAddingOrderWithWrongPropetyDone()
		{
			//Arrange
			var service = new OrdersService();
			var order = service.CreateNewOreder();
			order.Done = true;

			//Act
			service.AddOrder(order);

			//Assert
			// Exception!
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TestAddingOrderWithExistingOrderNumber()
		{
			//Arrange
			var service = new OrdersService();
			var order = service.CreateNewOreder();
			order.CustomerId = Guid.NewGuid();
			order.ExecutorId = Guid.NewGuid();

			//Act
			service.AddOrder(order);
			service.AddOrder(order);

			//Assert
			// Exception!
		}

		[TestMethod]
		public void TestSetOrderStatus()
		{
			//Arrange
			var service = new OrdersService();
			var order = service.CreateNewOreder();
			order.CustomerId = Guid.NewGuid();
			order.ExecutorId = Guid.NewGuid();
			order.Done = false;

			//Act
			service.AddOrder(order);
			service.SetOrderStatus(order.OrderNumber, true);

			//Assert
			Assert.AreEqual(true, order.Done);
		}

		[TestMethod]
		public void TestGetScheduleByDate()
		{
			//Arrange
			var officeId = Guid.NewGuid();
			var dt = DateTime.Now.Date;
			var service = new OrdersService();

			var order1 = service.CreateNewOreder();
			order1.CustomerId = Guid.NewGuid();
			order1.ExecutorId = officeId;
			order1.ExecutionTime = dt;
			service.AddOrder(order1);

			var order2 = service.CreateNewOreder();
			order2.CustomerId = Guid.NewGuid();
			order2.ExecutorId = officeId;
			order2.ExecutionTime = dt;
			service.AddOrder(order2);

			var order3 = service.CreateNewOreder();
			order3.CustomerId = Guid.NewGuid();
			order3.ExecutorId = officeId;
			order3.ExecutionTime = dt.AddDays(3);
			service.AddOrder(order3);

			//Act
			var orders = service.GetScheduleByDate(officeId, dt).ToArray();

			//Assert
			Assert.AreNotEqual(2, service.Orders.Count());
			Assert.AreEqual(2, orders.Count());
			Assert.IsNotNull(orders.FirstOrDefault(x => x.OrderNumber == order1.OrderNumber));
			Assert.IsNotNull(orders.FirstOrDefault(x => x.OrderNumber == order2.OrderNumber));
			Assert.IsNull(orders.FirstOrDefault(x => x.OrderNumber == 0));
		}
	}
}
