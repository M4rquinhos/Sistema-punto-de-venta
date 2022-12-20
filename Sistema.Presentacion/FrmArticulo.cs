using BarcodeLib;
using Sistema.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmArticulo : Form
    {
        private string rutaOrigen;
        private string rutaDestino;
        private string directorio = @"E:\Documentos\dev\cursos\cursos\c#\ImSistema";
        private string NombreAnt;
        public FrmArticulo()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                dtgListado.DataSource = NArticulo.Listar();
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
                dtgListado.DataSource = NArticulo.Buscar(txtBuscar.Text);
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
            dtgListado.Columns[0].Width = 100;
            dtgListado.Columns[1].Width = 50; //ID
            dtgListado.Columns[3].Width = 100; 
            dtgListado.Columns[3].HeaderText = "Categoria";
            dtgListado.Columns[4].Width = 100;
            dtgListado.Columns[4].HeaderText = "Código";
            dtgListado.Columns[5].Width = 150; //Nombre
            dtgListado.Columns[6].Width = 100;
            dtgListado.Columns[6].HeaderText = "Precio Venta";
            dtgListado.Columns[7].Width = 60; //Stock
            dtgListado.Columns[8].Width = 200;
            dtgListado.Columns[8].HeaderText = "Descripción";
            dtgListado.Columns[9].Width = 100; //Imagen
            dtgListado.Columns[10].Width = 100; //Estado
        }

        private void Limpiar()
        {
            txtBuscar.Clear();
            txtId.Clear();
            txtNombre.Clear();
            txtCodigo.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();
            txtImagen.Clear();
            picImagen.Image = null;
            pnlcodigo.BackgroundImage = null;
            btnGuardarCod.Enabled = true;
            txtDescripcion.Clear();
            btnInsertar.Visible = true;
            btnActualizar.Visible = false;
            errorIcono.Clear();
            rutaDestino = "";
            rutaOrigen = "";

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

        private void CargarCategoria()
        {
            try
            {
                cboCategoria.DataSource = NCategoria.Seleccionar();
                cboCategoria.ValueMember = "idcategoria";
                cboCategoria.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            Listar();
            CargarCategoria();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                picImagen.Image = Image.FromFile(file.FileName);
                txtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf(@"\" ) + 1);
                rutaOrigen = file.FileName;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Barcode codigo = new Barcode();
            codigo.IncludeLabel = true;
            pnlcodigo.BackgroundImage = codigo.Encode(TYPE.CODE128, txtCodigo.Text, Color.Black, Color.White, 400, 100);
            btnGuardarCod.Enabled = true;
        }

        private void btnGuardarCod_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)pnlcodigo.BackgroundImage.Clone();

            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.AddExtension = true;
            dialogoGuardar.Filter = "Image PNG (*.png)|*.png ";
            dialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(dialogoGuardar.FileName))
            {
                imgFinal.Save(dialogoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (cboCategoria.Text == string.Empty || txtNombre.Text == string.Empty || txtPrecioVenta.Text == string.Empty || txtStock.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(cboCategoria, "Ingrese una categoria");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                    errorIcono.SetError(txtPrecioVenta, "Ingrese el precio de venta");
                    errorIcono.SetError(txtStock, "Ingrese el stock");
                }
                else
                {
                    respuesta = NArticulo.Insertar(
                        Convert.ToInt32(cboCategoria.SelectedValue), 
                        txtCodigo.Text, 
                        txtNombre.Text.Trim(), 
                        Convert.ToDecimal(txtPrecioVenta.Text), 
                        Convert.ToInt32(txtStock.Text), 
                        txtDescripcion.Text.Trim(), 
                        txtImagen.Text.Trim()
                        );

                    if (respuesta.Equals("OK"))
                    {
                        MensajeOk("Registro insertado correctamente");
                        if (txtImagen.Text != string.Empty)
                        {
                            rutaDestino = directorio + txtImagen.Text;
                            File.Copy(rutaOrigen, rutaDestino);  
                        }
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
                cboCategoria.SelectedValue = Convert.ToString(dtgListado.CurrentRow.Cells["idcategoria"].Value);
                txtCodigo.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Codigo"].Value);
                NombreAnt = Convert.ToString(dtgListado.CurrentRow.Cells["Nombre"].Value);
                txtNombre.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Nombre"].Value);
                txtPrecioVenta.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Precio_Venta"].Value);
                txtStock.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Stock"].Value);
                txtDescripcion.Text = Convert.ToString(dtgListado.CurrentRow.Cells["Descripcion"].Value);
                string imagen = Convert.ToString(dtgListado.CurrentRow.Cells["Imagen"].Value);
                if (imagen != string.Empty)
                {
                    picImagen.Image = Image.FromFile(directorio + imagen);
                    txtImagen.Text = imagen;
                }
                else
                {
                    picImagen.Image = null;
                    txtImagen.Text = "";
                }
                tabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Seleccione desde la celda nombre." + ex.Message + ex.StackTrace);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (txtId.Text == string.Empty || cboCategoria.Text == string.Empty || txtNombre.Text == string.Empty || txtPrecioVenta.Text == string.Empty || txtStock.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    errorIcono.SetError(cboCategoria, "Ingrese una categoria");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                    errorIcono.SetError(txtPrecioVenta, "Ingrese el precio de venta");
                    errorIcono.SetError(txtStock, "Ingrese el stock");
                }
                else
                {
                    respuesta = NArticulo.Actualizar(
                        Convert.ToInt32(txtId.Text),
                        Convert.ToInt32(cboCategoria.SelectedValue),
                        txtCodigo.Text,
                        NombreAnt,
                        txtNombre.Text.Trim(),
                        Convert.ToDecimal(txtPrecioVenta.Text),
                        Convert.ToInt32(txtStock.Text),
                        txtDescripcion.Text.Trim(),
                        txtImagen.Text.Trim()
                        );

                    if (respuesta.Equals("OK"))
                    {
                        MensajeOk("Registro actualizado correctamente");
                        if (txtImagen.Text != string.Empty && rutaOrigen != string.Empty)
                        {
                            rutaDestino = directorio + txtImagen.Text;
                            File.Copy(rutaOrigen, rutaDestino);
                        }
                        Listar();
                        tabGeneral.SelectedIndex = 0;
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
