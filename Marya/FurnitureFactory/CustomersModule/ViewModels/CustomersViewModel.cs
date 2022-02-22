using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using System.Collections.Generic;

namespace OrdersModule.ViewModels
{
    public class CustomersViewModel : BindableBase
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
