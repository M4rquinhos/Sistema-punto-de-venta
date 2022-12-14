using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class DCategoria
    {
        public DataTable Listar()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion(); //Creando la conexion con la BD
                SqlCommand comando = new SqlCommand("categoria_listar", sqlCon); //recibe que es lo que se quiere hacer (SP O QUERY)
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
                SqlCommand comando = new SqlCommand("categoria_buscar", sqlCon); 
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

        public string Existe(string valor)
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("categoria_existe", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                SqlParameter parExiste = new SqlParameter();
                parExiste.ParameterName = "@existe";
                parExiste.SqlDbType = SqlDbType.Int;
                parExiste.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parExiste);
                sqlCon.Open();
                comando.ExecuteNonQuery();
                respuesta = Convert.ToString(parExiste.Value);

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

        public string Insertar(Categoria Obj)
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("categoria_insertar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo insertar el registro";
                return respuesta;

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

        public string Actualizar(Categoria Obj) 
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("categoria_actualizar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Obj.IdCategoria;
                comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";
                return respuesta;

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

        public string Eliminar(int id)
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("categoria_eliminar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
                return respuesta;

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

        public string Activar(int id)
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("categoria_activar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro";
                return respuesta;

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

        public string Desactivar(int id)
        {
            string respuesta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = Conexion.GetInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("categoria_desactivar", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivar el registro";
                return respuesta;

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
