using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Modularity
{
    public abstract class ModuleBase : IModule
    {
        #region Costructor

        public ModuleBase(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;
        }

        #endregion

        #region Fields And Properties

        protected readonly IRegionManager RegionManager;
        protected readonly IUnityContainer Container;

        #endregion

        #region Methods

        public abstract void Initialize();

        #endregion
    }
}
