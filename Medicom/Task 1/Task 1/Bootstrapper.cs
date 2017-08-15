using Microsoft.Practices.Unity;
using Prism.Unity;
using Medicom.Views;
using System.Windows;
using Prism.Modularity;
using DAL;
using Infrastructure.Common.Models.DAL;
using DAL.Services;
using Infrastructure.Common.Models.Configuration;
using Prism.Events;
using Medicom.ViewModels.Services;
using Prism.Regions;
using System;
using Infrastructure.Common.Services;

namespace Medicom
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
            var regionManager = this.Container.Resolve<IRegionManager>();

            var region = regionManager.Regions["ContentRegion"];
            region.RemoveAll();
            region.Add(this.Container.Resolve<ItemsView>());

            region = regionManager.Regions["MenuRegion"];
            region.RemoveAll();
            region.Add(this.Container.Resolve<MenuView>());
        }

        protected override void ConfigureModuleCatalog()
        {
            //base.ConfigureModuleCatalog();
            var moduleType = typeof(DALModule);
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = moduleType.Name,
                ModuleType = moduleType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IConfigurationService, ConfigurationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepositoryService, RepositoryService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IApplicationCommandsService, ApplicationCommandsService>(new ContainerControlledLifetimeManager());
        }
    }
}
