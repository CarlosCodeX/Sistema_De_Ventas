using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    class ConexionBD
    {

        public static String cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();

    }
}
