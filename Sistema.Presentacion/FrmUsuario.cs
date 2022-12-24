﻿using Sistema.Negocio;
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
            txtDescripcion.Clear();
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

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}