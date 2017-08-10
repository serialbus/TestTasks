using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Framework = System.Configuration;

namespace Infrastructure.Common.Models.Configuration
{
    public static class ConfigurationManager
    {
        #region Fields And Properties

        public static string PathToRepositoryFile
        {
            get
            {
                var path = Framework::ConfigurationManager.AppSettings["PathToRepositoryFile"];
                // Если пусть пустой (по умолчанию), то файл репозитория ищем в директории приложения
                return String.IsNullOrEmpty(path) ? Environment.CurrentDirectory : path;
            }
            set
            {
                Framework::ConfigurationManager.AppSettings["PathToRepositoryFile"] = value;
            }
        }
        
        #endregion

        #region Methods
        #endregion
    }
}
