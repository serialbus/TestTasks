using Infrastructure.Common.Models;
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
	/// Interaction logic for OrderDetailsView.xaml
	/// </summary>
	public partial class OrderDetailsView : UserControl, IInteractionRequestAware
	{
		#region Constructors

		public OrderDetailsView()
		{
			InitializeComponent();
			DataContext = new OrderDetailsViewModel();
			ViewModel.PropertyChanged += EventHandlre_ViewModel_PropertyChanged;
		}

		#endregion

		#region Fields And Properties

		public OrderDetailsViewModel ViewModel
		{
			get { return (OrderDetailsViewModel)DataContext; }
		}

		public Action FinishInteraction
		{
			get { return ViewModel.FinishInteraction; }
			set { ViewModel.FinishInteraction = value; }
		}

	
		public INotification Notification
		{
			get { return ViewModel.Confirmation; }
			set
			{
				ViewModel.Confirmation = (IConfirmation)value;
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
