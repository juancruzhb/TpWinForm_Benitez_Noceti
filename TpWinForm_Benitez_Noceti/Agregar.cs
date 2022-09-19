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
using negocio;

namespace TpWinForm_Benitez_Noceti
{
    public partial class Agregar : Form
    {
        Articulo _articulo;

        private Articulo articulo = null;
        public Agregar()
        {
            InitializeComponent();
        }
        public Agregar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();

            ArticuloNegocio neg = new ArticuloNegocio();
            try
            {
                articulo.Id = _articulo != null ? _articulo.Id : 0;
                articulo.Codigo = txtCod.Text;
                articulo.Nombre = txtNomb.Text;
                articulo.Precio = Convert.ToDouble(txtPrecio.Text);
                articulo.Descripcion = txtDescr.Text;
                articulo.Categoria = (Categoria)cboxCat.SelectedItem;
                articulo.Marca = (Marca)cboxMarca.SelectedItem;
                articulo.Imagen = txtImagen.Text;

                if (articulo.Id == 0)
                {
                    neg.agregar(articulo);
                    MessageBox.Show("Se ha agregado el articulo correctamente", "Agregar Articulo");

                }
                else
                {
                    neg.editar(articulo);
                    MessageBox.Show("Se ha modificado el articulo correctamente", "Modificar Articulo");

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Close();
                //((frmPrincipal)this.Owner).cargarArticulos();
            }
            
            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Agregar_Load(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);

            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca = new MarcaNegocio();


            try
            {
                cboxMarca.DataSource = marca.listar();
                cboxMarca.ValueMember = "Id";
                cboxMarca.DisplayMember = "Descripcion";
                cboxCat.DataSource = categoria.listar();
                cboxCat.ValueMember = "Id";
                cboxCat.DisplayMember = "Descripcion";

                
                cargarArticulo(articulo);


            }
            catch (Exception ex)
            {

                throw ex;
            }


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

        private void txtImagen_TextChanged(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        private void cargarArticulo(Articulo articulo)
        {
            _articulo = articulo;
            if(articulo != null)
            {
                clearForm();
                txtCod.Text = articulo.Codigo;
                txtNomb.Text = articulo.Nombre;
                txtDescr.Text = articulo.Descripcion;
                txtImagen.Text = articulo.Imagen;
                txtPrecio.Text = articulo.Precio.ToString();
                cboxMarca.SelectedValue = articulo.Marca.Id;
                cboxCat.SelectedValue = articulo.Categoria.Id;
            }
        }
        private void clearForm()
        {
            txtCod.Text = string.Empty;
            txtNomb.Text = string.Empty;
            txtDescr.Text = string.Empty;
            txtImagen.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            cboxMarca.SelectedValue = articulo.Marca.Id;
            cboxCat.SelectedValue = articulo.Categoria.Id;
        }
    }
}
