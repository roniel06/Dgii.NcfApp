using System;
using System.ComponentModel.DataAnnotations;

namespace Dgii.NcfApp.Models
{
    public class PlacasResultHistory 
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Placa { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public string Color { get; set; }
        public string AnioFabricacion { get; set; }
        public string RncCedulaPropietario { get; set; }
    }
}

