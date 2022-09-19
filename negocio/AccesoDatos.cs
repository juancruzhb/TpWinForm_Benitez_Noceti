using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    public class AccesoDatos
    {

        private SqlConnection conexion;
        private SqlCommand command;
        private SqlDataReader reader;
        public SqlDataReader Reader
        {
            get { return reader; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CATALOGO_DB;Data Source=.\\SQLEXPRESS");
            command = new SqlCommand();
        }

        public void query(string query)
        {
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;
        }

        public void exec()
        {
            command.Connection = conexion;
            try
            {
                conexion.Open();
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void execQuery()
        {
            command.Connection= conexion;
            try
            {
                conexion.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void setearParametros(string Nombre, object valor)
        {
            command.Parameters.AddWithValue(Nombre, valor);
        }

        public void cerrarConexion()
        {
            if (reader != null)
                reader.Close();
            conexion.Close();
        }

    }
}

