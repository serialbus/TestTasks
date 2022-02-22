using Infrastructure.Common.Module;
using Infrastructure.Common.Services;
using System.Collections.ObjectModel;

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
