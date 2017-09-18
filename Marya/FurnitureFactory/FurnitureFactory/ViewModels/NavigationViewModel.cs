using Infrastructure.Common.Module;
using Infrastructure.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FurnitureFactory.ViewModels
{
	public class NavigationViewModel
	{
		public NavigationViewModel()
		{
		}

		public ObservableCollection<INavigationMenu> NavigationMenu
		{
			get { return NavigationService.NavigationMenu; }
		}
	}
}
