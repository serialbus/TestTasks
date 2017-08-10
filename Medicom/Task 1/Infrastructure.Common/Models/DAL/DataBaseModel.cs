using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.Common.Models.DAL
{
    [Serializable]
    [XmlRoot]
    public class DataBaseModel
    {
        [XmlArray]
        public Note[] Notes { get; set; }
        [XmlArray]
        public WebAccount[] WebAccounts { get; set; }
        [XmlArray]
        public CreditCard[] CreaditCards { get; set; }
    }
}
