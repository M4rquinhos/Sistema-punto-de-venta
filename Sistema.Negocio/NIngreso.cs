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
    public class NIngreso
    {
        public static DataTable Listar()
        {
            DIngreso datos = new DIngreso();
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DIngreso datos = new DIngreso();
            return datos.Buscar(valor);
        }

        public static string Insertar(int idProveedor, int idUsuario, string tipoComprobante, string serieComprobante, string numComprobante, decimal impuesto, decimal total, DataTable detalle)
        {
            DIngreso datos = new DIngreso();
            Ingreso obj = new Ingreso();
            //se puede simplificar 
            obj.IdProveedor = idProveedor;
            obj.IdUsuario = idUsuario;
            obj.TipoComprobante = tipoComprobante;
            obj.SerieComprobante = serieComprobante;
            obj.NumComprobante = numComprobante;
            obj.Impuesto = impuesto;
            obj.Total = total;
            obj.Detalles = detalle;
            return datos.Insertar(obj);
        }

        public static string Anular(int id)
        {
            DIngreso datos = new DIngreso();
            return datos.Anular(id);
        }
    }
}
