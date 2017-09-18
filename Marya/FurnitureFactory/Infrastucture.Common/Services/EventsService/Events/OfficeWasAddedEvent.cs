using Infrastructure.Common.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
	/// <summary>
	/// Собитие возникает при добалении нового офиса.
	/// </summary>
	public class OfficeWasAddedEvent: PubSubEvent<Office>
	{
	}
}
