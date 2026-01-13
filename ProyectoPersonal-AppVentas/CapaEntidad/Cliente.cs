using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {

        public int IdCliente { get; set; }
        public String Nombre { get; set; }
        public String Documento { get; set; }
        public String Telefono { get; set; }
        public String Email { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }

    }
}
