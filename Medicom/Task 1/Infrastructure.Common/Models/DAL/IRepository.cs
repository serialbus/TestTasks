using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models.DAL
{
    public interface IRepository
    {
        IList<Note> Notes { get; }
        IList<WebAccount> WebAccounts { get; }
        IList<CreditCard> CreditCards { get; }

        /// <summary>
        /// Получает данные из источника
        /// </summary>
        void Pull();
        /// <summary>
        /// Сохраняет данные в источник 
        /// </summary>
        void SaveChanges();
    }
}
