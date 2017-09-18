using Infrastructure.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
