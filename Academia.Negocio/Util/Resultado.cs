using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia.Negocio.Util
{
    public class Resultado<T>
    {
        public T Value { get; set; }
        public bool Success { get; set; }
        public string Mensaje { get; set; }
    }
}
