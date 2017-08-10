using Microsoft.Practices.Unity;
using Prism.Unity;
using Task_1.Views;
using System.Windows;

namespace Task_1
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
