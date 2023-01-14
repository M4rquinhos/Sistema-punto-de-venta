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
    public class NArticulo
    {
        public static DataTable Listar()
        {
            DArticulo datos = new DArticulo();
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DArticulo datos = new DArticulo();
            return datos.Buscar(valor);
        }

        public static string Insertar(int idcategoria, string codigo, string nombre, decimal precioVenta, int stock, string descripcion, string imagen)
        {
            DArticulo datos = new DArticulo();
            string existe = datos.Existe(nombre);
            if (existe.Equals("1"))
            {
                return "El articulo ya existe";
            }
            else
            {
                Articulo Obj = new Articulo();
                Obj.IdCategoria = idcategoria;
                Obj.Codigo = codigo;
                Obj.Nombre = nombre;
                Obj.PrecioVenta = precioVenta;
                Obj.Stock = stock;
                Obj.Descripcion = descripcion;
                Obj.Imagen = imagen;
                return datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int idarticulo, int idcategoria, string codigo, string nombreAnt, string nombre, decimal precioVenta, int stock, string descripcion, string imagen)
        {
            DArticulo datos = new DArticulo();
            Articulo Obj = new Articulo();

            if (nombreAnt.Equals(nombre))
            {
                Obj.IdArticulo = idarticulo;
                Obj.IdCategoria = idcategoria;
                Obj.Codigo = codigo;
                Obj.Nombre = nombre;
                Obj.PrecioVenta = precioVenta;
                Obj.Stock = stock;
                Obj.Descripcion = descripcion;
                Obj.Imagen = imagen;
                return datos.Actualizar(Obj);
            }
            else
            {
                string existe = datos.Existe(nombre);
                if (existe.Equals("1"))
                {
                    return "El articulo ya existe";
                }
                else
                {
                    Obj.IdArticulo = idarticulo;
                    Obj.IdCategoria = idcategoria;
                    Obj.Codigo = codigo;
                    Obj.Nombre = nombre;
                    Obj.PrecioVenta = precioVenta;
                    Obj.Stock = stock;
                    Obj.Descripcion = descripcion;
                    Obj.Imagen = imagen;
                    return datos.Actualizar(Obj);
                }
            }


        }

        public static string Eliminar(int id)
        {
            DArticulo datos = new DArticulo();
            return datos.Eliminar(id);
        }

        public static string Activar(int id)
        {
            DArticulo datos = new DArticulo();
            return datos.Activar(id);
        }

        public static string Desactivar(int id)
        {
            DArticulo datos = new DArticulo();
            return datos.Desactivar(id);
        }

        public static DataTable BuscarCodigoBarras(string valor)
        {
            DArticulo datos = new DArticulo();
            return datos.BuscarCodigoBarras(valor);
        }
    }
}
