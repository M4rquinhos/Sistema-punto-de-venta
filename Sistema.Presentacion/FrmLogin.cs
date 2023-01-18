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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tabla = new DataTable();
                tabla = NUsuario.Login(txtEmail.Text.Trim(), txtClave.Text.Trim());
                if (tabla.Rows.Count <= 0)
                {
                    MessageBox.Show("El email o la clave son incorrectos", "Acceso al sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Convert.ToBoolean(tabla.Rows[0][4]) == false)
                    {
                        MessageBox.Show("Este usuario no esta activo", "Acceso al sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        FrmPrincipal frm = new FrmPrincipal();
                        Variables.IdUsuario = Convert.ToInt32(tabla.Rows[0][0]);
                        frm.idUsuario = Convert.ToInt32(tabla.Rows[0][0]);
                        frm.idRol = Convert.ToInt32(tabla.Rows[0][1]);
                        frm.rol = Convert.ToString(tabla.Rows[0][2]);
                        frm.nombre = Convert.ToString(tabla.Rows[0][3]);
                        frm.estado = Convert.ToBoolean(tabla.Rows[0][4]);
                        frm.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
