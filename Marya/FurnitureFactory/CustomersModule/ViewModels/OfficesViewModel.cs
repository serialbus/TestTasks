using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersModule.ViewModels
{
	public class OfficesViewModel: BindableBase
	{
		#region Constructor

		public OfficesViewModel(IUnityContainer container)
		{
			OfficesService = container.Resolve<IOfficesService>();
			EventsService.Events.GetEvent<OfficeWasAddedEvent>().Subscribe(OnAddingOffice);
			Offices = new ObservableCollection<Office>(OfficesService.Offices);
			SelectedOffice = Offices.FirstOrDefault();
		}

		#endregion

		#region Fields And Properties

		private readonly IOfficesService OfficesService;
		public ObservableCollection<Office> Offices { get; private set; }
		private Office _SelectedOffice;
		public Office SelectedOffice
		{
			get { return _SelectedOffice; }
			set
			{
				_SelectedOffice = value;
				OnPropertyChanged(() => SelectedOffice);
				OnPropertyChanged(() => OffeceSchedule);
			}
		}
		public IEnumerable<String> OffeceSchedule
		{
			get
			{
				if (SelectedOffice == null)
					return Enumerable.Empty<String>();
				else
				{
					return _SelectedOffice.DailySchedule.Select(time =>
						 new DateTime().AddTicks(time.Ticks).ToShortTimeString());
				}
			}
		}

		#endregion

		#region Methods

		private void OnAddingOffice(Office office)
		{
			Offices.Add(office);
		}

		#endregion
	}
}
