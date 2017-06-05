using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Module.Cashbox.Views
{
    /// <summary>
    /// Interaction logic for ProductEditingView
    /// </summary>
    [Export("ProductView", typeof(ProductView))]
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
        }
    }
}
