using Infrastructure.Common.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Services
{
    public class OfficesService : IOfficesService
    {
        static OfficesService()
        {
            _Offices = new List<Office>();

            var office = new Office();
            office.Name = "Москва";
            office.AddDailyMeetingTime(new TimeSpan(8, 0, 0)); // 8 часов утра
            office.AddDailyMeetingTime(new TimeSpan(10, 0, 0)); // 10 часов утра
            office.AddDailyMeetingTime(new TimeSpan(12, 0, 0)); // 12 часов утра
            _Offices.Add(office);

            office = new Office();
            office.Name = "Саратов";
            office.AddDailyMeetingTime(new TimeSpan(9, 0, 0)); // 9 часов утра
            office.AddDailyMeetingTime(new TimeSpan(11, 0, 0)); // 11 часов утра
            office.AddDailyMeetingTime(new TimeSpan(13, 0, 0)); // 13 часов 
            _Offices.Add(office);

            office = new Office();
            office.Name = "Волгоград";
            office.AddDailyMeetingTime(new TimeSpan(14, 0, 0)); // 14 часов
            office.AddDailyMeetingTime(new TimeSpan(16, 0, 0)); // 16 часов
            office.AddDailyMeetingTime(new TimeSpan(18, 0, 0)); // 18 часов
            _Offices.Add(office);
        }

        public OfficesService()
        {
        }

        private static List<Office> _Offices;

        public IEnumerable<Office> Offices
        {
            get { return _Offices; }
        }

        public void AddOffice(Office office)
        {
            if (_Offices.Exists(x => x.Uid == office.Uid))
                throw new ArgumentException(String.Format(
                    "Невозможно добавить офис. Офис с UID={0} существует", office.Uid),
                    "office");

            _Offices.Add(office);
            //Формируем событие
            EventsService.Events.GetEvent<OfficeWasAddedEvent>().Publish(office);
        }
    }
}
