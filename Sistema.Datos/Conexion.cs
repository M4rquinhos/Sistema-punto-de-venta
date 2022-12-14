using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class Conexion
    {
        private static Conexion con = null;

        //cadena de conexion

        private Conexion()
        {
           
        }

        public SqlConnection CrearConexion()
        {
            SqlConnection cadena = new SqlConnection();

            try
            {
                cadena.ConnectionString = "Data Source=DESKTOP-4DN5MOQ;Initial Catalog=dbsistema;Integrated Security=True";
            }
            catch (Exception ex)
            {

                cadena = null;
                throw ex;
            }
            return cadena;
        }


        public static Conexion GetInstancia()
        {
            if (con == null)
            {
                con = new Conexion();
            }
            return con;
        }

    }
}
