using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace OrdersModule.ViewModels
{
    public class ClientAddsOrderViewModel : BindableBase
    {
        #region Constructors

        public ClientAddsOrderViewModel(IUnityContainer container)
        {
            _OrdersService = container.Resolve<IOrdersService>();
            _CustomerService = container.Resolve<ICustomerService>();
            _OfficesService = container.Resolve<IOfficesService>();

            Customers = new ObservableCollection<Customer>(_CustomerService.Customers);
            Offices = new ObservableCollection<Office>(_OfficesService.Offices);

            _CreateOrderCommand = new DelegateCommand(OnCreateOrder, CanCreateOrder)
                                            .ObservesProperty(() => SelectedCustomer);

            EventsService.Events.GetEvent<OrderWasAddedEvent>().Subscribe(OnOrderWasAdded);
        }

        #endregion

        #region Fields And Properties

        private readonly IOrdersService _OrdersService;
        private readonly ICustomerService _CustomerService;
        private readonly IOfficesService _OfficesService;
        public ObservableCollection<Customer> Customers { get; private set; }
        public ObservableCollection<Office> Offices { get; private set; }
        private Customer _SelectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _SelectedCustomer; }
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged(() => SelectedCustomer);
                OnPropertyChanged(() => IsVisibleMyOrders);
                OnPropertyChanged(() => CustomerOrders);
            }
        }
        private Office _SelectedOffice;
        public Office SelectedOffice
        {
            get { return _SelectedOffice; }
            set
            {
                _SelectedOffice = value;
                OnPropertyChanged(() => SelectedOffice);
            }
        }

        private IEnumerable<Order> _CustomerOrders;
        public IEnumerable<Order> CustomerOrders
        {
            get
            {
                return _SelectedCustomer == null ? Enumerable.Empty<Order>() :
                    _OrdersService.GetOrdersByCustomer(_SelectedCustomer.Uid);
            }
        }

        public Visibility IsVisibleMyOrders
        {
            get { return SelectedCustomer == null ? Visibility.Hidden : Visibility.Visible; }
        }

        #endregion

        #region Methods

        private void OnOrderWasAdded(Order order)
        {
            if (SelectedCustomer != null && order.CustomerId == SelectedCustomer.Uid)
                OnPropertyChanged(() => CustomerOrders);
        }

        #endregion

        #region Commands

        private DelegateCommand _CreateOrderCommand;
        public ICommand CreateOrderCommand
        {
            get { return _CreateOrderCommand; }
        }
        private void OnCreateOrder()
        {
            var order = _OrdersService.CreateNewOreder();
            order.CustomerId = SelectedCustomer.Uid;
            _OrdersService.AddOrder(order);
        }
        private bool CanCreateOrder()
        {
            return SelectedCustomer != null;
        }

        #endregion
    }
}
