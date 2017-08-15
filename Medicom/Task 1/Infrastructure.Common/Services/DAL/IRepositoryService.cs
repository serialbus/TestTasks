using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models.DAL
{
    public interface IRepositoryService
    {
        IList<ModelBase> Items { get; }

        bool IsConnect { get; }
        /// <summary>
        /// Получает данные из источника
        /// </summary>
        void Open();
        void Close();
        /// <summary>
        /// Сохраняет данные в источник 
        /// </summary>
        void SaveChanges();
    }
}
