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
            Products = new ObservableCollection<Product>(DataBaseService.Repository.Products);
            Orders = new ObservableCollection<Order>(DataBaseService.Repository.Orders);
        }

        #endregion

        #region Fields And Properties

        //public bool KeepAlive => true;
        public ObservableCollection<Order> Orders { get; private set; }
        public ObservableCollection<Product> Products { get; private set; }

        private Order _SelectedOrder;
        public Order SelectedOrder
        {
            get { return _SelectedOrder; }
            set
            {
                _SelectedOrder = value;
                RaisePropertyChanged("SelectedOrder");
            }
        }

        #endregion
    }
}
