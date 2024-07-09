using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class EstadosNegocio
    {
        EstadosNegocio estados = new EstadosNegocio();

        public List <EstadosNegocio> lista()
        {
            try
            {
                return estados.lista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
