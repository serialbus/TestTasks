using Infrastructure.Common.Models;
using System.Collections.Generic;

namespace Infrastructure.Common.Services
{
    public interface IOfficesService
    {
        /// <summary>
        /// Возвращает список офисов
        /// </summary>
        IEnumerable<Office> Offices { get; }
        /// <summary>
        /// Добавляет новый офис
        /// </summary>
        /// <param name="office"></param>
        void AddOffice(Office office);
    }
}
