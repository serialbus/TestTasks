using Infrastructure.Common.Models.DAL;
using Infrastructure.Common.Services.Configuration;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.ComponentModel.Composition;

namespace Infrastructure.DAL
{
    [ModuleExport(typeof(DALModule))]
    public class DALModule : IModule
    {
        IRegionManager _regionManager;
        //IRepositoryService RepositoryService;
        //IConfigurationService ConfigurationService;

        [ImportingConstructor]
        public DALModule(IRegionManager regionManager)//, IConfigurationService configurationService)
        {
            _regionManager = regionManager;
            //ConfigurationService = configurationService;
        }

        public void Initialize()
        {
            //RepositoryService = new RepositoryService(ConfigurationService);
            //throw new NotImplementedException();
        }
    }
}