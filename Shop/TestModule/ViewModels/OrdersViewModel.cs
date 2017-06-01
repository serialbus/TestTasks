using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
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
        }

        #endregion

        #region Fields And Properties

        //public bool KeepAlive => true;
        
        #endregion
    }
}
