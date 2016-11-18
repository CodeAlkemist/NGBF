using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace NGBF
{
    public class XMLHandler
    {
        public const string Schema = "http://www.w3.org/2001/XMLSchema";
        public static string dataPath = Application.dataPath + "/Core/Data";
        public static void Deserialize(string fileName, ref object output)
        {
            XmlSerializer ser = new XmlSerializer(output.GetType(), Schema);
            FileStream fs = new FileStream(dataPath + fileName, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            output = ser.Deserialize(reader);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        public static void Serialize<T>(T data, string fileName)
        {
            XmlDocument doc = SerializeToXmlDocument(data);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using(XmlWriter writer = XmlWriter.Create(dataPath + fileName, settings))
            {
                doc.Save(writer);
            }
        }
        /// <summary>
        /// Created by marc_s. (for credit) will add summary later.
        /// </summary>
        /// <param name="input">Data to serialize.</param>
        /// <returns>Returns the data as an XML Document</returns>
        public static XmlDocument SerializeToXmlDocument(object input)
        {
            XmlSerializer ser = new XmlSerializer(input.GetType(), Schema);

            XmlDocument xd = null;

            using (MemoryStream memStm = new MemoryStream())
            {
                ser.Serialize(memStm, input);

                memStm.Position = 0;

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;

                using (var xtr = XmlReader.Create(memStm, settings))
                {
                    xd = new XmlDocument();
                    xd.Load(xtr);
                }
            }

            return xd;
        }
    }
}
