using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Models;

namespace Infrastructure.Common.Services
{
	public class CustomorsService : ICustomerService
	{
		public CustomorsService()
		{
		}

		private static List<Customer> _Customers = new List<Customer> {
			new Customer { LastName = "Попов", FirstName = "Андрей", MiddleName = "Сергеевич",
				Address = "г.Саратов, ул. Чапаева, д5, кв7" },
			new Customer { LastName = "Семёнов", FirstName = "Семен", MiddleName = "Семёнович",
				Address = "г.Саратов, ул. Чернышевского, д3, кв10" }
		};

		public IList<Customer> Customers
		{
			get
			{
				return _Customers;
			}
		}
	}
}
