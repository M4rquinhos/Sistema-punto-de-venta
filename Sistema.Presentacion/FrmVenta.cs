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
    public partial class FrmVenta : Form
    {
        private DataTable dtDetalle = new DataTable();
        public FrmVenta()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                dtgListado.DataSource = NVenta.Listar();
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
                dtgListado.DataSource = NVenta.Buscar(txtBuscar.Text);
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
            txtCodigo.Clear();
            txtIdCliente.Clear();
            txtNombreCliente.Clear();
            txtSerieComprobante.Clear();
            txtNumComprobante.Clear();
            dtDetalle.Clear();
            txtSubTotal.Text = "0.00";
            txtTotalImpuesto.Text = "0.00";
            txtTotal.Text = "0.00";

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
            dtDetalle.Columns.Add("stock", Type.GetType("System.Int32"));
            dtDetalle.Columns.Add("cantidad", Type.GetType("System.Int32"));
            dtDetalle.Columns.Add("precio", Type.GetType("System.Decimal"));
            dtDetalle.Columns.Add("descuento", Type.GetType("System.Decimal"));
            dtDetalle.Columns.Add("importe", Type.GetType("System.Decimal"));

            dtgDetalle.DataSource = dtDetalle;

            dtgDetalle.Columns[0].Visible = false;
            dtgDetalle.Columns[1].HeaderText = "CODIGO";
            dtgDetalle.Columns[1].Width = 100;
            dtgDetalle.Columns[2].HeaderText = "ARTICULO";
            dtgDetalle.Columns[2].Width = 200;
            dtgDetalle.Columns[3].HeaderText = "STOCK";
            dtgDetalle.Columns[3].Width = 80;
            dtgDetalle.Columns[4].HeaderText = "CANTIDAD";
            dtgDetalle.Columns[4].Width = 70;
            dtgDetalle.Columns[5].HeaderText = "PRECIO";
            dtgDetalle.Columns[5].Width = 70;
            dtgDetalle.Columns[6].HeaderText = "DESCUENTO";
            dtgDetalle.Columns[6].Width = 80;
            dtgDetalle.Columns[7].HeaderText = "IMPORTE";
            dtgDetalle.Columns[7].Width = 80;

            dtgDetalle.Columns[1].ReadOnly = true;
            dtgDetalle.Columns[2].ReadOnly = true;
            dtgDetalle.Columns[3].ReadOnly = true;
            dtgDetalle.Columns[7].ReadOnly = true;
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

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            Listar();
            CrearTabla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            FrmVista_ClienteVenta vista = new FrmVista_ClienteVenta();
            vista.ShowDialog();
            txtIdCliente.Text = Convert.ToString(Variables.IdCliente);
            txtNombreCliente.Text = Variables.NombreCliente;
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable tabla = new DataTable();
                    tabla = NArticulo.BuscarCodigoBarrasVenta(txtCodigo.Text.Trim());
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
                            Convert.ToInt32(tabla.Rows[0][4]),
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

        private void AgregarDetalle(int idArticulo, string codigo, string nombre, int stock, decimal precio)
        {
            //Impedir que se agreguen articulos repetidos
            bool agregar = true;
            foreach (DataRow filaTemp in dtDetalle.Rows)
            {
                if (Convert.ToInt32(filaTemp["idarticulo"]) == idArticulo)
                {
                    agregar = false;
                    MensajeError($"El articulo {nombre} ya ha sido agregado");
                }
            }

            if (agregar)
            {
                DataRow fila = dtDetalle.NewRow();
                fila["idarticulo"] = idArticulo;
                fila["codigo"] = codigo;
                fila["articulo"] = nombre;
                fila["stock"] = stock;
                fila["cantidad"] = 1;
                fila["precio"] = precio;
                fila["descuento"] = 0;
                fila["importe"] = precio;
                dtDetalle.Rows.Add(fila);
                CalcularTotales();
            }
        }

        private void CalcularTotales()
        {
            decimal total = 0, subtotal = 0;

            if (dtDetalle.Rows.Count == 0)
            {
                total = 0;
            }
            else
            {

                foreach (DataRow filaTemp in dtDetalle.Rows)
                {
                    total += Convert.ToDecimal(filaTemp["importe"]);
                }
            }

            subtotal = total / (1 + Convert.ToDecimal(txtImpuesto.Text));
            txtTotal.Text = total.ToString("#0.00#");
            txtSubTotal.Text = subtotal.ToString("#0.00#");
            txtTotalImpuesto.Text = (total - subtotal).ToString("#0.00#");
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
                dtgArticulos.DataSource = NArticulo.BuscarVenta(txtBuscar.Text.Trim());
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
            int stock;
            idArticulo = Convert.ToInt32(dtgArticulos.CurrentRow.Cells["ID"].Value);
            codigo = Convert.ToString(dtgArticulos.CurrentRow.Cells["Codigo"].Value);
            nombre = Convert.ToString(dtgArticulos.CurrentRow.Cells["Nombre"].Value);
            stock = Convert.ToInt32(dtgArticulos.CurrentRow.Cells["Stock"].Value);
            precio = Convert.ToDecimal(dtgArticulos.CurrentRow.Cells["Precio_Venta"].Value);
            AgregarDetalle(idArticulo, codigo, nombre, stock, precio);
        }

        private void dtgDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataRow fila = (DataRow)dtDetalle.Rows[e.RowIndex];
            string articulo = Convert.ToString(fila["articulo"]);
            int cantidad = Convert.ToInt32(fila["cantidad"]);
            int stock = Convert.ToInt32(fila["stock"]);
            decimal precio = Convert.ToDecimal(fila["precio"]);
            decimal descuento = Convert.ToDecimal(fila["descuento"]);
            if (cantidad > stock)
            {
                cantidad = stock;
                MensajeError($"La cantidad de venta del articulo {articulo} supera el stock disponible {stock}");
                fila["cantidad"] = cantidad;
            }
            fila["importe"] = (precio * cantidad) - descuento;
            CalcularTotales();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (txtIdCliente.Text == string.Empty || txtImpuesto.Text == string.Empty || txtNumComprobante.Text == string.Empty || dtDetalle.Rows.Count == 0)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtIdCliente, "Seleccione un cliente");
                    errorIcono.SetError(txtImpuesto, "Ingrese un impuesto");
                    errorIcono.SetError(dtgDetalle, "Ingrese al menos un detalle");
                }
                else
                {
                    respuesta = NVenta.Insertar(
                        Convert.ToInt32(txtIdCliente.Text),
                        Variables.IdUsuario,
                        cboComprobante.Text.Trim(),
                        txtSerieComprobante.Text.Trim(),
                        txtNumComprobante.Text.Trim(),
                        Convert.ToDecimal(txtImpuesto.Text),
                        Convert.ToDecimal(txtTotal.Text),
                        dtDetalle
                        );
                    if (respuesta.Equals("OK"))
                    {
                        MensajeOk("Registro insertado correctamente");
                        Limpiar();
                        Listar();
                    }
                    else
                    {
                        MensajeError(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            tabGeneral.SelectedIndex = 0;
        }
    }
}
