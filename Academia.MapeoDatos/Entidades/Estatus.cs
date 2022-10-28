using System.ComponentModel.DataAnnotations;

namespace Academia.MapeoDatos.Entidades
{
    public class Estatus
    {
        [Key]
        public int EstatusId { get; set; }
        public string Nombre { get; set; }
    }
}
