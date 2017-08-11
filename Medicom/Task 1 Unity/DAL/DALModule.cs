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
        IRegionManager _regionManager;

        public DALModule(IUnityContainer unityContainer, IRegionManager regionManager): 
            base(unityContainer, regionManager)
        {
        }

        public override void Initialize()
        {
        }
    }
}