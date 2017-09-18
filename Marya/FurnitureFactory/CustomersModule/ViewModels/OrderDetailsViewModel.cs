using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace OrdersModule.ViewModels
{
	public class OrderDetailsViewModel: BindableBase
	{
		#region Constructors

		public OrderDetailsViewModel()
		{
			_ConfirmCommand = new DelegateCommand(OnConfirm, CanConfirm)
				.ObservesProperty(() => SelectedOffice)
				.ObservesProperty(() => SelectedCustomer)
				.ObservesProperty(() => SelectedTime);
			_RejectCommand = new DelegateCommand(OnReject);
			_SelectedTimeCommand = new DelegateCommand<DateTime?>(OnSelectedTime);

			Custormers = new CustomorsService().Customers;
			SelectedCustomer = null;
			OfficesService = new OfficesService();
			SelectedOffice = null;
			OrdersServece = new OrdersService();

			SelectedDate = DateTime.Now;
			SelectedTime = null;
		}

		#endregion

		#region Fields And Properties

		private IOrdersService OrdersServece { get; set; }
		public Order Order { get; set; }
		public Action FinishInteraction { get; set; }
		private IConfirmation _Confirmation;
		public IConfirmation Confirmation
		{
			get { return _Confirmation; }
			set
			{
				_Confirmation = value;
				if (_Confirmation != null && _Confirmation.Content != null)
				{
					Order = (Order)_Confirmation.Content;
					SelectedTime = null;
					SelectedCustomer = null;
					SelectedOffice = null;
					SelectedDate = DateTime.Now;
					OnPropertyChanged(() => Order);
					OnPropertyChanged(() => OrderNumber);
					OnPropertyChanged(() => BlackoutDates);
					OnPropertyChanged(() => SelectedDate);
					OnPropertyChanged(() => SelectedCustomer);
					OnPropertyChanged(() => SelectedOffice);
					OnPropertyChanged(() => SelectedTime);
				}
			}
		}
		public string OrderNumber
		{
			get
			{
				return Order == null ? String.Empty : Order.OrderNumber.ToString("000000");
			}
		}
		public IEnumerable<Customer> Custormers { get; private set; }
		private Customer _SelectedCustomer;
		public Customer SelectedCustomer
		{
			get { return _SelectedCustomer; }
			set
			{
				_SelectedCustomer = value;
				OnPropertyChanged(() => SelectedCustomer);
			}
		}
		public IOfficesService OfficesService { get; private set; }
		public IEnumerable<Office> Offices { get { return OfficesService?.Offices; } }
		private Office _SelectedOffice;
		public Office SelectedOffice
		{
			get { return _SelectedOffice; }
			set
			{
				_SelectedOffice = value;
				OnPropertyChanged(() => SelectedOffice);
				OnPropertyChanged(() => IsEnabledSelectionDate);
				OnPropertyChanged(() => BlackoutDates);
				OnPropertyChanged(() => DaySchedule);
			}
		}
		public DateTime DateStart
		{
			get { return DateTime.Now; }
			set	{ }
		}
		public IEnumerable<CalendarDateRange> BlackoutDates
		{
			get
			{
				var blackoutDates = new List<CalendarDateRange>();

				if (_SelectedOffice != null && SelectedDate != null)
				{
					// Выбираем все заказы за текущий предыдущий и следующий месяцы, т.к на 
					// календаре они могут отображатся вместе с текущим
					var minDate = SelectedDate.Value.AddMonths(-1);
					var maxDate = SelectedDate.Value.AddMonths(1);

					var orders = OrdersServece.Orders.Where(order => order.ExecutorId == _SelectedOffice.Uid &&
										order.ExecutionTime.HasValue &&
										order.ExecutionTime.Value.Date >= minDate.Date && order.ExecutionTime.Value.Date < maxDate);

					for (var date = minDate; date <= maxDate; date = date.AddDays(1))
					{
						var dailyOrders = orders.Where(order => order.ExecutionTime.HasValue && order.ExecutionTime.Value.Date == date.Date);
						var c = dailyOrders.Select(order => order.ExecutionTime.Value.TimeOfDay).Intersect(_SelectedOffice.DailySchedule).Count();
						if (c == _SelectedOffice.DailySchedule.Count())
						{
							// День польностью занят
							blackoutDates.Add(new CalendarDateRange(date));
							// Если выбраная дата не доступна (полностью занята) сбрасываем
							if (date == SelectedDate)
								SelectedDate = null;
						}
					}
				}
				return blackoutDates;
			}
		}
		private DateTime? _SelectedDate;
		public DateTime? SelectedDate
		{
			get { return _SelectedDate; }
			set
			{
				_SelectedDate = value;
				OnPropertyChanged(() => BlackoutDates);
				OnPropertyChanged(() => SelectedDate);
				OnPropertyChanged(() => DaySchedule);
			}
		}
		public bool IsEnabledSelectionDate
		{
			get { return _SelectedOffice != null; }
		}
		public IEnumerable<Tuple<DateTime, bool>> DaySchedule
		{
			get
			{
				List<Tuple<DateTime, bool>> list = new List<Tuple<DateTime, bool>>();

				if (_SelectedOffice == null)
					return list;

				if (SelectedDate != null)
				{
					var reserved = OrdersServece.Orders.Where(order => order.ExecutorId == SelectedOffice.Uid &&
														 order.ExecutionTime.HasValue &&
														 order.ExecutionTime.Value.Date == SelectedDate.Value.Date)
												.Select(order => order.ExecutionTime.Value.TimeOfDay);

					foreach (var time in _SelectedOffice.DailySchedule)
					{
						var busy = !reserved.Contains(time);
						list.Add(new Tuple<DateTime, bool>(new DateTime(time.Ticks), busy));
					}
				}
				return list;
			}
		}
		private TimeSpan? _SelectedTime;
		public TimeSpan? SelectedTime
		{
			get { return _SelectedTime; }
			set
			{
				_SelectedTime = value;
				OnPropertyChanged(() => SelectedTime);
			}
		}

		#endregion

		#region Methods
		#endregion

		#region Commands

		private DelegateCommand _ConfirmCommand;
		public ICommand ConfirmCommand
		{
			get { return _ConfirmCommand; }
		}
		private void OnConfirm()
		{
			Confirmation.Confirmed = true;
			Order.CustomerId = SelectedCustomer.Uid;
			Order.ExecutionTime = SelectedDate.Value.Date.Add(_SelectedTime.Value);
			Order.ExecutorId = SelectedOffice.Uid;

			FinishInteraction?.Invoke();
		}
		private bool CanConfirm()
		{
			return SelectedCustomer != null && SelectedOffice != null && SelectedTime != null;
		}

		private DelegateCommand _RejectCommand;
		public ICommand RejectCommand
		{
			get { return _RejectCommand; }
		}
		private void OnReject()
		{
			Confirmation.Confirmed = false;
			FinishInteraction?.Invoke();
		}

		private DelegateCommand<DateTime?> _SelectedTimeCommand;
		public ICommand SelectedTimeCommand
		{
			get { return _SelectedTimeCommand; }
		}
		private void OnSelectedTime(DateTime? time)
		{
			if (time.HasValue)
				SelectedTime = time.Value.TimeOfDay;
		}

		#endregion
	}
}
