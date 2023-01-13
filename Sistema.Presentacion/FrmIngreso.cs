using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmIngreso : Form
    {
        private DataTable dtDetalle = new DataTable();

        public FrmIngreso()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                dtgListado.DataSource = NIngreso.Listar();
                Formato();
                Limpiar();
                lblTotal.Text = $"Total de registros: {Convert.ToString(dtgListado.Rows.Count)}";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Buscar()
        {
            try
            {
                dtgListado.DataSource = NIngreso.Buscar(txtBuscar.Text);
                Formato();
                lblTotal.Text = $"Total de registros: {Convert.ToString(dtgListado.Rows.Count)}";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Formato()
        {
            dtgListado.Columns[0].Visible = false;
            dtgListado.Columns[1].Visible = false;
            dtgListado.Columns[2].Visible = false;
            dtgListado.Columns[0].Width = 100;
            dtgListado.Columns[3].Width = 150;
            dtgListado.Columns[4].Width = 150;
            dtgListado.Columns[5].Width = 100;
            dtgListado.Columns[5].HeaderText = "Documento";
            dtgListado.Columns[6].Width = 70;
            dtgListado.Columns[6].HeaderText = "Serie";
            dtgListado.Columns[7].Width = 70;
            dtgListado.Columns[7].HeaderText = "Numero";
            dtgListado.Columns[8].Width = 60;
            dtgListado.Columns[9].Width = 100;
            dtgListado.Columns[10].Width = 100;
            dtgListado.Columns[11].Width = 100;
        }

        private void Limpiar()
        {
            txtBuscar.Clear();
            txtId.Clear();
            btnInsertar.Visible = true;
            errorIcono.Clear();

            dtgListado.Columns[0].Visible = false;
            btnAnular.Visible = false;
            chkSeleccionar.Checked = false;
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistemas de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistemas de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CrearTabla()
        {
            dtDetalle.Columns.Add("idarticulo", Type.GetType("System.Int32"));
            dtDetalle.Columns.Add("codigo", Type.GetType("System.String"));
            dtDetalle.Columns.Add("articulo", Type.GetType("System.String"));
            dtDetalle.Columns.Add("cantidad", Type.GetType("System.Int32"));
            dtDetalle.Columns.Add("precio", Type.GetType("System.Decimal"));
            dtDetalle.Columns.Add("importe", Type.GetType("System.Decimal"));

            dtgDetalle.DataSource = dtDetalle;

            dtgDetalle.Columns[0].Visible = false;
            dtgDetalle.Columns[1].HeaderText = "CODIGO";
            dtgDetalle.Columns[1].Width = 100;
            dtgDetalle.Columns[2].HeaderText = "ARTICULO";
            dtgDetalle.Columns[2].Width = 200;
            dtgDetalle.Columns[3].HeaderText = "CANTIDAD";
            dtgDetalle.Columns[3].Width = 70;
            dtgDetalle.Columns[4].HeaderText = "PRECIO";
            dtgDetalle.Columns[4].Width = 70;
            dtgDetalle.Columns[5].HeaderText = "IMPORTE";
            dtgDetalle.Columns[5].Width = 80;

            dtgDetalle.Columns[1].ReadOnly = true;
            dtgDetalle.Columns[2].ReadOnly = true;
            dtgDetalle.Columns[5].ReadOnly = true;
        }

        private void FrmIngreso_Load(object sender, EventArgs e)
        {
            Listar();
            CrearTabla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            FrmVista_ProveedorIngreso vista = new FrmVista_ProveedorIngreso();
            vista.ShowDialog();
            txtIdProveedor.Text = Convert.ToString(Variables.IdProveedor);
            txtNombrProveedor.Text = Variables.NombreProveedor;
        }
    }
}
