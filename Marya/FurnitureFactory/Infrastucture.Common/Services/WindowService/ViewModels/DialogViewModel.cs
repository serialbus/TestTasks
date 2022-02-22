using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Infrastructure.Common.Services.WindowService.ViewModels
{
    public class DialogViewModel : BindableBase
    {
        public DialogViewModel()
        {
            NotificationRequest = new InteractionRequest<INotification>();
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            CustomPopupViewRequest = new InteractionRequest<INotification>();
        }

        public InteractionRequest<INotification> NotificationRequest { get; private set; }
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }
        public InteractionRequest<INotification> CustomPopupViewRequest { get; private set; }
    }
}
