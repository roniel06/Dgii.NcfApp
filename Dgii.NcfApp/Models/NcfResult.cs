using System;
using System.Text.Json.Serialization;

namespace Dgii.NcfApp.Models
{
    public class NcfResult
    {

        [JsonPropertyName("NOMBRE")]
        public string Nombre { get; set; }

        [JsonPropertyName("COMPROBANTE")]
        public string Comprobante { get; set; }

        [JsonPropertyName("ES_VALIDO")]
        public string EsValido { get; set; }

        [JsonPropertyName("MENSAJE_VALIDACION")]
        public string MensajeValidacion { get; set; }

        [JsonPropertyName("RNC")]
        public string Rnc { get; set; }

        [JsonPropertyName("NCF")]
        public string Ncf { get; set; }

        [JsonPropertyName("ESTADO")]
        public string Estado { get; set; }

        [JsonPropertyName("FECHA_VIGENCIA")]
        public string FechaVigencia { get; set; }

    }
}

