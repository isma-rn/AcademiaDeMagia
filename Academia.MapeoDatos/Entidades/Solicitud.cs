using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia.MapeoDatos.Entidades
{
    public class Solicitud
    {
        [Key]
        public int SolicitudId { get; set; }        
        public DataType Creacion { get; set; }
        public DataType? UltimaModificacion { get; set; }
        
        public int EstudianteId { get; set; }
        [ForeignKey("EstudianteId")]
        public virtual Estudiante Estudiante { get; set; }

        public int EstatusId { get; set; }
        [ForeignKey("EstatusId")]        
        public virtual Estatus Estatus { get; set; }
    }
}
