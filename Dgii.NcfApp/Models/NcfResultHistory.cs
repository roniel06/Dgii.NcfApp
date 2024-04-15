using System;
using System.ComponentModel.DataAnnotations;

namespace Dgii.NcfApp.Models
{
    public class NcfResultHistory : NcfResult
    {
        [Key]
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}

