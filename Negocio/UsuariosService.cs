using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Negocio
{
    public class UsuariosService
    {
        private UsuariosDatos datos = new UsuariosDatos();

        public List<UsuariosEntidad> Lista()
        {
            try
            {
                var r = datos.Lista();
                return r;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UsuariosEntidad Obtener(int id)
        {
            try
            {
                return datos.obtener(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Crear(UsuariosEntidad entidad)
        {
            try
            {
                var myhash = SHA256.Create();
                byte[] messageBytes = Encoding.UTF8.GetBytes(entidad.contrasena);
                byte[] hashValue = myhash.ComputeHash(messageBytes);
                var hashedpassword = Convert.ToBase64String(hashValue);


                if (entidad.nombres == "")
                    throw new ArgumentException("El nombre no puede ser vacio");

                if (entidad.apellidos == "")
                    throw new ArgumentException("El apellido no puede ser vacio");

                if (entidad.usuario == "")
                    throw new ArgumentException("El usuario no puede ser vacio");

                if (entidad.telefono == "")
                    throw new ArgumentException("Debe ingresar un numero valido");

                if (entidad.Identificacion.ToString().Length < 4)
                    throw new ArgumentException("La cedula es muy corta");

                if (entidad.Identificacion <= 0)
                    throw new ArgumentException("La cédula debe ser un número positivo.");

                if (entidad.contrasena.Length < 8)
                    throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

                if (!Regex.IsMatch(entidad.contrasena, @"[A-Z]"))
                    throw new ArgumentException("La contraseña debe contener al menos una letra mayúscula.");

                if (!Regex.IsMatch(entidad.contrasena, @"[a-z]"))
                    throw new ArgumentException("La contraseña debe contener al menos una letra minúscula.");

                if (!Regex.IsMatch(entidad.contrasena, @"\d"))
                    throw new ArgumentException("La contraseña debe contener al menos un número.");

                if (!Regex.IsMatch(entidad.contrasena, @"[\W_]"))
                    throw new ArgumentException("La contraseña debe contener al menos un carácter especial.");

                return datos.crear(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Editar(UsuariosEntidad entidad)
        {
            try
            {
                var encontrado = datos.obtener(entidad.Identificacion);

                if (encontrado.Identificacion == 0)
                    throw new OperationCanceledException("No existe el usuario");


                if (entidad.nombres == "")
                    throw new ArgumentException("El nombre no puede ser vacio");

                if (entidad.apellidos == "")
                    throw new ArgumentException("El apellido no puede ser vacio");

                if (entidad.usuario == "")
                    throw new ArgumentException("El usuario no puede ser vacio");

                if (entidad.telefono == "")
                    throw new ArgumentException("Debe ingresar un numero valido");

                if (entidad.Identificacion.ToString().Length < 4)
                    throw new ArgumentException("La cedula es muy corta");

                if (entidad.Identificacion <= 0)
                    throw new ArgumentException("La cédula debe ser un número positivo.");

                if (entidad.contrasena.Length < 8)
                    throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

                if (!Regex.IsMatch(entidad.contrasena, @"[A-Z]"))
                    throw new ArgumentException("La contraseña debe contener al menos una letra mayúscula.");

                if (!Regex.IsMatch(entidad.contrasena, @"[a-z]"))
                    throw new ArgumentException("La contraseña debe contener al menos una letra minúscula.");

                if (!Regex.IsMatch(entidad.contrasena, @"\d"))
                    throw new ArgumentException("La contraseña debe contener al menos un número.");

                if (!Regex.IsMatch(entidad.contrasena, @"[\W_]"))
                    throw new ArgumentException("La contraseña debe contener al menos un carácter especial.");

                return datos.editar(entidad);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Eliminar(int id)
        {
            try
            {
                var encontrado = datos.obtener(id);

                if (encontrado.Identificacion == 0)
                    throw new OperationCanceledException("No existe el usuario");
                return datos.eliminar(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool login (int id, string password)
        {
            var usuario = datos.obtener (id);
            var contrasena = usuario.contrasena;

            var myhash = SHA256.Create();
            byte[] messageBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashValue = myhash.ComputeHash(messageBytes);

            if (Convert.FromBase64String(usuario.contrasena).SequenceEqual(hashValue))
            {
                return true;
            }
            else
            {
                return false;
            }

                
        }



    }
}

