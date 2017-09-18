using Infrastructure.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
	/// <summary>
	/// Интерфейс для работы с БД.
	/// </summary>
	public interface ICustomerService
	{
		IList<Customer> Customers { get; }
	}
}
