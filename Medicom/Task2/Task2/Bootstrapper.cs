using Microsoft.Practices.Unity;
using Prism.Unity;
using Medicom.Task2.Views;
using System.Windows;
using MultimediaModule;
using Prism.Modularity;

namespace Medicom.Task2
{
	class Bootstrapper : UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<Shell>();
		}

		protected override void InitializeShell()
		{
			Application.Current.MainWindow.Show();
		}

		protected override void ConfigureModuleCatalog()
		{
			//base.ConfigureModuleCatalog();
			var moduleType = typeof(MultimediaModule.MultimediaModule);
			ModuleCatalog.AddModule(new ModuleInfo
			{
				ModuleName = moduleType.Name,
				ModuleType = moduleType.AssemblyQualifiedName,
				InitializationMode = InitializationMode.WhenAvailable
			});
		}
	}
}
