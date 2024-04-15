using Dgii.NcfApp.Models;
using Newtonsoft.Json;
using System;
using System.Numerics;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

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
            T result = System.Text.Json.JsonSerializer.Deserialize<T>(content);
            return result;
        }

        public static GetPlacaResponse MapXmlToGetPlacaResult(string xml)
        {
            XNamespace soap = "http://www.w3.org/2003/05/soap-envelope";
            XNamespace dgii = "http://dgii.gov.do/";

            // Parse the XML string
            var doc = XDocument.Parse(xml);

            // Extract the relevant elements using LINQ to XML
            var placaElement = doc.Descendants(dgii + "placa").FirstOrDefault();
            var oposicionesElements = doc.Descendants(dgii + "OposicionModel");

            // Map placa element to Placa object
            var placa = new Placa
            {
                MARCA_VEHICULO = placaElement.Element(dgii + "MARCA_VEHICULO")?.Value,
                MODELO_VEHICULO = placaElement.Element(dgii + "MODELO_VEHICULO")?.Value,
                COLOR = placaElement.Element(dgii + "COLOR")?.Value,
                ANO_FABRICACION = placaElement.Element(dgii + "ANO_FABRICACION")?.Value,
                PLACA = placaElement.Element(dgii + "PLACA")?.Value,
                RNC_CEDULA_PROPIETARIO = placaElement.Element(dgii + "RNC_CEDULA_PROPIETARIO")?.Value
            };

            // Map oposiciones elements to OposicionModel array
            var oposiciones = oposicionesElements.Select(oposicionElement => new OposicionModel
            {
                NUMERO_OPOSICION = int.Parse(oposicionElement.Element(dgii + "NUMERO_OPOSICION")?.Value),
                TIPO_OPOSICION = oposicionElement.Element(dgii + "TIPO_OPOSICION")?.Value
            }).ToArray();

            // Create GetPlacaResult object
            var result = new GetPlacaResponse
            {
                Placa = placa,
                Oposiciones = oposiciones
            };

            return result;
        }
    }
}
