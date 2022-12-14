using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia.Negocio.ViewModels
{
    public class Registro
    {
        public int? Identificador { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? Creacion { get; set; }
        public string? CodigoIdentificacion { get; set; }
        public byte? Edad { get; set; }
        public int? AfinidadMagia { get; set; }
        public string? Grimorio { get; set; }
        public int? Estatus { get; set; }
    }
}
