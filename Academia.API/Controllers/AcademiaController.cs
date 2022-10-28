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

        [HttpPost]
        public async Task<IActionResult> EnviarSolicitud(Registro registro)
        {
            var validacion = _solicitudNegocio.ValidarNuevaSolicitud(registro);

            if (validacion.Success)
            {
                var result = await _solicitudNegocio.EnviarSolicitud(registro);

                if (result.Success)
                {
                    return Ok(new { Success = true});
                }

                return Ok(new { Success = false, Errors = result.Mensajes });
            }

            return Ok( new { Success = false, Errors = validacion.Mensajes});
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
