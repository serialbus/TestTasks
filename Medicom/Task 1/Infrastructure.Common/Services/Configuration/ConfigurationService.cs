using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Infrastructure.Common.Services.Configuration
{
    [Export(typeof(IConfigurationService))]
    public class ConfigurationService : IConfigurationService
    {
        #region Constructors
        
        [ImportingConstructor]
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
                return String.IsNullOrEmpty(path) ? Environment.CurrentDirectory : path;
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
