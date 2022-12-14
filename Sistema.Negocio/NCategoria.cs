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
    public class NCategoria
    {

        //se retorna lo que devuelven los metodos de la capa Datos 
        public static DataTable Listar()
        {
            DCategoria datos = new DCategoria();
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DCategoria datos = new DCategoria();
            return datos.Buscar(valor);
        }

        public static string Insertar(string nombre, string descripcion)
        {
            DCategoria datos = new DCategoria();
            string existe = datos.Existe(nombre);
            if (existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else
            {
                Categoria Obj = new Categoria();
                Obj.Nombre = nombre;
                Obj.Descripcion = descripcion;
                return datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int id, string nombreAnt, string nombre, string descripcion)
        {
            DCategoria datos = new DCategoria();
            Categoria Obj = new Categoria();

            if (nombreAnt.Equals(nombre))
            {
                Obj.IdCategoria = id;
                Obj.Nombre = nombre;
                Obj.Descripcion = descripcion;
                return datos.Actualizar(Obj);
            }
            else
            {
                string existe = datos.Existe(nombre);
                if (existe.Equals("1"))
                {
                    return "La categoria ya existe";
                }
                else
                {
                    Obj.IdCategoria = id;
                    Obj.Nombre = nombre;
                    Obj.Descripcion = descripcion;
                    return datos.Actualizar(Obj);
                }
            }

           
        } 

        public static string Eliminar(int id)
        {
            DCategoria datos = new DCategoria();
            return datos.Eliminar(id);
        }

        public static string Activar(int id)
        {
            DCategoria datos = new DCategoria();
            return datos.Activar(id);
        }

        public static string Desactivar(int id)
        {
            DCategoria datos = new DCategoria();
            return datos.Desactivar(id);
        }
    }
}
