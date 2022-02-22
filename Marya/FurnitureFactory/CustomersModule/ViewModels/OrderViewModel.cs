using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Prism.Mvvm;
using System;
using System.Linq;

namespace OrdersModule.ViewModels
{
    public class OrderViewModel : BindableBase
    {
        #region Constructor

        public OrderViewModel(IOfficesService officesService, ICustomerService customerService, Order order)
        {
            Order = order;
            OfficesService = officesService;
            CustomerService = customerService;
            if (order != null)
            {
                _CustomerId = Order.CustomerId;
                _IsDone = Order.Done;
                _OfficeId = Order.ExecutorId;
                _ExecutionTime = Order.ExecutionTime;
            }
        }

        #endregion

        #region Fields And Properties

        protected readonly IOfficesService OfficesService;
        protected readonly ICustomerService CustomerService;

        public Order Order { get; private set; }

        private bool _IsDone;
        public bool IsDone
        {
            get { return _IsDone; }
            set
            {
                _IsDone = value;
                OnPropertyChanged(() => IsDone);
            }
        }

        private Guid _CustomerId;
        public Guid CustomerId
        {
            get { return _CustomerId; }
            set
            {
                _CustomerId = value;
                OnPropertyChanged(() => CustomerId);
                OnPropertyChanged(() => CustomerFullName);
            }
        }
        public string CustomerFullName
        {
            get
            {
                var customer = CustomerService.Customers.FirstOrDefault(c => _CustomerId == c.Uid);
                return customer == null ? String.Empty : customer.FullName;
            }
        }
        private Guid _OfficeId;
        public Guid OfficeId
        {
            get { return _OfficeId; }
            set
            {
                _OfficeId = value;
                OnPropertyChanged(() => OfficeId);
                OnPropertyChanged(() => Office);
            }
        }
        public Office Office
        {
            get
            {
                return OfficesService == null ? null : OfficesService.Offices.FirstOrDefault(o => o.Uid == _OfficeId);
            }
        }

        private DateTime? _ExecutionTime;
        public DateTime? ExecutionTime
        {
            get { return _ExecutionTime; }
            set
            {
                _ExecutionTime = value;
                OnPropertyChanged(() => ExecutionTime);
            }
        }
        #endregion
    }
}
