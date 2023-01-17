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

        private void FormatoArticulos()
        {
            dtgArticulos.Columns[1].Visible = false;
            dtgArticulos.Columns[2].Width = 100;
            dtgArticulos.Columns[2].HeaderText = "Categoria";
            dtgArticulos.Columns[3].Width = 100;
            dtgArticulos.Columns[3].HeaderText = "Codigo";
            dtgArticulos.Columns[4].Width = 150;
            dtgArticulos.Columns[5].Width = 100;
            dtgArticulos.Columns[5].HeaderText = "Precio venta";
            dtgArticulos.Columns[6].Width = 60;
            dtgArticulos.Columns[7].Width = 200;
            dtgArticulos.Columns[7].HeaderText = "Descripcion";
            dtgArticulos.Columns[8].Width = 100;
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

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable tabla = new DataTable();
                    tabla = NArticulo.BuscarCodigoBarras(txtCodigo.Text.Trim());
                    if (tabla.Rows.Count <= 0)
                    {
                        MensajeError($"No existe ningun articulo con el codigo {txtCodigo.Text} asignado");
                    }
                    else
                    {
                        //Agregar al detalle
                        AgregarDetalle(
                            Convert.ToInt32(tabla.Rows[0][0]),
                            Convert.ToString(tabla.Rows[0][1]),
                            Convert.ToString(tabla.Rows[0][2]),
                            Convert.ToDecimal(tabla.Rows[0][3])
                            );
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void AgregarDetalle(int idArticulo, string codigo, string nombre, decimal precio)
        {
            DataRow fila = dtDetalle.NewRow();
            fila["idarticulo"] = idArticulo;
            fila["codigo"] = codigo;
            fila["articulo"] = nombre;
            fila["cantidad"] = 1;
            fila["precio"] = precio;
            fila["importe"] = precio;
            dtDetalle.Rows.Add(fila);
        }

        private void btnVerArticulos_Click(object sender, EventArgs e)
        {
            panelArticulos.Visible = true;
        }

        private void btnCerrarArticulo_Click(object sender, EventArgs e)
        {
            panelArticulos.Visible = false;
        }

        private void btnFiltrarArticulos_Click(object sender, EventArgs e)
        {
            try
            {
                dtgArticulos.DataSource = NArticulo.Buscar(txtBuscar.Text.Trim());
                FormatoArticulos();
                lblTotalArticulos.Text = $"Total registros: {Convert.ToString(dtgArticulos.Rows.Count)}";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dtgArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idArticulo;
            string codigo, nombre;
            decimal precio;
            idArticulo = Convert.ToInt32(dtgArticulos.CurrentRow.Cells["ID"].Value);
            codigo = Convert.ToString(dtgArticulos.CurrentRow.Cells["Codigo"].Value);
            nombre = Convert.ToString(dtgArticulos.CurrentRow.Cells["Nombre"].Value);
            precio = Convert.ToDecimal(dtgArticulos.CurrentRow.Cells["Precio_Venta"].Value);
            AgregarDetalle(idArticulo, codigo, nombre, precio);
        }
    }
}
