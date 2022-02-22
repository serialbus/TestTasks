using Prism.Events;

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
