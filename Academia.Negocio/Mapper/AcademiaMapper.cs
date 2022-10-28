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
        /// <summary>
        /// Combierte el objeto view model de solicitud a entidad Solicitud
        /// </summary>
        /// <param name="registro">objeto solicitud</param>
        /// <returns>Solicitud</returns>
        public static Solicitud SetSolicitud(Registro registro)
        {
            var result = new Solicitud();

            result.SolicitudId = registro.Identificador ?? 0;
            var nuevoEstudiante = new Estudiante()
            {
                Nombre = registro.Nombre?.Trim() ?? "",
                Apellido = registro.Apellido?.Trim() ?? "",
                Edad = registro.Edad ?? 0,
                Identificacion = registro.CodigoIdentificacion?.Trim() ?? "",
                AfinidadId = registro.AfinidadMagia ?? 0
            };
            result.Estudiante = nuevoEstudiante;                       

            return result;
        }
    }
}
