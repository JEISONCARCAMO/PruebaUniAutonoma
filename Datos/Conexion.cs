using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class conexion
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["CadenaSQL"].ToString();
    }
}
