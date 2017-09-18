using Infrastructure.Common.Services.WindowService.View;
using Infrastructure.Common.Services.WindowService.ViewModels;
using Prism.Interactivity;
using Prism.Interactivity.DefaultPopupWindows;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
	public static class DialogService
	{
		#region Constructor

		static DialogService()
		{
		}

		#endregion

		#region Fields And Properties
		#endregion

		#region Methods

		public static void ShowMessageBox(string title, string message)
		{
			DefaultNotificationWindow window = new DefaultNotificationWindow();
			
			var view = new DialogView();
			var vm = (DialogViewModel)view.DataContext;
			window.Content = view;

			vm.NotificationRequest.Raise(new Notification { Title = title, Content = message });
		}

		#endregion
	}
}
