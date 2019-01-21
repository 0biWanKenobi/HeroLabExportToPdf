using System;
using System.Xml.Serialization;

namespace HeroLabExportToPdf.Entities
{
    [Serializable]
    public class PageSize
    {
        [XmlAttribute("width")]
        public double Width { get; set; }

        [XmlAttribute("height")]
        public double Height { get; set; }
    }
}
