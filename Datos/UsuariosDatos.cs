using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Datos
{
    public class UsuariosDatos
    {
        public List<UsuariosEntidad> Lista()
        {

            List<UsuariosEntidad> lista = new List<UsuariosEntidad>();

            using (SqlConnection oConexion = new SqlConnection(conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("select * from fn_usuariosActivos()", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Entidades.UsuariosEntidad
                            {
                                Identificacion = Convert.ToInt32(reader["identificacion"].ToString()),
                                nombres = reader["nombres"].ToString(),
                                apellidos = reader["apellidos"].ToString(),
                                //fecha_nacimiento = (Convert.ToDateTime(reader["fecha_nacimiento"].ToString()),
                                telefono = reader["telefono"].ToString(),
                                usuario = reader["usuario"].ToString(),
                                contrasena = reader["contrasena"].ToString(),
                                //idestados =Convert.ToInt32(reader["estados"].ToString())
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
        public UsuariosEntidad obtener(int identificacion)
        {

            UsuariosEntidad entidad = new UsuariosEntidad();

            using (SqlConnection oConexion = new SqlConnection(conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("select * from  fn_usuario(@identificacion)", oConexion);
                cmd.Parameters.AddWithValue("@identificacion", identificacion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entidad.Identificacion = Convert.ToInt32(reader["identificacion"].ToString());
                            entidad.nombres = reader["nombres"].ToString();
                            entidad.apellidos = reader["apellidos"].ToString();
                            //entidad.fecha_nacimiento = reader["fechanacimiento"].ToString();
                            entidad.telefono = reader["telefono"].ToString();
                            entidad.contrasena = reader["contrasena"].ToString();
                            entidad.usuario=reader["usuario"].ToString();
                        }
                    }
                    return entidad;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool crear(UsuariosEntidad entidad)
        {
            var myhash = SHA256.Create();
            byte[] messageBytes = Encoding.UTF8.GetBytes(entidad.contrasena);
            byte[] hashValue = myhash.ComputeHash(messageBytes);
            var hashedpassword =  Convert.ToBase64String(hashValue);

            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_CrearUsuario", oConexion);
                cmd.Parameters.AddWithValue("@Identificacion", entidad.Identificacion);
                cmd.Parameters.AddWithValue("@Nombre", entidad.nombres);
                cmd.Parameters.AddWithValue("@Apellidos", entidad.apellidos);
                cmd.Parameters.AddWithValue("@Fecha_nacimiento", entidad.fecha_nacimiento);
                cmd.Parameters.AddWithValue("@telefono", entidad.telefono);
                cmd.Parameters.AddWithValue("@NUsuario", entidad.usuario);
                cmd.Parameters.AddWithValue("@Contrasena", hashedpassword);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasafectadas = cmd.ExecuteNonQuery();
                    if (filasafectadas > 0) respuesta = true;
                    return respuesta;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool editar(UsuariosEntidad entidad)
        {

            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oConexion);
                cmd.Parameters.AddWithValue("@identificacion", entidad.Identificacion);
                cmd.Parameters.AddWithValue("@nombre", entidad.nombres);
                cmd.Parameters.AddWithValue("@apellidos", entidad.apellidos);
                cmd.Parameters.AddWithValue("@Fecha_nacimiento", entidad.fecha_nacimiento);
                cmd.Parameters.AddWithValue("@telefono", entidad.telefono);
                cmd.Parameters.AddWithValue("@Usuario", entidad.usuario);
                cmd.Parameters.AddWithValue("@contrasena", entidad.contrasena);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasafectadas = cmd.ExecuteNonQuery();
                    if (filasafectadas > 0) respuesta = true;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool eliminar(int identificacion)
        {

            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteUsuario", oConexion);
                cmd.Parameters.AddWithValue("@Identificacion", identificacion);

                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasafectadas = cmd.ExecuteNonQuery();
                    if (filasafectadas > 0) respuesta = true;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
