using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.Common.Models
{
    [Serializable]
    public class WebAccount
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public Uri Url { get; set; }
    }
}
