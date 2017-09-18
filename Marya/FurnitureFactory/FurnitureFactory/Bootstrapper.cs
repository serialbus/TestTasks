using Microsoft.Practices.Unity;
using Prism.Unity;
using FurnitureFactory.Views;
using System.Windows;
using Prism.Modularity;
using System;
using Infrastructure.Common.Services;

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
