using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema.Negocio;

namespace Sistema.Presentacion
{
    public partial class FrmCliente : Form
    {
        private string nombreAnt;
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                dtgListado.DataSource = NPersona.ListarClientes();
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

        private void Limpiar()
        {
            txtBuscar.Clear();
            txtId.Clear();
            txtNombre.Clear();
            txtNumDoc.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            errorIcono.Clear();

            dtgListado.Columns[0].Visible = false;
            btnEliminar.Visible = false;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (txtNombre.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    respuesta = NPersona.Insertar(
                        "Cliente", //Tipo persona; es mejor declarar un variable.
                        txtNombre.Text.Trim(),
                        cboTipoDoc.Text.Trim(),
                        txtNumDoc.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtEmail.Text.Trim());

                    if (respuesta.Equals("OK"))
                    {
                        MensajeOk("Registro insertado correctamente");
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

        private void dtgListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                txtId.Text = Convert.ToString(dtgListado.CurrentRow.Cells["ID"].Value);
                nombreAnt = Convert.ToString(dtgListado.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Nombre"].Value);
                cboTipoDoc.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Tipo_Documento"].Value);
                txtNumDoc.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Num_Documento"].Value);
                txtDireccion.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Direccion"].Value);
                txtTelefono.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Telefono"].Value);
                txtEmail.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Email"].Value);
                tabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Seleccione desde la celda nombre | Error: {ex.Message + ex.StackTrace}");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if ( txtId.Text == string.Empty || txtNombre.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    respuesta = NPersona.Actualizar(
                        Convert.ToInt32(txtId.Text),
                        "Cliente", //Tipo persona; es mejor declarar un variable.
                        nombreAnt,
                        txtNombre.Text.Trim(),
                        cboTipoDoc.Text.Trim(),
                        txtNumDoc.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtEmail.Text.Trim()
                        );
                    if (respuesta.Equals("OK"))
                    {
                        MensajeOk("Registro actualizado correctamente");
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

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                dtgListado.Columns[0].Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                dtgListado.Columns[0].Visible = false;
                btnEliminar.Visible = false;
            }
        }

        private void dtgListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtgListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dtgListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Quieres eliminar el o los registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";
                    foreach (DataGridViewRow row in dtgListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NPersona.Eliminar(codigo);

                            if (respuesta.Equals("OK"))
                            {
                                MensajeOk($"Se elimino el registro correctamente: {Convert.ToString(row.Cells[3].Value)} ");
                            }
                            else
                            {
                                MensajeError(respuesta);
                            }
                        }
                    }
                    Listar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Listar();
        }
    }
}
