using Infrastructure.Common.Module;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;
using System.Windows.Input;
using TestModule.Views;

namespace TestModule
{
    [Module(ModuleName = WellKnownModuleNames.TestModule, OnDemand = false)]
	public class TestModule : IModule
	{
		#region Constructors

		public TestModule(IUnityContainer container, IRegionManager regionManager)
		{
			Container = container;
			RegionManager = regionManager;

			_ShowComboBoxTestCommand = new DelegateCommand(OnShowComboBoxTest, CanShowComboBoxTest);
		}

		#endregion

		#region Fields And Properties

		private readonly IRegionManager RegionManager;
		private readonly IUnityContainer Container;

		#endregion

		#region Methods

		public void Initialize()
		{
			NavigationService.NavigationMenu.Add(new NavigationMenu("ComboBox тест", ShowComboBoxTestCommand));
		}

		#endregion

		#region Commands

		private DelegateCommand _ShowComboBoxTestCommand;
		public ICommand ShowComboBoxTestCommand
		{
			get { return _ShowComboBoxTestCommand; }
		}
		private void OnShowComboBoxTest()
		{
			var region = RegionManager.Regions["ContentRegion"];
			region.RemoveAll();
			RegionManager.RegisterViewWithRegion("ContentRegion", () => Container.Resolve<ComboBoxTestView>());
		}
		private bool CanShowComboBoxTest()
		{
			return RegionManager != null && Container != null;
		}

		#endregion
	}
}
