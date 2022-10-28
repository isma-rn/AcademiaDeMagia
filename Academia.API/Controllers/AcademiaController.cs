using Academia.MapeoDatos;
using Academia.MapeoDatos.Entidades;
using Academia.Negocio;
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

        [HttpGet]
        public async Task<ActionResult<List<Afinidad>>> ObtenerSolicitudes()
        {
            var solicitud = new Solicitud();

            var result = await _solicitudNegocio.GetA();
            
            return result;
        }
    }
}
