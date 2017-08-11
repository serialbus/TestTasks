using System;
using System.Windows;
using System.Reflection;
using System.Globalization;
using Prism.Mef;
using Prism.Mvvm;
using Task.Views;
using System.ComponentModel.Composition.Hosting;
using Infrastructure.Common;
using Infrastructure.DAL;
using Infrastructure.Common.Services.Configuration;

namespace Task
{
    class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<ShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (ShellView)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CommonModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DALModule).Assembly));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }


        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.FullName;
                viewName = viewName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}{1}", viewName, suffix);

                var assembly = viewType.GetTypeInfo().Assembly;
                var type = assembly.GetType(viewModelName, true);

                return type;
            });
        }
    }
}
