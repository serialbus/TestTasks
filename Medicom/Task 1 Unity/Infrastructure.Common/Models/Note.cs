using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.Common.Models
{
    [Serializable]
    public class Note: ModelBase
    {
        [XmlElement]
        public string Content { get; set; }

        public override string AsString()
        {
            return String.Format("Наименование: {0} Содержание: {1}", Name, Content);
        }
    }
}
