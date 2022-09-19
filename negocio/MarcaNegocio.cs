using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.query("Select Id,Descripcion from MARCAS");
                datos.exec();

                while (datos.Reader.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (int)datos.Reader["Id"];
                    marca.Descripcion = (string)datos.Reader["Descripcion"];
                    lista.Add(marca);

                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }


    }
}
