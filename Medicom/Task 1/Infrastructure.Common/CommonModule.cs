using Prism.Modularity;
using Prism.Mef.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Models.DAL;

namespace Infrastructure.Common
{
    [ModuleExport(typeof(CommonModule))]
    public class CommonModule : IModule
    {
        public void Initialize()
        {

        }
    }
}
