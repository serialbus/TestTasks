using Prism.Modularity;
using Prism.Mef.Modularity;
using Prism.Regions;
using System;
using System.ComponentModel.Composition;
using Prism.Commands;
using Infrastructure.Common.Modularity;
using Infrastructure.Common.Services;
using Module.Cashbox.Views;
using Module.Cashbox.ViewModels;

namespace Module.Cashbox
{
    [ModuleExport(typeof(CashboxModule))]
    public class CashboxModule : IModule
    {
        private readonly IRegionManager RegionManager;

        [ImportingConstructor]
        public CashboxModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;

            OnShowNewOrderCommand = new NavigationCommand("Касса", OnShowNewOrder);
            OnShowOrdersCommand = new NavigationCommand("Заказы", OnShowOrders);
            OnShowProductsCommand = new NavigationCommand("Товары", OnShowProducts);
        }

        public void Initialize()
        {
            NavigationService.NavigationMenu.Add(OnShowNewOrderCommand);
            NavigationService.NavigationMenu.Add(OnShowOrdersCommand);
            NavigationService.NavigationMenu.Add(OnShowProductsCommand);
        }

        private NavigationCommand OnShowNewOrderCommand;
        private void OnShowNewOrder()
        {
            RegionManager.RequestNavigate("ContentRegion", new Uri("OrderView", UriKind.Relative));
        }
        private NavigationCommand OnShowOrdersCommand;
        private void OnShowOrders()
        {
            RegionManager.RequestNavigate("ContentRegion", new Uri("OrdersView", UriKind.Relative));
        }

        private NavigationCommand OnShowProductsCommand;
        private void OnShowProducts()
        {
            RegionManager.RequestNavigate("ContentRegion", new Uri("ProductsView", UriKind.Relative));
        }
    }
}