﻿using System;
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
    public partial class FrmProveedor : Form
    {
        public FrmProveedor()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                dtgListado.DataSource = NPersona.ListarProveedores();
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
                dtgListado.DataSource = NPersona.BuscarProveedores(txtBuscar.Text);
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
            dtgListado.Columns[8].Width = 120;//email
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
            btnEliminar.Visible = false;
            chkSeleccionar.Checked = false;
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistemas de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
