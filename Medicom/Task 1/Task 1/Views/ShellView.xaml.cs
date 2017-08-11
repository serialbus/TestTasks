using System;
using System.ComponentModel.Composition;
using System.Windows;
using Task.ViewModels;

namespace Task.Views
{
    [Export(typeof(ShellView))]
    public partial class ShellView : Window
    {
        //[ImportingConstructor()]
        public ShellView()
        {
            InitializeComponent();
        }

        //[ImportingConstructor]
        //public ShellView(ShellViewModel vm)
        //{
        //    //InitializeComponent();
        //    DataContext = vm;
        //}
    }
}
