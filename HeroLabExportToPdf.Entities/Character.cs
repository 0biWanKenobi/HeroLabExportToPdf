﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HeroLabExportToPdf.Entities
{
    [Serializable, XmlRoot(ElementName = "character")]
    public class Character : BaseCharacter
    {
        [XmlAttribute("name")]
        public string HeroName { get; set; }
        [XmlAttribute("playername")]
        public string PlayerName { get; set; }

        [XmlElement(ElementName = "race")]
        public Attribute Race { get; set; }

        [XmlElement(ElementName = "alignment")]
        public Attribute Alignment { get; set; }

        [XmlElement(ElementName = "size")]
        public Attribute Size { get; set; }

        [XmlElement("deity")]
        public Attribute Deity { get; set; }

        [XmlArray(ElementName = "types"), XmlArrayItem("type")]
        public List<Attribute> Types { get; set; } // eg Humanoid

        [XmlArray(ElementName = "subtypes"), XmlArrayItem("subtype")]
        public List<Attribute> Subtypes { get; set; }

        [XmlArray(ElementName = "favoredclasses"), XmlArrayItem("favoredclass")]
        public List<Attribute> FavoredClasses { get; set; }

        [XmlArray(ElementName = "classes"), XmlArrayItem("class")]
        public List<Class> Classes { get; set; }

        [XmlArray(ElementName = "languages"), XmlArrayItem("language")]
        public List<Attribute> Languages { get; set; }

        [XmlElement("health")]
        public Health Health { get; set; }

        [XmlElement("money")]
        public Money Money { get; set; }

        [XmlElement("personal")]
        public Personal Personal { get; set; }

        [XmlArray("attributes"), XmlArrayItem("attribute")]
        public List<ClassAttribute> Attributes { get; set; }
    }



    [Serializable]
    public class Attribute
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }
    }

    [Serializable]
    public class Class : Attribute
    {
        [XmlAttribute("level")]
        public string Level { get; set; }
    }

    [Serializable]
    public struct Health
    {
        [XmlAttribute("hitdice")]
        public string HitDice { get; set; }

        [XmlAttribute("hitpoints")]
        public string HitPoints { get; set; }

    }

    [Serializable]
    public struct Money
    {
        [XmlAttribute("gold")]
        public int Gold { get; set; }

        [XmlAttribute("silver")]
        public int Silver{get; set;}
        [XmlAttribute("copper")]
        public int Copper{get; set;}
    }

    [Serializable]
    public struct Personal
    {
        [XmlAttribute("gender")]
        public string Gender { get; set; }
        [XmlAttribute("age")]
        public int Age { get; set; }
        [XmlAttribute("hair")]
        public string Hair { get; set; }
        [XmlAttribute("eyes")]
        public string Eyes { get; set; }
        [XmlAttribute("skin")]
        public string Skin { get; set; }
    }

    [Serializable]
    public class ClassAttribute : Attribute
    {
        [XmlElement("attrvalue")]
        public AttributeValue ClassAttributeValue { get; set; }

        [XmlElement("attrbonus")]
        public AttributeBonus ClassAttributeBonus { get; set; }

        [Serializable]
        public struct AttributeValue
        {
            [XmlAttribute("text")]
            public int Text { get; set; }

            [XmlAttribute("base")]
            public int Base { get; set; }

            [XmlAttribute("modified")]
            public int Modified { get; set; }
        }

        [Serializable]
        public struct AttributeBonus
        {
            [XmlAttribute("text")]
            public int Text { get; set; }

            [XmlAttribute("base")]
            public string Base { get; set; }

            [XmlAttribute("modified")]
            public string Modified { get; set; }
        }
    }

    [Serializable]
    public class TextAttribute
    {
        [XmlAttribute("text")]
        public string Text { get; set; }
    }
}