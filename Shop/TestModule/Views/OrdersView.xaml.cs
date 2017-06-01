using Module.Cashbox.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Module.Cashbox.Views
{
    /// <summary>
    /// Interaction logic for OredersView
    /// </summary>
    [Export("OrdersView", typeof(OrdersView))]
    public partial class OrdersView : UserControl
    {
        public OrdersView()
        {
            InitializeComponent();
        }
    }
}
