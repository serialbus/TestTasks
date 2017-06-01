using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Cashbox.ViewModels
{
    [Export("OrderViewModel", typeof(OrderViewModel))]
    public class OrderViewModel: BindableBase
    {
        #region Constructor

        public OrderViewModel() { }

        #endregion
    }
}
