using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia.Negocio.Util
{
    public class Resultado
    {
        public bool Success { get; set; }
        public List<string> Mensajes = new List<string>();
    }
}
