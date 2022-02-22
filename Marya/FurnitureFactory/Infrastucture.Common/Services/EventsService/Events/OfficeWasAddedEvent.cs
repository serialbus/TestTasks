using Infrastructure.Common.Models;
using Prism.Events;

namespace Infrastructure.Common.Services
{
    /// <summary>
    /// Собитие возникает при добалении нового офиса.
    /// </summary>
    public class OfficeWasAddedEvent : PubSubEvent<Office>
    {
    }
}
