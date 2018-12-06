using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using HeroLabExportToPdf.Entities;

namespace HeroLabExportToPdf.Business
{
    public class ProfileLoader
    {

        private readonly string _xmlPath;
       
        public ProfileLoader(string xmlPath)
        {
            _xmlPath = xmlPath;
        }

        public void Import()
        {
            using (var reader = XmlReader.Create(_xmlPath))
            {
                var xmlCharacter = XDocument.Load(reader)
                    .Root?
                    .Element("public")?
                    .Element("character");
                Deserialize(xmlCharacter);

            }
        }


        private void Deserialize(XElement character)
        {
            var deserializer = new XmlSerializer(typeof(Character));
            var portfolio = deserializer.Deserialize(new StringReader(character.ToString())) as Character;
        }
    }
}
