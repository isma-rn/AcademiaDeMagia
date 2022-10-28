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

        public async Task<Resultado> GuardarSolicitud(Registro registro)
        {
            var result = new Resultado();           

            if (registro.Identificador.HasValue && registro.Identificador > 0)
            {
                var consulta = _context.Solicitud.Where(w => w.SolicitudId == registro.Identificador)
                    .Include(i => i.Estudiante).FirstOrDefault();
                if (consulta != null)
                {
                    consulta.UltimaModificacion = DateTime.Now;
                    consulta.Estudiante.Nombre = registro.Nombre?.Trim() ?? "";
                    consulta.Estudiante.Apellido = registro.Apellido?.Trim() ?? "";
                    consulta.Estudiante.Edad = registro.Edad??0;
                    consulta.Estudiante.Identificacion = registro.CodigoIdentificacion?.Trim() ?? "";
                    consulta.Estudiante.AfinidadId = registro.AfinidadMagia??0;

                    _context.Entry(consulta).State = EntityState.Modified;
                }
                else
                {
                    result.Mensajes.Add("Error, No se encpontró solicitud");
                    return result;
                }                
            }
            else
            {
                var nuevaSolicitud = AcademiaMapper.SetSolicitud(registro);
                nuevaSolicitud.EstatusId = 1;
                nuevaSolicitud.Creacion = DateTime.Now;
                _context.Solicitud.Add(nuevaSolicitud);
            }

            try
            {
                await _context.SaveChangesAsync();

                if (registro.Identificador.HasValue && registro.Identificador > 0)
                {
                    result.Mensajes.Add("Actualizado correctamente");
                }
                else
                {
                    result.Mensajes.Add("Creado correctamente");
                }
            }
            catch (Exception error)
            {
                result.Mensajes.Add(error.Message);
                return result;
            }

            result.Success = true;            
            return result;
        }
        public async Task<Resultado> ActualizarEstatusSolicitud(ActualizaEstatus actualiza)
        {
            var result = new Resultado();
            var seAsignoGrimorio = false;

            var consultaSolicitud = _context.Solicitud.Where(w => w.SolicitudId == actualiza.Identificador)
                .Include(i => i.Estudiante).FirstOrDefault();
            if (consultaSolicitud != null)
            {
                var consultaEstatus = _context.Estatus.Where(w => w.EstatusId == actualiza.NuevoValor)
                    .FirstOrDefault();
                if (consultaEstatus != null)
                {
                    consultaSolicitud.EstatusId = consultaEstatus.EstatusId;
                    consultaSolicitud.UltimaModificacion = DateTime.Now;
                    if(consultaSolicitud.EstatusId == 2)
                    {
                        var nuevoGrimorio = AsignarGrimorioAleatorio();
                        if (nuevoGrimorio == -1)
                        {
                            result.Mensajes.Add("Error, No se puede aprobar solicitud porque no existe grimorio alguno para asignarlo al estudiante");
                            return result;
                        }
                        consultaSolicitud.Estudiante.GrimorioId = nuevoGrimorio;
                        seAsignoGrimorio = true;
                    }
                    _context.Entry(consultaSolicitud).State = EntityState.Modified;
                }
                else
                {
                    result.Mensajes.Add("Error, No se reconoce estatus nuevo");
                    return result;
                }
            }
            else
            {
                result.Mensajes.Add("Error, No se encpontró solicitud");
                return result;
            }
            
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
            if (seAsignoGrimorio)
            {
                result.Mensajes.Add("Estatus actualizado correctamente, Se asignó Grimorio Correctamente");
            }
            else
            {
                result.Mensajes.Add("Estatus actualizado correctamente");
            }
            return result;
        }
        private int AsignarGrimorioAleatorio()
        {
            var consulta = _context.Grimonio.Select(s => s.GrimorioId).ToList();
            if(consulta.Count > 0)
            {
                if(consulta.Count == 1)
                {
                    return 0;
                }

                var rand = new Random();
                var aleatorio = rand.Next(0, consulta.Count - 1 );
                return aleatorio;
            }

            return -1;
        }
        public Resultado ValidarNuevaSolicitud(Registro registro)
        {
            var result = new Resultado();

            if(registro != null)
            {
                if (registro.Identificador.HasValue && registro.Identificador>0)
                {
                    var existe = _context.Solicitud.Where(w => w.SolicitudId == registro.Identificador).Any();
                    if (!existe)
                    {
                        result.Mensajes.Add("Error, No se encpontró solicitud");
                    }
                }
                //
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
        public Resultado ValidarNuevoEstatus(ActualizaEstatus actualiza)
        {
            var result = new Resultado();

            if (!actualiza.Identificador.HasValue || (actualiza.Identificador.HasValue && actualiza.Identificador == 0 ))
            {
                result.Mensajes.Add("Ingresa identificador solicitud");
            }
            else
            {
                var existe = _context.Solicitud.Where(w => w.SolicitudId == actualiza.Identificador).Any();
                if (!existe)
                {
                    result.Mensajes.Add("Error, No se encpontró solicitud");
                }
            }
            //
            if (!actualiza.NuevoValor.HasValue || (actualiza.NuevoValor.HasValue && actualiza.NuevoValor == 0))
            {
                result.Mensajes.Add("Ingresa nuevo valor de estatus");
            }
            else
            {
                var existe = _context.Estatus.Where(w => w.EstatusId == actualiza.NuevoValor).Any();
                if (!existe)
                {
                    result.Mensajes.Add("Error, estatus nuevo no válido");
                }
            }
            
            if (result.Mensajes.Count == 0)
            {
                result.Success = true;
            }           

            return result;
        }

        public async Task<List<Afinidad>> GetA()
        {
            return await _context.Afinidad.ToListAsync();
        }

    }
}