using Infrastructure.Common.Module;
using System.Collections.ObjectModel;

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
