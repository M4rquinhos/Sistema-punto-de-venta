﻿using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class DVenta
    {
        public DataTable Listar()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion(); //Creando la conexion con la BD
                SqlCommand comando = new SqlCommand("venta_listar", sqlCon); //recibe que es lo que se quiere hacer (SP O QUERY)
                comando.CommandType = CommandType.StoredProcedure; //Se indica que es un SP
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }

        public DataTable Buscar(string valor)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("venta_buscar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }

        public DataTable ConsultaFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("venta_consulta_fechas", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@fecha_inicio", SqlDbType.Date).Value = fechaInicio;
                comando.Parameters.Add("@fecha_fin", SqlDbType.Date).Value = fechaFin;
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }

        public DataTable ListarDetalle(int idventa)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("venta_listar_detalle", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idventa", SqlDbType.Int).Value = idventa;
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }

        public string Insertar(Venta Obj)
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("venta_insertar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcliente", SqlDbType.Int).Value = Obj.IdCliente;
                comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Obj.IdUsuario;
                comando.Parameters.Add("@tipo_comprobante", SqlDbType.VarChar).Value = Obj.TipoComprobante;
                comando.Parameters.Add("@serie_comprobante", SqlDbType.VarChar).Value = Obj.SerieComprobante;
                comando.Parameters.Add("@num_comprobante", SqlDbType.VarChar).Value = Obj.NumComprobante;
                comando.Parameters.Add("@impuesto", SqlDbType.VarChar).Value = Obj.Impuesto;
                comando.Parameters.Add("@total", SqlDbType.VarChar).Value = Obj.Total;
                comando.Parameters.Add("@detalle", SqlDbType.Structured).Value = Obj.Detalles;
                sqlCon.Open();
                comando.ExecuteNonQuery();
                respuesta = "OK";

            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return respuesta;
        }

        public string Anular(int id)
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("venta_anular", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idventa", SqlDbType.Int).Value = id;
                sqlCon.Open();
                comando.ExecuteNonQuery();
                respuesta = "OK";

            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return respuesta;
        }

    }
}
