using OrdersModule.ViewModels;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrdersModule.Views
{
	/// <summary>
	/// Interaction logic for OrderEditorDialogView.xaml
	/// </summary>
	public partial class OrderEditorDialogView : UserControl, IInteractionRequestAware
	{
		#region Constructor

		public OrderEditorDialogView()
		{
			InitializeComponent();
			if (DataContext == null)
				DataContext = new OrderEditorDialogViewModel(null, null, null, null);
		}

		#endregion

		#region Fields And Properties

		private OrderEditorDialogViewModel ViewModel
		{
			get { return DataContext as OrderEditorDialogViewModel; }
			set
			{
				_DatePicker.BlackoutDates.Clear();
				DataContext = value;
				if (value != null)
					value.PropertyChanged += EventHandlre_ViewModel_PropertyChanged;
			}
		}

		public Action FinishInteraction
		{
			get { return ViewModel?.FinishInteraction; }
			set
			{
				if (ViewModel != null && ViewModel is OrderEditorDialogViewModel)
					ViewModel.FinishInteraction = value;
			}
		}

		public INotification Notification
		{
			get { return ViewModel == null ? new Confirmation { Confirmed = false } :
					ViewModel.Confirmation; }
			set
			{
				var confirmation = (IConfirmation)value;
					ViewModel = (OrderEditorDialogViewModel)confirmation.Content;
			}
		}

		#endregion

		#region Event Handlers

		private void EventHandlre_ViewModel_PropertyChanged(object sender,
			System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "BlackoutDates":
					{
						_DatePicker.BlackoutDates.Clear();

						foreach (var range in ViewModel.BlackoutDates)
						{
							this._DatePicker.BlackoutDates.Add(range);
						}
						break;
					}
			}
		}

		#endregion
	}
}
