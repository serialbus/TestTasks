using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.Common.Models
{
    [Serializable]
    public class WebAccount: ModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// класс Uri не поддерживает сереилизацию XML
        /// </remarks>
        [XmlIgnore]
        public Uri Url { get; set; }
        [XmlElement("Url")]
        public string UrlAsString
        {
            get { return Url != null ? Url.AbsoluteUri : null; }
            set { Url = String.IsNullOrEmpty(value) ? null : new Uri(value); }
        }
    }
}
