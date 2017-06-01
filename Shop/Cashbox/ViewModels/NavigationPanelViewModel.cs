using Infrastructure.Common.Modularity;
using Infrastructure.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace Shop.ViewModels
{
    [Export(typeof(NavigationPanelViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NavigationPanelViewModel : BindableBase
    {
        public NavigationPanelViewModel() { }
        public ObservableCollection<INavigationCommand> NavigationMenu
        {
            get { return NavigationService.NavigationMenu; }
        } 
    }
}
