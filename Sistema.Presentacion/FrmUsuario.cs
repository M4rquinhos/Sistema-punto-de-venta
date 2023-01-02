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
    public partial class FrmUsuario : Form
    {
        private string emailAnt;
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                dtgListado.DataSource = NUsuario.Listar();
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
                dtgListado.DataSource = NUsuario.Buscar(txtBuscar.Text);
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
            dtgListado.Columns[2].Visible = false;
            dtgListado.Columns[1].Width = 50; //ID
            dtgListado.Columns[3].Width = 100; //nombre rol
            dtgListado.Columns[4].Width = 170; //nombre user
            dtgListado.Columns[5].Width = 100; //tipo documento
            dtgListado.Columns[5].HeaderText = "Documento";
            dtgListado.Columns[6].Width = 100;
            dtgListado.Columns[6].HeaderText = "Numero Doc";
            dtgListado.Columns[7].Width = 120;
            dtgListado.Columns[7].HeaderText = "Direccion";
            dtgListado.Columns[8].Width = 100;
            dtgListado.Columns[8].HeaderText = "Telefono";
            dtgListado.Columns[9].Width = 120;
        }

        private void Limpiar()
        {
            txtBuscar.Clear();
            txtId.Clear();
            txtNombre.Clear();
            txtNumDoc.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            txtClave.Clear();
            txtTelefono.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            errorIcono.Clear();

            dtgListado.Columns[0].Visible = false;
            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
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

        private void CargarRol()
        {
            try
            {
                cboRol.DataSource = NRol.Listar();
                cboRol.ValueMember = "idrol";
                cboRol.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            Listar();
            CargarRol();
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
                if ( cboRol.Text == string.Empty || txtNombre.Text == string.Empty || txtEmail.Text == string.Empty || txtClave.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(cboRol, "Ingrese un rol");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                    errorIcono.SetError(txtEmail, "Ingrese un email");
                    errorIcono.SetError(txtClave, "Ingrese la clave");
                }
                else
                {
                    respuesta = NUsuario.Insertar(
                        Convert.ToInt32(cboRol.SelectedValue),
                        txtNombre.Text.Trim(),
                        cboTipoDoc.Text.Trim(),
                        txtNumDoc.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtClave.Text.Trim()
                        );
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
                cboRol.SelectedValue = Convert.ToString(dtgListado.CurrentRow.Cells["idrol"].Value);
                txtNombre.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Nombre"].Value);
                cboTipoDoc.SelectedValue = Convert.ToString(dtgListado.CurrentRow.Cells["Tipo_documento"].Value);
                txtNumDoc.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Num_documento"].Value);
                txtDireccion.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Direccion"].Value);
                txtTelefono.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Telefono"].Value);
                emailAnt = Convert.ToString(dtgListado.CurrentRow.Cells["Email"].Value);
                txtEmail.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Email"].Value);
                tabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Seleccione desde la celda nombre | Error:{ex.Message + ex.StackTrace}");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (txtId.Text == string.Empty || cboRol.Text == string.Empty || txtNombre.Text == string.Empty || txtEmail.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(cboRol, "Ingrese un rol");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                    errorIcono.SetError(txtEmail, "Ingrese un email");
                }
                else
                {
                    respuesta = NUsuario.Actualizar(
                        Convert.ToInt32(txtId.Text),
                        Convert.ToInt32(cboRol.SelectedValue),
                        txtNombre.Text.Trim(),
                        cboTipoDoc.Text.Trim(),
                        txtNumDoc.Text.Trim(),
                        txtDireccion.Text.Trim(),
                        txtTelefono.Text.Trim(),
                        emailAnt,
                        txtEmail.Text.Trim(),
                        txtClave.Text.Trim()
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

        private void dtgListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtgListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dtgListado.Rows[e.RowIndex].Cells["Seleccionar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSeleccionar.Checked)
            {
                dtgListado.Columns[0].Visible = true;
                btnActivar.Visible = true;
                btnDesactivar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                dtgListado.Columns[0].Visible = false;
                btnActivar.Visible = false;
                btnDesactivar.Visible = false;
                btnEliminar.Visible = false;
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
                            respuesta = NUsuario.Eliminar(codigo);

                            if (respuesta.Equals("OK"))
                            {
                                MensajeOk($"Se elimino el registro correctamente: {Convert.ToString(row.Cells[4].Value)} ");
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

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Quieres desactivar el o los registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";
                    foreach (DataGridViewRow row in dtgListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NUsuario.Desactivar(codigo);

                            if (respuesta.Equals("OK"))
                            {
                                MensajeOk($"Se desactivo el registro correctamente: {Convert.ToString(row.Cells[4].Value)} ");
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

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Quieres activar el o los registro(s)?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";
                    foreach (DataGridViewRow row in dtgListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NUsuario.Activar(codigo);

                            if (respuesta.Equals("OK"))
                            {
                                MensajeOk($"Se activo el registro correctamente: {Convert.ToString(row.Cells[4].Value)} ");
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
    }
}
