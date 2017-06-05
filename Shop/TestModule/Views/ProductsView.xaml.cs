using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Module.Cashbox.Views
{
    /// <summary>
    /// Interaction logic for ProductsView
    /// </summary>
    [Export("ProductsView", typeof(ProductsView))]
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
        }
    }
}
