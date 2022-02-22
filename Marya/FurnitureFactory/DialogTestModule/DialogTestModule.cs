using Infrastructure.Common.Module;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;
using System.Windows.Input;

namespace DialogTestModule
{
    [Module(ModuleName = WellKnownModuleNames.DialogTestModule, OnDemand = false)]
    public class DialogTestModule : ModuleBase
    {
        #region Constructors

        public DialogTestModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
            Container = container;
            RegionManager = regionManager;

            _ShowCustomersCommand = new DelegateCommand(OnShowCustomers, CanShowCustomers);
            //_ShowOrdersCommand = new DelegateCommand(OnShowOrders, CanShowOrders);
            //_ShowOfficesCommand = new DelegateCommand(OnShowOffices, CanShowOffices);
            //_ShowClientAddsOrderCommand = new DelegateCommand(OnShowClientAddsOrder, CanShowClientAddsOrder);
            //_ShowOredersDispatcherCommand = new DelegateCommand(OnShowOredersDispatcher, CanShowOredersDispatcher);
        }

        #endregion

        #region Fields And Properties

        private readonly IRegionManager RegionManager;
        private readonly IUnityContainer Container;

        #endregion

        #region Methods

        public override void Initialize()
        {
            //RegionManager.RegisterViewWithRegion("ContentRegion", () => Container.Resolve<CustomersView>());
            NavigationService.NavigationMenu.Add(new NavigationMenu("Диалог 1", ShowCustomersCommand));
            //NavigationService.NavigationMenu.Add(new NavigationMenu("Заказы", ShowOrdersCommand));
            //NavigationService.NavigationMenu.Add(new NavigationMenu("Офисы", ShowOfficesCommand));
            //NavigationService.NavigationMenu.Add(new NavigationMenu("Сделать заказ", ShowClientAddsOrderCommand));
            //NavigationService.NavigationMenu.Add(new NavigationMenu("Диспетчер заказов", ShowOredersDispatcherCommand));
        }

        #endregion

        #region Commands Navigation

        private DelegateCommand _ShowCustomersCommand;
        public ICommand ShowCustomersCommand
        {
            get { return _ShowCustomersCommand; }
        }
        private void OnShowCustomers()
        {
            var region = RegionManager.Regions["ContentRegion"];
            region.RemoveAll();
            //RegionManager.RegisterViewWithRegion("ContentRegion", () => Container.Resolve<CustomersView>());
        }
        private bool CanShowCustomers()
        {
            return RegionManager != null && Container != null;
        }

        //private DelegateCommand _ShowOrdersCommand;
        //public ICommand ShowOrdersCommand
        //{
        //    get { return _ShowOrdersCommand; }
        //}
        //private void OnShowOrders()
        //{
        //    var region = RegionManager.Regions["ContentRegion"];
        //    region.RemoveAll();
        //    RegionManager.RegisterViewWithRegion("ContentRegion", () => Container.Resolve<OrdersView>());
        //}
        //private bool CanShowOrders()
        //{
        //    return RegionManager != null && Container != null;
        //}

        //private DelegateCommand _ShowOfficesCommand;
        //public ICommand ShowOfficesCommand
        //{
        //    get { return _ShowOfficesCommand; }
        //}
        //private void OnShowOffices()
        //{
        //    var region = RegionManager.Regions["ContentRegion"];
        //    region.RemoveAll();
        //    RegionManager.RegisterViewWithRegion("ContentRegion", () => Container.Resolve<OfficesView>());
        //}
        //private bool CanShowOffices()
        //{
        //    return RegionManager != null && Container != null;
        //}

        //private DelegateCommand _ShowClientAddsOrderCommand;
        //public ICommand ShowClientAddsOrderCommand
        //{
        //    get { return _ShowClientAddsOrderCommand; }
        //}
        //private void OnShowClientAddsOrder()
        //{
        //    var region = RegionManager.Regions["ContentRegion"];
        //    region.RemoveAll();
        //    RegionManager.RegisterViewWithRegion("ContentRegion", () => Container.Resolve<ClientAddsOrderView>());
        //}
        //private bool CanShowClientAddsOrder()
        //{
        //    return RegionManager != null && Container != null;
        //}

        //private DelegateCommand _ShowOredersDispatcherCommand;
        //public ICommand ShowOredersDispatcherCommand
        //{
        //    get { return _ShowOredersDispatcherCommand; }
        //}
        //private void OnShowOredersDispatcher()
        //{
        //    var region = RegionManager.Regions["ContentRegion"];
        //    region.RemoveAll();
        //    RegionManager.RegisterViewWithRegion("ContentRegion", () => Container.Resolve<OrdersDispatcherView>());
        //}
        //private bool CanShowOredersDispatcher()
        //{
        //    return RegionManager != null && Container != null;
        //}

        #endregion
    }
}
