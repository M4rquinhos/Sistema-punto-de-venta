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
    public class NVenta
    {
        public static DataTable Listar()
        {
            DVenta datos = new DVenta();
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DVenta datos = new DVenta();
            return datos.Buscar(valor);
        }

        public static DataTable ListarDetalle(int idventa)
        {
            DVenta datos = new DVenta();
            return datos.ListarDetalle(idventa);
        }

        public static string Insertar(int idCliente, int idUsuario, string tipoComprobante, string serieComprobante, string numComprobante, decimal impuesto, decimal total, DataTable detalle)
        {
            DVenta datos = new DVenta();
            Venta obj = new Venta();
            //se puede simplificar 
            obj.IdCliente = idCliente;
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
            DVenta datos = new DVenta();
            return datos.Anular(id);
        }
    }
}
