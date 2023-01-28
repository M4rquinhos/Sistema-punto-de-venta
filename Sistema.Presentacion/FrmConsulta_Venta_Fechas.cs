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
using Sistema.Presentacion.Reportes;

namespace Sistema.Presentacion
{
    public partial class FrmConsulta_Venta_Fechas : Form
    {
        public FrmConsulta_Venta_Fechas()
        {
            InitializeComponent();
        }

        private void ConsultaFechas()
        {
            try
            {
                dtgListado.DataSource = NVenta.ConsultaFechas(Convert.ToDateTime(dtpFechaInicio.Value), Convert.ToDateTime(dtpFechaFin.Value));
                Formato();
                Limpiar();
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
            dtgListado.Columns[0].Visible = false;
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistemas de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistemas de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmConsulta_Venta_Fechas_Load(object sender, EventArgs e)
        {
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ConsultaFechas();
        }

        private void dtgListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dtgMostrarDetalle.DataSource = NVenta.ListarDetalle(
                    Convert.ToInt32(dtgListado.CurrentRow.Cells["ID"].Value)
                );
                decimal total, subtotal;
                decimal impuesto = Convert.ToDecimal(dtgListado.CurrentRow.Cells["Impuesto"].Value);
                total = Convert.ToDecimal(dtgListado.CurrentRow.Cells["Total"].Value);
                subtotal = total / (1 + impuesto);
                txtSubTotalD.Text = subtotal.ToString("#0.00#");
                txtTotalD.Text = total.ToString("#0.00#");
                txtTotalImpuestoD.Text = (total - subtotal).ToString("#0.00#");
                PanelMostrarVenta.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                Variables.IdVenta = Convert.ToInt32(dtgListado.CurrentRow.Cells["ID"].Value);
                FrmReporteComprobanteVenta comprobante = new FrmReporteComprobanteVenta();
                comprobante.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCerrarDetalle_Click(object sender, EventArgs e)
        {
            PanelMostrarVenta.Visible = false;
        }
    }
}
