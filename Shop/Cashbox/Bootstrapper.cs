using Prism.Mef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using System.Windows;
using Shop.Views;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using Module.Cashbox;
using Prism.Mvvm;
using System.Globalization;
using Infrastructure.DAL;
using Infrastructure.Common.Services;
using Infrastructure.Common;

namespace Shop
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<ShellView>("ShellView");
            //return base.CreateShell();
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
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ShopContext).Assembly));
            //Module A is referenced in in the project and directly in code.
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CashboxModule).Assembly));
        }

        protected override void ConfigureViewModelLocator()
        {
            //base.ConfigureViewModelLocator();

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
