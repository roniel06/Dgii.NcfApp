using System;
using System.Text.Json;
using System.Xml;

namespace Dgii.NcfApp.Helpers
{
    public static class XmlClassHelper
    {


        public static T DeserializeXml<T>(string xml, string elementName)
        {
            // Crea un documento XML y carga el contenido
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            // Busca el contenido dentro del elemento especificado
            XmlNodeList nodeList = doc.GetElementsByTagName(elementName);
            string content = nodeList[0].InnerText;

            // Deserializa el contenido a la clase especificada
            T result = JsonSerializer.Deserialize<T>(content);
            return result;
        }
    }
}
