using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.Common.Models
{
    public abstract class ModelBase
    {
        [XmlElement]
        public string Name { get; set; }
    }
}
