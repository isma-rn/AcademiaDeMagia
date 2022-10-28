using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia.MapeoDatos.Entidades
{
    public class Grimorio
    {
        [Key]
        public int GrimorioId { get; set; }
        [MaxLength(20)]
        public string Nombre { get; set; }
        public int NumeroHojas { get; set; }
    }
}
