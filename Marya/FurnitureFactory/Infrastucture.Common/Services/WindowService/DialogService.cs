using Infrastructure.Common.Services.WindowService.View;
using Infrastructure.Common.Services.WindowService.ViewModels;
using Prism.Interactivity.DefaultPopupWindows;
using Prism.Interactivity.InteractionRequest;

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
