using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
	public enum ServiceStatus
	{
		/// <summary>
		/// Сервис работает
		/// </summary>
		Ready,
		/// <summary>
		/// Свервис не работает
		/// </summary>
		OutOfService
	}
}
