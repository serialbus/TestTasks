using System.ComponentModel.Composition;
using System.Windows;

namespace Task.Views
{
    [Export("ShellView", typeof(ShellView))]
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }
    }
}
