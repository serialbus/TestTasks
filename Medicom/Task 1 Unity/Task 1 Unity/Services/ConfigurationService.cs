using Infrastructure.Common.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicom.ViewModels.Services
{
    public class ConfigurationService : IConfigurationService
    {
        #region Constructors

        public ConfigurationService()
        { }

        #endregion

        #region Fields And Properties

        public string PathToRepositoryFile
        {
            get
            {
                var path = ConfigurationManager.AppSettings["PathToRepositoryFile"];
                // Если пусть пустой (по умолчанию), то файл репозитория ищем в директории приложения
                return String.IsNullOrEmpty(path) ? Path.Combine(Environment.CurrentDirectory, "db.json")  : path;
            }
            set
            {
                ConfigurationManager.AppSettings["PathToRepositoryFile"] = value;
            }
        }

        #endregion

        #region Methods
        #endregion
    }
}
