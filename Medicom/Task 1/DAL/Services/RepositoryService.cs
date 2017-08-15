using Infrastructure.Common.Events.DAL;
using Infrastructure.Common.Models;
using Infrastructure.Common.Models.Configuration;
using Infrastructure.Common.Models.DAL;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DAL.Services
{
    public class RepositoryService : IRepositoryService
    {
        #region Constructors

        public RepositoryService(IUnityContainer container)
        {
            _ConfigurationService = container.Resolve<IConfigurationService>();
            _EventAggregator = container.Resolve<IEventAggregator>();
        }

        #endregion

        #region Fields And Properties

        private static object SyncRoot = new object();
        private readonly IConfigurationService _ConfigurationService;
        private readonly IEventAggregator _EventAggregator;

        private List<ModelBase> _Items;
        public IList<ModelBase> Items => _Items;

        public bool IsConnect => _Items != null;

        #endregion

        #region

        public void Open()
        {
            ReadDataBaseFile(_ConfigurationService.PathToRepositoryFile);
            OnConnectionStatusChanged();
        }

        public void Close()
        {
            _Items = null;
            OnConnectionStatusChanged();
        }

        public void SaveChanges()
        {
            if(IsConnect)
                WriteDataBaseFile(_ConfigurationService.PathToRepositoryFile);
        }

        private void ReadDataBaseFile(string pathToFile)
        {
            if (!File.Exists(pathToFile))
            {
                //TODO: Файл репозитория не найден, формируем сообщение пользователю через событие
                //throw new Exception("Файл базы данных не найден");
                File.Create(pathToFile);
                _Items = new List<ModelBase>();
                return;
            }

            try
            {
                var content = File.ReadAllText(pathToFile);
                var result = JsonConvert.DeserializeObject<ModelBase[]>(content, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                _Items = result.ToList();
            }
            catch
            {
                _Items = new List<ModelBase>();
            }
        }

        private void WriteDataBaseFile(string pathToFile)
        {
            var json = JsonConvert.SerializeObject(Items.ToArray(), new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            File.WriteAllText(pathToFile, json);
        }

        private void OnConnectionStatusChanged()
        {
            _EventAggregator.GetEvent<DbConnectionStatusChangedEvent>().Publish();

        }
        #endregion
    }
}
