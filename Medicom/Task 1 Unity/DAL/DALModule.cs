using Infrastructure.Common.Models.DAL;
using Infrastructure.Common.Modularity;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace DAL
{
    [Module(ModuleName = "DALModule")]
    //[ModuleDependency()]
    public class DALModule : ModuleBase
    {
        private readonly IRegionManager RegionManager;

        public DALModule(IUnityContainer unityContainer): 
            base(unityContainer)
        {
            RegionManager = unityContainer.Resolve<IRegionManager>();
        }

        public override void Initialize()
        {
        }
    }
}