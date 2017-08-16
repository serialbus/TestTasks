using Microsoft.Practices.Unity;
using Prism.Unity;
using Medicom.Task2.Views;
using System.Windows;

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
	}
}
