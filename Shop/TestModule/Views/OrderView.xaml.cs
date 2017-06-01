using Module.Cashbox.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module.Cashbox.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    [Export("OrderView", typeof(OrderView))]
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
        }
    }
}
