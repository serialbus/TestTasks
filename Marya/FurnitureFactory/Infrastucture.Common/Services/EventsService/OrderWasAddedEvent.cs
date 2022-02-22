using Infrastructure.Common.Models;
using Prism.Events;

namespace Infrastructure.Common.Services
{
    public class OrderWasAddedEvent : PubSubEvent<Order>
    {
    }
}
