using Infrastructure.Common.Models;
using System.Collections.Generic;

namespace Infrastructure.Common.Services
{
    /// <summary>
    /// Интерфейс для работы с БД.
    /// </summary>
    public interface ICustomerService
    {
        IList<Customer> Customers { get; }
    }
}
