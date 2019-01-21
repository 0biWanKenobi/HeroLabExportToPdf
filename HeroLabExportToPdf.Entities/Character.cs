using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HeroLabExportToPdf.Entities
{

    

    [Serializable, XmlRoot("character")]
    public class Character : BaseCharacter
    {
        

        [XmlAttribute("name")]
        public string HeroName { get; set; }
        [XmlAttribute("playername")]
        public string PlayerName { get; set; }

        [XmlElement("race")]
        public Attribute Race { get; set; }

        [XmlElement("alignment")]
        public Attribute Alignment { get; set; }

        [XmlArray("attributes"), XmlArrayItem("attribute")]
        public List<CharacterAttribute> Stats { get; set; }

        [XmlElement("saves")]
        public SaveRolls SaveRolls { get; set; }

        [XmlElement("maneuvers")]
        public Maneuvers Maneuvers { get; set; }

        [XmlElement("initiative")]
        public Initiative Initiative { get; set; }

        [XmlArray("skills"), XmlArrayItem("skill")]
        public List<Skill> Skills { get; set; }

        [XmlArray("feats"), XmlArrayItem("feat")]
        public List<Feat> Feats { get; set; }

        [XmlElement("size")]
        public Attribute Size { get; set; }

        [XmlElement("deity")]
        public Attribute Deity { get; set; }

        [XmlArray("types"), XmlArrayItem("type")]
        public List<Attribute> Types { get; set; } // eg Humanoid

        [XmlArray("subtypes"), XmlArrayItem("subtype")]
        public List<Attribute> Subtypes { get; set; }

        [XmlArray("favoredclasses"), XmlArrayItem("favoredclass")]
        public List<Attribute> FavoredClasses { get; set; }

        [XmlArray("classes"), XmlArrayItem("class")]
        public List<Class> Classes { get; set; }

        [XmlArray("languages"), XmlArrayItem("language")]
        public List<Attribute> Languages { get; set; }

        [XmlElement("health")]
        public Health Health { get; set; }

        [XmlElement("money")]
        public Money Money { get; set; }

        [XmlElement("personal")]
        public Personal Personal { get; set; }

        [XmlArray("defenses"), XmlArrayItem("armor")]
        public List<Armor> Defenses { get; set; }

        [XmlArray("gear"), XmlArrayItem("item")]
        public List<GearItem> Gear { get; set; }

        [XmlArray("melee"), XmlArrayItem("weapon")]
        public List<Weapon> MeleeWeapons { get; set; }
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

    public class CharacterAttribute
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("attrvalue")]
        public CharacterAttributeDetail Value { get; set; }

        [XmlElement("attrbonus")]
        public CharacterAttributeDetail Bonus { get; set; }

        [XmlElement("situationalmodifiers")]
        public SituationalModList Modifiers { get; set; }
    }



    [Serializable]
    public class TextElement
    {
        [XmlAttribute("text")]
        public string Text { get; set; }
    }

    [Serializable]
    public class TextWithValueElement : TextElement
    {

        [XmlAttribute("value")]
        public string Value { get; set; }
    }

    public class CharacterAttributeDetail : TextElement
    {

        [XmlAttribute("base")]
        public string Base { get; set; }

        [XmlAttribute("modified")]
        public string Modified { get; set; }
    }

    public class SituationalMod : TextElement
    {
        [XmlAttribute("source")]
        public string Source { get; set; }
    }

    public class SituationalModList : TextElement
    {
        [XmlElement("situationalmodifier")]
        public List<SituationalMod> SituationalMods { get; set; }
    }
    
    public class SaveRoll
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("abbr")]
        public string ShortName { get; set; }

        [XmlAttribute("save")]
        public string Save { get; set; }

        [XmlAttribute("base")]
        public string Base { get; set; }

        [XmlAttribute("fromattr")]
        public string FromAttr { get; set; }

        [XmlAttribute("fromresist")]
        public string FromResist { get; set; }

        [XmlAttribute("frommisc")]
        public string FromMisc { get; set; }

        [XmlElement("situationalmodifiers")]
        public SituationalModList SituationalModsList { get; set; }
    }

    public class SaveRolls
    {
        [XmlElement("save")]
        public List<SaveRoll> Rolls { get; set; }
        [XmlElement("allsaves")]
        public SaveRoll AllSaves { get; set; }
    }


    
    public class Skill {
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("situationalmodifiers")]
        public SituationalModList Situationalmodifiers { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("ranks")]
        public string Ranks { get; set; }

        [XmlAttribute("attrbonus")]
        public string AttrBonus { get; set; }

        [XmlAttribute("attrname")]
        public string AttrName { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute(AttributeName = "armorcheck")]
        public string ArmorCheck { get; set; } = "no";

        [XmlAttribute(AttributeName = "classskill")]
        public string ClassSkill { get; set; } = "no";

        [XmlAttribute("tools")]
        public string Tools { get; set; }

        [XmlAttribute(AttributeName = "trainedonly")]
        public string TrainedOnly { get; set; } = "no";

        [XmlAttribute(AttributeName = "usable")]
        public string Usable { get; set; } = "yes";
    }

    
    public class Feat {
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("featcategory")]
        public string FeatCategory { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("categorytext")]
        public string CategoryText { get; set; }

        [XmlAttribute("profgroup")] 
        public string ProfGroup { get; set; } = "no";

        [XmlAttribute("useradded")] 
        public string UserAdded { get; set; } = "yes";
    }

    [XmlRoot("maneuvertype")]
    public class Maneuver {
        [XmlElement("situationalmodifiers")]
        public SituationalModList Situationalmodifiers { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("bonus")]
        public string Bonus { get; set; }
        [XmlAttribute("cmb")]
        public string Cmb { get; set; }
        [XmlAttribute("cmd")]
        public string Cmd { get; set; }
    }

    [XmlRoot("maneuvers")]
    public class Maneuvers {
        [XmlElement("situationalmodifiers")]
        public SituationalModList Situationalmodifiers { get; set; }
        [XmlElement("maneuvertype")]
        public List<Maneuver> Maneuvertype { get; set; }
        [XmlAttribute("cmb")]
        public string Cmb { get; set; }
        [XmlAttribute("cmd")]
        public string Cmd { get; set; }
        [XmlAttribute("cmdflatfooted")]
        public string Cmdflatfooted { get; set; }
    }

    [XmlRoot(ElementName = "initiative")]
    public class Initiative
    {
        [XmlElement(ElementName = "situationalmodifiers")]
        public SituationalModList Situationalmodifiers { get; set; }

        [XmlAttribute(AttributeName = "total")]
        public string Total { get; set; }

        [XmlAttribute(AttributeName = "attrtext")]
        public string Attrtext { get; set; }

        [XmlAttribute(AttributeName = "misctext")]
        public string Misctext { get; set; }

        [XmlAttribute(AttributeName = "attrname")]
        public string Attrname { get; set; }
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

    
    

    public class GearItem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("quantity")]
        public string Quantity { get; set; }

        [XmlElement("weight")]
        public TextWithValueElement Weight { get; set; }

        [XmlElement("cost")]
        public TextWithValueElement Cost { get; set; }
    }

    public class Armor : GearItem
    {
        [XmlAttribute("ac")]
        public string Ac { get; set; }

        [XmlAttribute("equipped")]
        public string Equipped { get; set; }
    }


    public struct Weapon
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("categorytext")]
        public string Category { get; set; }

        [XmlAttribute("typetext")]
        public string Type { get; set; }

        [XmlAttribute("attack")]
        public string Attack { get; set; }

        [XmlAttribute("crit")]
        public string Crit { get; set; }

        [XmlAttribute("damage")]
        public string Damage { get; set; }

        [XmlAttribute("quantity")]
        public string Quantity { get; set; }

        [XmlElement("weight")]
        public TextWithValueElement Weight { get; set; }

        [XmlElement("cost")]
        public TextWithValueElement Cost { get; set; }

        [XmlElement("wepcategory")]
        public List<string> WeaponCategories { get; set; }

        [XmlElement("weptype")]
        public List<string> WeaponTypes { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
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
}
