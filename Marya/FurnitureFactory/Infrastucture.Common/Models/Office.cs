using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Models
{
    public class Office
    {
        #region Constructor

        public Office()
        {
            Uid = Guid.NewGuid();
            _DailySchedule = new List<TimeSpan>();
        }

        #endregion

        #region Fields And Properties

        public Guid Uid { get; private set; }
        public String Name { get; set; }

        private List<TimeSpan> _DailySchedule;
        /// <summary>
        /// Шаблон для создания суточного расписания работы офиса
        /// </summary>
        public IEnumerable<TimeSpan> DailySchedule
        {
            get { return _DailySchedule; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Добавляет новую точку проведения замера в шаблон дневного расписание
        /// работы офиса
        /// </summary>
        /// <param name="time"></param>
        public void AddDailyMeetingTime(TimeSpan time)
        {
            if (time.TotalSeconds >= (new TimeSpan(24, 0, 0)).TotalSeconds)
                throw new ArgumentOutOfRangeException("AddDailyMeetingTime",
                    "Невозможно добавить точку в расписание суток. Значение больше чем время суток");

            if (_DailySchedule.Exists(t => time == t))
                throw new ArgumentException(
                    "Невозможно добавить точку в расписание суток. Точка с указынным временем существует",
                    "AddDailyMeetingTime");

            _DailySchedule.Add(time);
        }

        #endregion
    }
}
