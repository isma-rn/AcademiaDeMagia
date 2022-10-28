using Academia.MapeoDatos;
using Academia.MapeoDatos.Entidades;
using Academia.Negocio.Mapper;
using Academia.Negocio.Util;
using Academia.Negocio.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Academia.Negocio
{    
    public class SolicitudNegocio
    {
        private BaseDatosContext _context;
        public SolicitudNegocio(BaseDatosContext context)
        {
            _context = context;
        }

        public async Task<Resultado> EnviarSolicitud(Registro registro)
        {
            var result = new Resultado();

            var nuevaSolicitud = AcademiaMapper.setSolicitud(registro);
            nuevaSolicitud.EstatusId = 1;
            _context.Solicitud.Add(nuevaSolicitud);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                result.Mensajes.Add(error.Message);
                return result;
            }

            result.Success = true;
            return result;
        }
        public Resultado ValidarNuevaSolicitud(Registro registro)
        {
            var result = new Resultado();

            if(registro != null)
            {
                if (string.IsNullOrEmpty(registro.Nombre))
                {
                    result.Mensajes.Add("Capture Nombre");
                }
                else if (registro.Nombre.Length>20)
                {
                    result.Mensajes.Add("Sólo se permiten máximo 20 caraceres para el Nombre");
                }
                //
                if (string.IsNullOrEmpty(registro.Apellido))
                {
                    result.Mensajes.Add("Capture Apellidos");
                }
                else if (registro.Apellido.Length > 20)
                {
                    result.Mensajes.Add("Sólo se permiten máximo 20 caraceres para el Apellido");
                }

                if (!registro.Edad.HasValue)
                {
                    result.Mensajes.Add("Capture Edad");
                }
                else if (!(registro.Edad >= 1 && registro.Edad <= 99))
                {
                    result.Mensajes.Add("Sólo se permiten edades de máximo 2 digítos");
                }
                //
                if (string.IsNullOrEmpty(registro.CodigoIdentificacion))
                {
                    result.Mensajes.Add("Capture Apellidos");
                }
                else if (registro.CodigoIdentificacion.Length > 10)
                {
                    result.Mensajes.Add("Sólo se permiten máximo 10 caraceres para el Identificación");
                }
                //
                if (!registro.AfinidadMagia.HasValue || registro.AfinidadMagia == 0)
                {
                    result.Mensajes.Add("Capture Afinidad");
                }
                else
                {
                    var existe = _context.Afinidad.Where(w => w.AfinidadId == registro.AfinidadMagia).Any();
                    if (!existe)
                    {
                        result.Mensajes.Add("No se encpontró afinidad seleccionada");
                    }
                }

                if (result.Mensajes.Count == 0)
                {
                    result.Success = true;
                }

                return result;
            }

            result.Mensajes.Add("No se encontraron datos");
            return result;
        }

        public async Task<List<Afinidad>> GetA()
        {
            return await _context.Afinidad.ToListAsync();
        }

    }
}