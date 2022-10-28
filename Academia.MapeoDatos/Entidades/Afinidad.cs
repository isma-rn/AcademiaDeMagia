using System.ComponentModel.DataAnnotations;

namespace Academia.MapeoDatos.Entidades
{
    public class Afinidad
    {
        [Key]
        public int AfinidadId { get; set; }
        [MaxLength(20)]
        public string Nombre { get; set; }
    }
}
