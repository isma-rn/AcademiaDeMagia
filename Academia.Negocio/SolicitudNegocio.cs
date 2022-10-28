using Academia.MapeoDatos;
using Academia.MapeoDatos.Entidades;
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

        public async Task<List<Afinidad>> GetA()
        {
            return await _context.Afinidad.ToListAsync();
        }

    }
}