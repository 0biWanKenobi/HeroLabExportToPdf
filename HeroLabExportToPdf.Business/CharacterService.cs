using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using HeroLabExportToPdf.Entities;

namespace HeroLabExportToPdf.Business
{
    public class CharacterService : ICharacterService<Character>
    {
        private readonly string _characterFile;
        
        public Character Character { get; private set; }

        

        public CharacterService(string characterFile)
        {
            _characterFile = characterFile;
            Load();
        }

        public CharacterService()
        {
            Character = new Character();
        }

        private void Load()
        {
            using (var reader = XmlReader.Create(_characterFile))
            {
                var xmlCharacter = XDocument.Load(reader)
                    .Root?
                    .Element("public")?
                    .Element("character");
                Character = Deserialize(xmlCharacter);
            }
        }

        private static Character Deserialize(XElement character)
        {
            var deserializer = new XmlSerializer(typeof(Character));
            return  deserializer.Deserialize(new StringReader(character.ToString())) as Character;

        }
    }
}
