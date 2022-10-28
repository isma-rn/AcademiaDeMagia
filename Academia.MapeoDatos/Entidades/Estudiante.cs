using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academia.MapeoDatos.Entidades
{
    public class Estudiante
    {
        [Key]
        public int EstudianteId { get; set; }
        [MaxLength(20)]
        public string Nombre { get; set; }
        [MaxLength(20)]
        public string Apellido { get; set; }
        [MaxLength(10)]
        public string Identificacion { get; set; }
        public byte Edad { get; set; }
        
        public int AfinidadId { get; set; }
        [ForeignKey("AfinidadId")]
        public virtual Afinidad Afinidad { get; set; }

        public int? GrimorioId { get; set; }
        public virtual Grimorio Grimorio { get; set; }
    }
}
