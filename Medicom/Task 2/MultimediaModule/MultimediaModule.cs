using Prism.Modularity;
using Prism.Regions;
using System;

namespace MultimediaModule
{
	public class MultimediaModule : IModule
	{
		IRegionManager _regionManager;

		public MultimediaModule(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void Initialize()
		{
		}
	}
}