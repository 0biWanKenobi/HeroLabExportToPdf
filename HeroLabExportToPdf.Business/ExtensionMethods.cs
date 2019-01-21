using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace HeroLabExportToPdf.Business
{
    public static class ExtensionMethods
    {
        public static string Hash256(this object o)
        {
            var serializer = new DataContractSerializer(o.GetType());
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, o);
                var sha256 = SHA256.Create();
                sha256.ComputeHash(memoryStream.ToArray());
                return Convert.ToBase64String(sha256.Hash);
            }
        }
    }
}
