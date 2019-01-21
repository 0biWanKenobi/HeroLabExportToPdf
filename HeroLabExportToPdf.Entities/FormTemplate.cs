using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HeroLabExportToPdf.Entities
{
    [Serializable, XmlRoot(ElementName = "Character")]
    public class FormTemplate
    {
        [XmlElement("PageSize")]
        public PageSize PageSize { get; set; }

        [XmlArray(ElementName = "FormFields"), XmlArrayItem("FormField")]
        public List<FormField> FormFields { get; set; }
    }
}
