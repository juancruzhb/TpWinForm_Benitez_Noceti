using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar(string busqueda = null)
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;

            try
            {
                conexion.ConnectionString = "Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = CATALOGO_DB; Data Source = .\\SQLEXPRESS";
                conexion.Open();
                string query = @"select a.Id, codigo,nombre,a.descripcion,c.Descripcion Cat,m.Descripcion Mar,imagenurl,Precio, m.Id IdMarca, c.Id IdCategoria 
                                    from ARTICULOS a join CATEGORIAS c on (a.IdCategoria = c.Id) 
                                    join MARCAS m on (a.IdMarca = m.id) 
                                    Where Estado like 1 ";


                if (!string.IsNullOrEmpty(busqueda))
                {
                    query += @"AND (codigo LIKE @busqueda or nombre LIKE @busqueda OR a.Descripcion LIKE @busqueda
                                OR c.Descripcion LIKE @busqueda OR m.Descripcion LIKE @busqueda)";

                    command.Parameters.Add(new SqlParameter("@busqueda", $"%{busqueda}%"));

                }
                command.CommandText = query;
                command.Connection = conexion;


                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)reader["Id"];
                    aux.Codigo = (string)reader["codigo"];
                    aux.Nombre = (string)reader["nombre"];
                    aux.Descripcion = (string)reader["descripcion"];
                    aux.Imagen = (string)reader["imagenurl"];
                    aux.Precio = Convert.ToDouble(reader["Precio"]);

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)reader["IdMarca"];
                    aux.Marca.Descripcion = (string)reader["Mar"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)reader["IdCategoria"];
                    aux.Categoria.Descripcion = (string)reader["Cat"];
                    

                    lista.Add(aux);
                }
                conexion.Close(); 

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
        }

        public void agregar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.query("Insert into articulos (codigo,nombre,descripcion,Precio,IdMarca,IdCategoria, ImagenUrl)VALUES('"+art.Codigo+"','"+art.Nombre+"','"+art.Descripcion+"','"+art.Precio+"',@idMarca,@idCategoria, '"+art.Imagen+"')");
                datos.setearParametros("@idMarca", art.Marca.Id);
                datos.setearParametros("@idCategoria", art.Categoria.Id);
                datos.execQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.query("UPDATE ARTICULOS SET Estado = 0 where Id = @Id");
                datos.setearParametros("@Id", id);
                datos.execQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void editar(Articulo articulo)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();

                datos.query(@"UPDATE ARTICULOS
                                SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion,
                                    IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl,
                                    Precio = @precio
                                WHERE ID = @Id");

                datos.setearParametros("@Codigo", articulo.Codigo);
                datos.setearParametros("@Nombre", articulo.Nombre);
                datos.setearParametros("@Descripcion", articulo.Descripcion);
                datos.setearParametros("@IdMarca", articulo.Marca.Id);
                datos.setearParametros("@IdCategoria", articulo.Categoria.Id);
                datos.setearParametros("@ImagenUrl", articulo.Imagen);
                datos.setearParametros("@Precio", articulo.Precio);
                datos.setearParametros("@Id", articulo.Id);

                datos.execQuery();


            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
