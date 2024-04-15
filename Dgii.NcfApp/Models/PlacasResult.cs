using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Dgii.NcfApp.Models
{
    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Envelope<T>
    {
        [XmlElement(ElementName = "Body")]
        public T Body { get; set; }
    }
    [XmlRoot(ElementName = "placa")]
    public class Placa
    {
        public string MARCA_VEHICULO { get; set; }
        public string MODELO_VEHICULO { get; set; }
        public string COLOR { get; set; }
        public int ANO_FABRICACION { get; set; }
        public string PLACA { get; set; }
        public long RNC_CEDULA_PROPIETARIO { get; set; }
    }

    [XmlRoot(ElementName = "OposicionModel")]
    public class OposicionModel
    {
        public int NUMERO_OPOSICION { get; set; }
        public string TIPO_OPOSICION { get; set; }
    }

    [XmlRoot(ElementName = "GetPlacaResult")]
    public class GetPlacaResult
    {
        public Placa placa { get; set; }
        [XmlArray("oposiciones")]
        [XmlArrayItem("OposicionModel", typeof(OposicionModel))]
        public OposicionModel[] oposiciones { get; set; }
    }

    [XmlRoot(ElementName = "GetPlacaResponse", Namespace = "http://dgii.gov.do/")]
    public class GetPlacaResponse
    {
        public GetPlacaResult GetPlacaResult { get; set; }
    }

}

