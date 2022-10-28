using Academia.MapeoDatos.Entidades;
using Academia.Negocio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia.Negocio.Mapper
{
    public class AcademiaMapper
    {
        public static Solicitud setSolicitud(Registro registro, bool esCreacion = false)
        {
            var result = new Solicitud();
            if (esCreacion)
            {
                result.Creacion = DateTime.Now;
                result.EstatusId = 1;
            }
            else
            {
                result.UltimaModificacion = DateTime.Now;
            }
            var nuevoEstudiante = new Estudiante()
            {
                Nombre = registro.Nombre ?? "",
                Apellido = registro.Apellido ?? "",
                Edad = registro.Edad ?? 0,
                Identificacion = registro.CodigoIdentificacion??"",
                AfinidadId = registro.AfinidadMagia ?? 0
            };
            result.Estudiante = nuevoEstudiante;

            return result;
        }
    }
}
