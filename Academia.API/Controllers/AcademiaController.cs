using Academia.MapeoDatos;
using Academia.MapeoDatos.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Academia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademiaController : ControllerBase
    {
        private BaseDatosContext _context;
        public AcademiaController(BaseDatosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Afinidad> Get() => _context.Afinidad.ToList();
    }
}
