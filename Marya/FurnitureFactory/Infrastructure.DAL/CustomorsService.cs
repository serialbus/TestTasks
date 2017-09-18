using Infrastructure.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastucture.Common.Models;

namespace Infrastructure.DAL
{
	public class CustomorsService : ICustomerService
	{
		public CustomorsService()
		{
		}

		private static List<Customer> _Customers = new List<Customer> {
			new Customer { LastName = "Попов", FirstName = "Андрей", MiddleName = "Сергеевич", Address = "" },
			new Customer {LastName = "Семёнов", FirstName = "Семен", MiddleName = "Семёнович", Address = "" }
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
