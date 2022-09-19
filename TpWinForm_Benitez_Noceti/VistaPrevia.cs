using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;

namespace TpWinForm_Benitez_Noceti
{
    public partial class VistaPrevia : Form
    {
        public VistaPrevia()
        {
            InitializeComponent();
        }


        public void cargarArticulo(Articulo articulo)
        {
            txtCod.Text = articulo.Codigo;
            txtNomb.Text = articulo.Nombre;
            txtDescr.Text = articulo.Descripcion;
            txtImagen.Text = articulo.Imagen;
            txtCategoria.Text = articulo.Categoria.ToString();
            txtMarca.Text = articulo.Marca.ToString();
            txtPrecio.Text = articulo.Precio.ToString();
            cargarImagen(articulo.Imagen);
            this.ShowDialog();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);

            }
            catch (Exception)
            {

                pbxArticulo.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png");

            }
        }
    }
}
