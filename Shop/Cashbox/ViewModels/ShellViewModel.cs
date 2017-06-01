using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Shop.ViewModels
{
    [Export("ShellViewModel", typeof(ShellViewModel))]
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel()
        {

        }
    }
}
