using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class EstadosDatos
    {
        public List<EstadoEntidades> Lista()
        {
            List<EstadoEntidades> lista = new List<EstadoEntidades>();

            using (SqlConnection oConexion = new SqlConnection(conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("select * from fn_estados()", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Entidades.EstadoEntidades
                            {
                                idestados = Convert.ToInt32(reader["estados"].ToString()),
                                nombre = reader["nombre"].ToString()
                            });

                        }
                    }
                    return lista;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
