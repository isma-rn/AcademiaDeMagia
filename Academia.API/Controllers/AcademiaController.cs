using Academia.MapeoDatos;
using Academia.MapeoDatos.Entidades;
using Academia.Negocio;
using Academia.Negocio.Util;
using Academia.Negocio.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Academia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademiaController : ControllerBase
    {
        private readonly SolicitudNegocio _solicitudNegocio;
        public AcademiaController(SolicitudNegocio solicitudNegocio)
        {
            _solicitudNegocio = solicitudNegocio;
        }

        /// <summary>
        /// Método para envío o actualización de solicitudes para la academia de magia
        /// </summary>
        /// <param name="registro">objeto solicitud</param>
        /// <returns>json</returns>
        [HttpPost]
        [Route("GuardarSolicitud")]
        public async Task<IActionResult> GuardarSolicitud(Registro registro)
        {
            var validacion = _solicitudNegocio.ValidarNuevaSolicitud(registro);

            if (validacion.Success)
            {
                var result = await _solicitudNegocio.GuardarSolicitud(registro);

                if (result.Success)
                {
                    var primerMensaje = result.Mensajes.FirstOrDefault();
                    return Ok(new { Success = true, Mensaje = primerMensaje??""});
                }

                return Ok(new { Success = false, Errors = result.Mensajes });
            }

            return Ok( new { Success = false, Errors = validacion.Mensajes});
        }

        /// <summary>
        /// Método para actualizar estatus de solicitudes para la academia de magia
        /// </summary>
        /// <param name="actualiza">objeto para actualizar estatus</param>
        /// <returns>json</returns>
        [HttpPost]
        [Route("ActulizarEstatusSolicitud")]
        public async Task<IActionResult> ActulizarEstatusSolicitud(ActualizaEstatus actualiza)
        {
            var validacion = _solicitudNegocio.ValidarNuevoEstatus(actualiza);

            if (validacion.Success)
            {
                var result = await _solicitudNegocio.ActualizarEstatusSolicitud(actualiza);

                if (result.Success)
                {
                    var primerMensaje = result.Mensajes.FirstOrDefault();
                    return Ok(new { Success = true, Mensaje = primerMensaje ?? "" });
                }

                return Ok(new { Success = false, Errors = result.Mensajes });
            }

            return Ok(new { Success = false, Errors = validacion.Mensajes });
        }

        /// <summary>
        /// Obtiene una lista de todas las solicitudes disponibles
        /// </summary>
        /// <returns>json</returns>
        [HttpGet]
        [Route("ConsultarSolicitudes")]
        public async Task<ActionResult> ConsultarSolicitudes()
        {
            var result = await _solicitudNegocio.GetAll();

            return Ok(new { Lista = result });
        }

        /// <summary>
        /// Obtiene un listado de estudiantes, de acuerdo a un grimorio
        /// </summary>
        /// <param name="identificador">identificador grimorio</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ConsultarAsignaciones")]
        public async Task<ActionResult> ConsultarAsignaciones(int identificador)
        {
            var result = await _solicitudNegocio.GetAsignaciones(identificador);

            return Ok(new { Lista = result });
        }

        /// <summary>
        /// Elimina de forma física una solicitud
        /// </summary>
        /// <param name="identificador">identificador solicitud</param>
        /// <returns></returns>
        [HttpPost]
        [Route("EliminarSolicitud")]
        public async Task<ActionResult> EliminarSolicitud(int identificador)
        {
            var result = await _solicitudNegocio.EliminarSolicitud(identificador);

            return Ok(new { success = result.Success, Mensajes =  result.Mensajes});
        }
    }
}
