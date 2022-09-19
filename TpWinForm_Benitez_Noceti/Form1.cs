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
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulos;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Agregar agregar = new Agregar();
            agregar.ShowDialog(this);
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargarArticulos();

        }

        public void cargarArticulos(string busqueda = null)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            if(!string.IsNullOrEmpty(busqueda)) 
            {
                listaArticulos = negocio.listar(busqueda);
                dgvArticulos.DataSource = listaArticulos;
            }
            else
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
            }

        }



        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            VistaPrevia vista = new VistaPrevia();
            Articulo articulo = new Articulo();

            articulo.Codigo = dgvArticulos.Rows[e.RowIndex].Cells[0].Value.ToString();
            articulo.Nombre = dgvArticulos.Rows[e.RowIndex].Cells[1].Value.ToString();
            articulo.Descripcion = dgvArticulos.Rows[e.RowIndex].Cells[2].Value.ToString();
            articulo.Marca = (Marca)dgvArticulos.Rows[e.RowIndex].Cells[3].Value;
            articulo.Categoria = (Categoria)dgvArticulos.Rows[e.RowIndex].Cells[4].Value;
            articulo.Precio = (double)dgvArticulos.Rows[e.RowIndex].Cells[5].Value;
            articulo.Imagen = dgvArticulos.Rows[e.RowIndex].Cells[6].Value.ToString();




            vista.cargarArticulo(articulo);
        }

        private void dgvArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)dgvArticulos.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if(cell.Value.ToString() == "Editar")
            {
                Articulo seleccionado;
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                Agregar agregar = new Agregar(seleccionado);
                agregar.ShowDialog();
            }
            if(cell.Value.ToString() == "Eliminar")
            {
                string mensaje = "Desea eliminar este articulo?";
                string caption = "Eliminacion de articulo";

                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(mensaje, caption, botones);
                if(result == System.Windows.Forms.DialogResult.Yes)
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(seleccionado.Id);
                    MessageBox.Show("Se ha eliminado el articulo seleccionado");
                    cargarArticulos();
                }
              
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cargarArticulos(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }
    }
}
