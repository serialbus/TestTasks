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
        [XmlElement]
        public int Number { get; set; }

        public override string AsString()
        {
            return string.Format("Наименование: {0} Номер: {1} Дата окончания: {2}",
                Name, Number, ExpirationDate.ToShortDateString());
        }
    }
}
