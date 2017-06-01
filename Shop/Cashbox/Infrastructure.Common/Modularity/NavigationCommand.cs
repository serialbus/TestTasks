using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Common.Modularity
{
    public class NavigationCommand : DelegateCommand, INavigationCommand
    {

        #region Constructors

        public NavigationCommand(string text, Action executeMethod) : base(executeMethod)
        {
            Text = text;
        }
        public NavigationCommand(string text, Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            Text = text;
        }

        #endregion

        #region Fields And Properties

        public string Text { get; private set; }
        
        #endregion
    }
}
