using Shop.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Shop.Views
{
    public partial class NavigationPanelView : UserControl
    {
        [ImportingConstructor]
        public NavigationPanelView()
        {
            InitializeComponent();
            DataContext = new NavigationPanelViewModel();
        }
        [ImportingConstructor]
        public NavigationPanelView(NavigationPanelViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
