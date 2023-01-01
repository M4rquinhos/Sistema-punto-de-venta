using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    public class NPersona
    {
        public static DataTable Listar()
        {
            DPersona datos = new DPersona();
            return datos.Listar();
        }

        public static DataTable ListarProveedores()
        {
            DPersona datos = new DPersona();
            return datos.ListarProveedores();
        }

        public static DataTable ListarClientes()
        {
            DPersona datos = new DPersona();
            return datos.ListarClientes();
        }

        public static DataTable Buscar(string valor)
        {
            DPersona datos = new DPersona();
            return datos.Buscar(valor);
        }

        public static DataTable BuscarProveedores(string valor)
        {
            DPersona datos = new DPersona();
            return datos.Buscar(valor);
        }

        public static DataTable BuscarClientes(string valor)
        {
            DPersona datos = new DPersona();
            return datos.Buscar(valor);
        }

        public static string Existe(string valor)
        {
            DPersona datos = new DPersona();
            return datos.Existe(valor);
        }

        public static string Insertar(string tipoPersona, string nombre, string tipoDocumento, string numDocumento, string direccion, string telefono, string email)
        {
            DPersona datos = new DPersona();
            string existe = datos.Existe(email);
            if (existe.Equals("1"))
            {
                return "El proveedor o cliente ya existe";
            }
            else
            {
                Persona Obj = new Persona();
                Obj.TipoPersona = tipoPersona;
                Obj.Nombre = nombre;
                Obj.TipoDocumento = tipoDocumento;
                Obj.NumeroDocumento = numDocumento;
                Obj.Direccion = direccion;
                Obj.Telefono = telefono;
                Obj.Email = email;
                return datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int idPersona, string tipoPersona, string nombreAnt, string nombre, string tipoDocumento, string numDocumento, string direccion, string telefono, string email)
        {
            DPersona datos = new DPersona();
            Persona Obj = new Persona();

            if (nombreAnt.Equals(nombre))
            {
                Obj.IdPersona = idPersona;
                Obj.TipoPersona = tipoPersona;
                Obj.Nombre = nombre;
                Obj.TipoDocumento = tipoDocumento;
                Obj.NumeroDocumento = numDocumento;
                Obj.Direccion = direccion;
                Obj.Telefono = telefono;
                Obj.Email = email;
                return datos.Actualizar(Obj);
            }
            else
            {
                string existe = datos.Existe(nombre);
                if (existe.Equals("1"))
                {
                    return "El proveedor o cliente ya existe";
                }
                else
                {
                    Obj.IdPersona = idPersona;
                    Obj.TipoPersona = tipoPersona;
                    Obj.Nombre = nombre;
                    Obj.TipoDocumento = tipoDocumento;
                    Obj.NumeroDocumento = numDocumento;
                    Obj.Direccion = direccion;
                    Obj.Telefono = telefono;
                    Obj.Email = email;
                    return datos.Actualizar(Obj);
                }
            }
        }

        public static string Eliminar(int id)
        {
            DPersona datos = new DPersona();
            return datos.Eliminar(id);
        }
    }
}
