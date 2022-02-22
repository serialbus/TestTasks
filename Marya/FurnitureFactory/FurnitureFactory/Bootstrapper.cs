using FurnitureFactory.Views;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace FurnitureFactory
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindowView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<ICustomerService, CustomorsService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IOrdersService, OrdersService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IOfficesService, OfficesService>(new ContainerControlledLifetimeManager());
            base.ConfigureContainer();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //return base.CreateModuleCatalog();
            return new DirectoryModuleCatalog { ModulePath = @".\Modules" };
        }
    }
}
