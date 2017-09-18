using Infrastructure.Common.Module;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Common.Services
{
	/// <summary>
	/// Сервис для хранения комманд навигации по приложению, 
	/// предостваляемые модулями приложения
	/// </summary>
	public static class NavigationService
	{
		private static ObservableCollection<INavigationMenu> _NavigationMenu = 
			new ObservableCollection<INavigationMenu>();
		public static ObservableCollection<INavigationMenu> NavigationMenu
		{
			get { return _NavigationMenu; }
		}
	}
}
