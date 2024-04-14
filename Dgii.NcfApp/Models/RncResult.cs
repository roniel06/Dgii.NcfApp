using System;
using System.Text.Json.Serialization;

namespace Dgii.NcfApp.Models
{
    public class RncResult
    {
        [JsonPropertyName("RGE_RUC")]
        public string Rnc { get; set; }
        [JsonPropertyName("RGE_NOMBRE")]
        public string RazonSocial { get; set; }
        [JsonPropertyName("NOMBRE_COMERCIAL")]
        public string NombreComercial { get; set; }
    }

}

