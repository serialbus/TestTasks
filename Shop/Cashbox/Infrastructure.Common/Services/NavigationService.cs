using Infrastructure.Common.Modularity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
    /// <summary>
    /// Сервис для хранения комманд навигации по приложению, 
    /// предостваляемые модулями приложения
    /// </summary>
    public static class NavigationService
    {
        private readonly static ObservableCollection<INavigationCommand> _NavigationMenu =
            new ObservableCollection<INavigationCommand>();

        public static ObservableCollection<INavigationCommand> NavigationMenu
        {
            get { return _NavigationMenu; }
        }
    }
}
