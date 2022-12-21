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
    public partial class FrmRol : Form
    {
        public FrmRol()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                dtgListado.DataSource = NRol.Listar();
                Formato();
                lblTotal.Text = $"Total de registros: {Convert.ToString(dtgListado.Rows.Count)}";

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Formato()
        {
            dtgListado.Columns[0].Width = 100;
            dtgListado.Columns[0].HeaderText = "ID";
            dtgListado.Columns[1].Width = 200;
            dtgListado.Columns[1].HeaderText = "Nombre";
        }

        private void FrmRol_Load(object sender, EventArgs e)
        {
            Listar();
        }
    }
}
