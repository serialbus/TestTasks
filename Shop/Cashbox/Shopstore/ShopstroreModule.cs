using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopstore
{
    public class ShopstoreModule : IModule
    {
        IRegionManager _regionManager;

        [ImportingConstructor]
        public ShopstoreModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
