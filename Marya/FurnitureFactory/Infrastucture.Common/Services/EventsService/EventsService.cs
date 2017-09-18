using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
	public class EventsService
	{
		static EventsService()
		{
			Events = new EventAggregator();
		}

		public static IEventAggregator Events { get; private set; }
	}
}
