using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
    public class NUsuario
    {
        public static DataTable Listar()
        {
            DUsuario datos = new DUsuario();
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DUsuario datos = new DUsuario();
            return datos.Buscar(valor);
        }

        public static DataTable Login(string email, string clave)
        {
            DUsuario datos = new DUsuario();
            return datos.Login(email, clave);
        }

        public static string Insertar(int idrol, string nombre, string tipoDocumento, string numDocumento, string direccion, string telefono, string email, string clave)
        {
            DUsuario datos = new DUsuario();
            string existe = datos.Existe(email);
            if (existe.Equals("1"))
            {
                return "El usuario ya existe";
            }
            else
            {
                Usuario Obj = new Usuario();
                Obj.IdRol = idrol;
                Obj.Nombre = nombre;
                Obj.TipoDocumento = tipoDocumento;
                Obj.NumDocumento = numDocumento;
                Obj.Direccion = direccion;
                Obj.Telefono = telefono;
                Obj.Email = email;
                Obj.Clave = clave;
                return datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int idusuario, int idrol, string nombre, string tipoDocumento, string numDocumento, string direccion, string telefono, string emailAnt, string email, string clave)
        {
            DUsuario datos = new DUsuario();
            Usuario Obj = new Usuario();

            if (emailAnt.Equals(email))
            {
                Obj.IdUsuario = idusuario;
                Obj.IdRol = idrol;
                Obj.Nombre = nombre;
                Obj.TipoDocumento = tipoDocumento;
                Obj.NumDocumento = numDocumento;
                Obj.Direccion = direccion;
                Obj.Telefono = telefono;
                Obj.Email = email;
                Obj.Clave = clave;
                return datos.Actualizar(Obj);
            }
            else
            {
                string existe = datos.Existe(email);
                if (existe.Equals("1"))
                {
                    return "El usuario ya existe";
                }
                else
                {
                    Obj.IdUsuario = idusuario;
                    Obj.IdRol = idrol;
                    Obj.Nombre = nombre;
                    Obj.TipoDocumento = tipoDocumento;
                    Obj.NumDocumento = numDocumento;
                    Obj.Direccion = direccion;
                    Obj.Telefono = telefono;
                    Obj.Email = email;
                    Obj.Clave = clave;
                    return datos.Actualizar(Obj);
                }
            }


        }

        public static string Eliminar(int id)
        {
            DUsuario datos = new DUsuario();
            return datos.Eliminar(id);
        }

        public static string Activar(int id)
        {
            DUsuario datos = new DUsuario();
            return datos.Activar(id);
        }

        public static string Desactivar(int id)
        {
            DUsuario datos = new DUsuario();
            return datos.Desactivar(id);
        }
    }
}
