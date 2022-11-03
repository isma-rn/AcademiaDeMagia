using System;

namespace Academia.ClienteWeb.Models
{
    public class Registro
    {
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Creacion { get; set; }
        public string CodigoIdentificacion { get; set; } = string.Empty;
        public byte Edad { get; set; }
        public int AfinidadMagia { get; set; }
        public string Grimorio { get; set; } = string.Empty;
        public int Estatus { get; set; }
    }
}
