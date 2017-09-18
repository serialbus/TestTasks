using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Common.Module
{
	public class NavigationMenu : INavigationMenu
	{
		public NavigationMenu(string text, ICommand command)
		{
			Text = text;
			Command = command;
		}

		public ICommand Command { get; private set; }

		public string Text { get; private set; }
	}
}
