using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace OrdersModule.ViewModels
{
    public class OrdersDispatcherViewModel : BindableBase
    {
        #region Constructors

        public OrdersDispatcherViewModel(IUnityContainer container)
        {
            ConfirmationDialogRequest = new InteractionRequest<IConfirmation>();

            _OrdersService = container.Resolve<IOrdersService>();
            _CustomerService = container.Resolve<ICustomerService>();
            _OfficesService = container.Resolve<IOfficesService>();

            Orders = new ObservableCollection<OrderViewModel>(
                _OrdersService.Orders
                    .Where(order => !order.ExecutionTime.HasValue || order.ExecutorId == Guid.Empty)
                    .Select(order => new OrderViewModel(_OfficesService, _CustomerService, order)));

            SelectedOrder = Orders.FirstOrDefault();

            EventsService.Events.GetEvent<OrderWasAddedEvent>().Subscribe(OnOrderWasAdded);

            _EditOrderCommand = new DelegateCommand(OnEditOrder, CanEditOrder).ObservesProperty(() => SelectedOrder);
        }

        #endregion

        #region Fields And Properties

        public InteractionRequest<IConfirmation> ConfirmationDialogRequest { get; private set; }

        private readonly IOrdersService _OrdersService;
        private readonly ICustomerService _CustomerService;
        private readonly IOfficesService _OfficesService;

        public ObservableCollection<OrderViewModel> Orders { get; private set; }

        private OrderViewModel _SelectedOrder;
        public OrderViewModel SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                OnPropertyChanged(() => SelectedOrder);
            }
        }

        #endregion

        #region Methods

        private void OnOrderWasAdded(Order order)
        {
            if (!order.ExecutionTime.HasValue || order.ExecutorId == Guid.Empty)
                Orders.Add(new OrderViewModel(_OfficesService, _CustomerService, order));
        }

        #endregion

        #region Commands

        private DelegateCommand _EditOrderCommand;
        public ICommand EditOrderCommand
        {
            get { return _EditOrderCommand; }
        }
        private void OnEditOrder()
        {
            var vm = new OrderEditorDialogViewModel(_OfficesService, _CustomerService, _OrdersService, SelectedOrder.Order);

            ConfirmationDialogRequest.Raise(new Confirmation { Title = "Редактирование заказа", Content = vm });

            if (vm.Confirmation.Confirmed)
            {
                SelectedOrder.Order.ExecutionTime = vm.ExecutionTime;
                SelectedOrder.Order.ExecutorId = vm.OfficeId;
                Orders.Remove(SelectedOrder);
            }
        }
        private bool CanEditOrder()
        {
            return SelectedOrder != null;
        }

        #endregion
    }
}
