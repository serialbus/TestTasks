using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.Common.Models
{
    [Serializable]
    public class CreditCard: ModelBase
    {
        [XmlElement]
        public DateTime ExpirationDate { get; set; }
    }
}
