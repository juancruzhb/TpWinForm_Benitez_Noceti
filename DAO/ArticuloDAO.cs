using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAO
{
    public class ArticuloDAO
    {
        private SqlConnection cn;
        private SqlCommand cmd;
        private SqlDataReader dr;
        public SqlDataReader Dr
        {
            get { return dr; }
        }

        public ArticuloDAO()
        {
            cn = new SqlConnection("server=DESKTOP-ED31VDF; database=CATALOGO_DB; integrated security=true");
            cmd = new SqlCommand();
        }

        public void query(string query)
        {
            cmd.CommandType=System.Data.CommandType.Text;
            cmd.CommandText=query;
        }

        public void exec()
        {
            cmd.Connection = cn;
            try
            {
                cn.Open();
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void cerrarCN()
        {
            if(dr != null)
                dr.Close();
            cn.Close();
        }


    }

    
    
}
