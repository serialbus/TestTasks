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

        public ModuleBase(IUnityContainer container)
        {
            Container = container;
        }

        #endregion

        #region Fields And Properties

        protected readonly IUnityContainer Container;

        #endregion

        #region Methods

        public abstract void Initialize();

        #endregion
    }
}
