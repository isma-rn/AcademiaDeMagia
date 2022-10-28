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

        [HttpGet]
        public async Task<ActionResult<List<Afinidad>>> ObtenerSolicitudes()
        {
            var solicitud = new Solicitud();

            var result = await _solicitudNegocio.GetA();
            
            return result;
        }
    }
}
