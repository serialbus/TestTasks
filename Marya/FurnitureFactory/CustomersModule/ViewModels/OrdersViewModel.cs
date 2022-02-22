using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace OrdersModule.ViewModels
{
    public class OrdersViewModel : BindableBase
    {
        public OrdersViewModel(IUnityContainer container)
        {
            OrdersService = container.Resolve<IOrdersService>();
            CustomerService = container.Resolve<ICustomerService>();
            OfficeService = container.Resolve<IOfficesService>();

            _AddOrderCommand = new DelegateCommand(OnAddOrder, CanAddOrder);

            NotificationRequest = new InteractionRequest<INotification>();
            ConfirmationDialogRequest = new InteractionRequest<IConfirmation>();

            Orders = new ObservableCollection<Order>(OrdersService.Orders);

            EventsService.Events.GetEvent<OrderWasAddedEvent>().Subscribe(OnOrderAdded);
        }

        #region Fields And Properties

        public InteractionRequest<INotification> NotificationRequest { get; private set; }
        public InteractionRequest<IConfirmation> ConfirmationDialogRequest { get; private set; }


        private readonly IOrdersService OrdersService;
        private readonly ICustomerService CustomerService;
        private readonly IOfficesService OfficeService;

        public ObservableCollection<Order> Orders { get; private set; }

        private Order _SelectedOrder;
        public Order SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                OnPropertyChanged(() => SelectedOrder);
                OnPropertyChanged(() => SelectedCustomer);
                OnPropertyChanged(() => SelectedOffice);
                OnPropertyChanged(() => IsCanSetOrderStatus);
                OnPropertyChanged(() => IsDone);
            }
        }
        public Customer SelectedCustomer
        {
            get
            {
                return SelectedOrder == null ? null :
                    CustomerService.Customers.FirstOrDefault(customer => customer.Uid == SelectedOrder.CustomerId);
            }
        }
        public Office SelectedOffice
        {
            get
            {
                return SelectedOrder == null ? null :
                    OfficeService.Offices.FirstOrDefault(office => office.Uid == SelectedOrder.ExecutorId);

            }
        }
        public bool IsCanSetOrderStatus
        {
            get
            {
                return SelectedOrder != null;
            }
        }
        public bool IsDone
        {
            get { return SelectedOrder == null ? false : SelectedOrder.Done; }
            set
            {
                if (SelectedOrder != null)
                    SelectedOrder.Done = value;
            }
        }
        #endregion

        #region Methods

        private void OnOrderAdded(Order order)
        {
            Orders.Add(order);
            SelectedOrder = order;
        }

        #endregion

        #region Commands

        private DelegateCommand _AddOrderCommand;

        public ICommand AddOrderCommand { get { return _AddOrderCommand; } }

        private void OnAddOrder()
        {
            bool dialogResult = false;

            var order = OrdersService.CreateNewOreder();

            ConfirmationDialogRequest.Raise(new Confirmation { Title = "Редактирование заказа", Content = order },
                c => { dialogResult = c.Confirmed; });

            if (dialogResult)
            {
                OrdersService.AddOrder(order);
            }
        }
        private bool CanAddOrder()
        {
            return true;
        }

        #endregion
    }
}
