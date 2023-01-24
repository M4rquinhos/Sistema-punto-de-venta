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
    public partial class FrmVista_ClienteVenta : Form
    {
        public FrmVista_ClienteVenta()
        {
            InitializeComponent();
        }

        private void Buscar()
        {
            try
            {
                dtgListado.DataSource = NPersona.BuscarClientes(txtBuscar.Text);
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
            dtgListado.Columns[0].Visible = false; //seleccionar
            dtgListado.Columns[1].Width = 50; // id
            dtgListado.Columns[2].Width = 100;
            dtgListado.Columns[2].HeaderText = "Tipo persona";
            dtgListado.Columns[3].Width = 170; //Nombre
            dtgListado.Columns[4].Width = 100;
            dtgListado.Columns[4].HeaderText = "Documento";
            dtgListado.Columns[5].Width = 100;
            dtgListado.Columns[5].HeaderText = "Numero Doc";
            dtgListado.Columns[6].Width = 120;
            dtgListado.Columns[6].HeaderText = "Direccion";
            dtgListado.Columns[7].Width = 100;
            dtgListado.Columns[7].HeaderText = "Telefono";
            dtgListado.Columns[8].Width = 170;//email
        }

        private void FrmVista_ClienteVenta_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void dtgListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Variables.IdCliente = Convert.ToInt32(dtgListado.CurrentRow.Cells["ID"].Value);
            Variables.NombreCliente = Convert.ToString(dtgListado.CurrentRow.Cells["Nombre"].Value);
            Close();
        }
    }
}
