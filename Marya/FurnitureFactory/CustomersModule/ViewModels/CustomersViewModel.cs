using Infrastructure.Common.Services;
using Infrastructure.Common.Models;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersModule.ViewModels
{
	public class CustomersViewModel: BindableBase
	{
		public CustomersViewModel(IUnityContainer container)
		{
			CustomerService = container.Resolve<ICustomerService>();
		}

		private readonly ICustomerService CustomerService;

		public IList<Customer> Customers { get { return CustomerService.Customers; } }

		public Customer SelectedCustomer { get; set; }
	}
}
