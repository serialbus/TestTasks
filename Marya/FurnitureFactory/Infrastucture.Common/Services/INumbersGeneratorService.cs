using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
	public interface INumbersGeneratorService
	{
		ServiceStatus Status { get; }
		int GenerateOrderNumber();
	}
}
