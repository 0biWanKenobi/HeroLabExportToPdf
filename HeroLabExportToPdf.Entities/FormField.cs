using System;
using System.Xml.Serialization;

namespace HeroLabExportToPdf.Entities
{
    [Serializable]
    public class FormField
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("x")]
        public double X {get; set;}

        [XmlAttribute("y")]
        public double Y {get; set;}

        [XmlAttribute("width")]
        public double Width {get; set;}

        [XmlAttribute("height")]
        public double Height {get; set;}

        [XmlAttribute("fontsize")]
        public double FontSize { get; set; }

        [XmlAttribute("page")]
        public int Page { get; set; }

        [XmlAttribute("font")]
        public string Font { get; set; }

        [XmlAttribute("type")]
        public int Type { get; set; }
    }
}
