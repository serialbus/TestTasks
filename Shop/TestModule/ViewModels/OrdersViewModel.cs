using Infrastructure.Common;
using Infrastructure.Common.Models;
using Infrastructure.Common.Models.DAL;
using Infrastructure.Common.Services;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Cashbox.ViewModels
{
    [Export("OrdersViewModel", typeof(OrdersViewModel))]
    public class OrdersViewModel : BindableBase//, IRegionMemberLifetime
    {
        #region Constructors

        public OrdersViewModel()
        {
            Orders = new ObservableCollection<Order>(DataBaseService.Repository.Orders);
        }

        #endregion

        #region Fields And Properties

        //public bool KeepAlive => true;
        public ObservableCollection<Order> Orders { get; private set; }
        public IEnumerable<OrderPosition> OrderPositions
        {
            get { return SelectedOrder == null ? Enumerable.Empty<OrderPosition>() : SelectedOrder.OrderPositions; }
        }

        private Order _SelectedOrder;
        public Order SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                RaisePropertyChanged("SelectedOrder");
                RaisePropertyChanged("OrderPositions");
            }
        }

        #endregion
    }
}
