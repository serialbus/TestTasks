using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services.WindowService.ViewModels
{
	public class DialogViewModel: BindableBase
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
