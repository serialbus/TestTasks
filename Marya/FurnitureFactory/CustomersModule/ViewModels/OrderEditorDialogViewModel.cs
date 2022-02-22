using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace OrdersModule.ViewModels
{
    public class OrderEditorDialogViewModel : OrderViewModel
    {
        #region Constructors

        public OrderEditorDialogViewModel(IOfficesService officesService, ICustomerService customerService,
            IOrdersService ordersService, Order order)
            : base(officesService, customerService, order)
        {
            OrdersService = ordersService;

            _Confirmation = new Confirmation { Confirmed = false, Content = this };

            _ConfirmCommand = new DelegateCommand(OnConfirm, CanConfirm);
            _RejectCommand = new DelegateCommand(OnReject);
            _SelectedTimeCommand = new DelegateCommand<DateTime?>(OnSelectedTime);

            SelectedOffice = null;
            SelectedDate = DateTime.Now;
            SelectedTime = null;
        }

        #endregion

        #region Fields And Properties

        protected readonly IOrdersService OrdersService;
        public IEnumerable<Office> Offices
        {
            get { return OfficesService == null ? Enumerable.Empty<Office>() : OfficesService.Offices; }
        }
        public Office SelectedOffice
        {
            get { return Office; }
            set
            {
                OfficeId = value != null ? value.Uid : Guid.Empty;
                OnPropertyChanged(() => SelectedOffice);
                OnPropertyChanged(() => IsEnabledSelectionDate);
                OnPropertyChanged(() => BlackoutDates);
                OnPropertyChanged(() => DaySchedule);
            }
        }
        public DateTime DateStart
        {
            get { return DateTime.Now; }
            set { }
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
            get { return OfficeId != Guid.Empty; }
        }
        public IEnumerable<Tuple<DateTime, bool>> DaySchedule
        {
            get
            {
                List<Tuple<DateTime, bool>> list = new List<Tuple<DateTime, bool>>();

                if (SelectedOffice == null)
                    return list;

                if (SelectedDate != null)
                {
                    var reserved = OrdersService.Orders.Where(order => order.ExecutorId == SelectedOffice.Uid &&
                                                         order.ExecutionTime.HasValue &&
                                                         order.ExecutionTime.Value.Date == SelectedDate.Value.Date)
                                                .Select(order => order.ExecutionTime.Value.TimeOfDay);

                    foreach (var time in SelectedOffice.DailySchedule)
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
        public IEnumerable<CalendarDateRange> BlackoutDates
        {
            get
            {
                var blackoutDates = new List<CalendarDateRange>();

                if (SelectedOffice != null && SelectedDate != null)
                {
                    // Выбираем все заказы за текущий предыдущий и следующий месяцы, т.к на 
                    // календаре они могут отображатся вместе с текущим
                    var minDate = SelectedDate.Value.AddMonths(-1);
                    var maxDate = SelectedDate.Value.AddMonths(1);

                    var orders = OrdersService.Orders.Where(order => order.ExecutorId == SelectedOffice.Uid &&
                                        order.ExecutionTime.HasValue &&
                                        order.ExecutionTime.Value.Date >= minDate.Date && order.ExecutionTime.Value.Date < maxDate);

                    for (var date = minDate; date <= maxDate; date = date.AddDays(1))
                    {
                        var dailyOrders = orders.Where(order => order.ExecutionTime.HasValue && order.ExecutionTime.Value.Date == date.Date);
                        var c = dailyOrders.Select(order => order.ExecutionTime.Value.TimeOfDay).Intersect(SelectedOffice.DailySchedule).Count();
                        if (c == SelectedOffice.DailySchedule.Count())
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

        public Action FinishInteraction { get; set; }

        private Confirmation _Confirmation;
        public IConfirmation Confirmation
        {
            get { return _Confirmation; }
        }

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
            ExecutionTime = SelectedDate.Value.Date.Add(SelectedTime.Value);
            FinishInteraction?.Invoke();
        }
        private bool CanConfirm()
        {
            return true;
            //return OfficeId != Guid.Empty && ExecutionTime != null;
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