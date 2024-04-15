using System.Xml.Serialization;

namespace Dgii.NcfApp.Models
{
    [XmlRoot(ElementName = "GetPlacaResult", Namespace = "http://dgii.gov.do/")]
    public class GetPlacaResponse
    {
        public Placa Placa { get; set; }
        public OposicionModel[] Oposiciones { get; set; }
    }

    public class Placa
    {
        public string MARCA_VEHICULO { get; set; }
        public string MODELO_VEHICULO { get; set; }
        public string COLOR { get; set; }
        public string ANO_FABRICACION { get; set; }
        public string PLACA { get; set; }
        public string RNC_CEDULA_PROPIETARIO { get; set; }
    }

    public class OposicionModel
    {
        public int NUMERO_OPOSICION { get; set; }
        public string TIPO_OPOSICION { get; set; }
    }
}
