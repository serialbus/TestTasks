using Infrastructure.Common.Models.DAL;
using Infrastructure.Common.Services;
using Prism.Mef.Modularity;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    [ModuleExport(typeof(CommonModule))]
    public class CommonModule : IModule
    {
        [Import(typeof(IRepository))]
        public IRepository Repository { get; set; }

        public void Initialize()
        {
            DataBaseService.Repository = Repository;
            //throw new NotImplementedException();
        }
    }
}
