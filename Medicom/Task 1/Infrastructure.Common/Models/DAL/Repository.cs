using Infrastructure.Common.Models.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Infrastructure.Common.Models.DAL
{
    public class Repository : IRepository
    {
        #region Fields And Properties

        private static object SyncRoot = new object();

        private List<Note> _Notes = new List<Note>();
        public IList<Note> Notes => _Notes;

        private List<WebAccount> _WebAccounts = new List<WebAccount>();
        public IList<WebAccount> WebAccounts => _WebAccounts;

        private List<CreditCard> _CreditCards = new List<CreditCard>();
        public IList<CreditCard> CreditCards => _CreditCards;

        #endregion

        #region

        public void Pull()
        {
            ReadDataBaseFile(ConfigurationManager.PathToRepositoryFile);
        }

        public void SaveChanges()
        {
            WriteDataBaseFile(ConfigurationManager.PathToRepositoryFile);
        }

        private void ReadDataBaseFile(string pathToFile)
        {
            if (!File.Exists(pathToFile))
            {
                //TODO: Файл репозитория не найден, формируем сообщение пользователю через событие
                throw new Exception("Файл базы данных не найден");
            }

            using (var fs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = XmlReader.Create(fs))
                {
                    var serializer = new XmlSerializer(typeof(DataBaseModel));
                    if (serializer.CanDeserialize(reader))
                    {
                        var db = (DataBaseModel)serializer.Deserialize(reader);

                        lock (SyncRoot)
                        {
                            _Notes = db.Notes.ToList();
                            _WebAccounts = db.WebAccounts.ToList();
                            _CreditCards = db.CreaditCards.ToList();
                        }
                    }
                    else
                    {
                        //TODO: не удалось дессериализовать файл базы данных, формируем сообщение пользователю
                        throw new Exception("Не удалось дессериализовать файл базы данных");
                    }
                }
            }
        }

        private void WriteDataBaseFile(string pathToFile)
        {
            var serializer = new XmlSerializer(typeof(DataBaseModel));

            using (var fs = new FileStream(pathToFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(fs, new DataBaseModel
                {
                    CreaditCards = _CreditCards.ToArray(),
                    Notes = _Notes.ToArray(),
                    WebAccounts = _WebAccounts.ToArray()
                });
            }
        }

        #endregion
    }
}
