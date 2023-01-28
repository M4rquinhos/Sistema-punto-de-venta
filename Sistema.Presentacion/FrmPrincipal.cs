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
    public partial class FrmPrincipal : Form
    {
        private int childFormNumber = 0;
        public int idUsuario;
        public int idRol;
        public string nombre;
        public string rol;
        public bool estado;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategoria frm = new FrmCategoria();
            frm.MdiParent = this;
            frm.Show();
        }

        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArticulo frm = new FrmArticulo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            stBarraInf.Text = $"Desarrolldo por www.marquinos.com, Usuario: {nombre}";
            MessageBox.Show($"Bienvenido: {nombre}", "Sistema Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //aquí podria ser un switch(?
            if (rol.Equals("Administrador"))
            {
                mnuAlmacen.Enabled = true;
                mnuIngresos.Enabled = true;
                mnuVentas.Enabled = true;
                mnuAccesos.Enabled = true;
                mnuConsultas.Enabled = true;
                tsCompras.Enabled = true;
                tsVentas.Enabled = true;
            }
            else
            {
                if (rol.Equals("Vendedor"))
                {
                    mnuAlmacen.Enabled = false;
                    mnuIngresos.Enabled = false;
                    mnuVentas.Enabled = true;
                    mnuAccesos.Enabled = false;
                    mnuConsultas.Enabled = true;
                    tsCompras.Enabled = false;
                    tsVentas.Enabled = true;
                }
                else
                {
                    if (rol.Equals("Almacenista"))
                    {
                        mnuAlmacen.Enabled = true;
                        mnuIngresos.Enabled = true;
                        mnuVentas.Enabled = false;
                        mnuAccesos.Enabled = false;
                        mnuConsultas.Enabled = true;
                        tsCompras.Enabled = true;
                        tsVentas.Enabled = false;
                    }
                    else
                    {
                        mnuAlmacen.Enabled = false;
                        mnuIngresos.Enabled = false;
                        mnuVentas.Enabled = false;
                        mnuAccesos.Enabled = false;
                        mnuConsultas.Enabled = false;
                        tsCompras.Enabled = false;
                        tsVentas.Enabled = false;
                    }
                }
            }
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRol frm = new FrmRol();
            frm.MdiParent = this;
            frm.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuario frm = new FrmUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult opcion;

            opcion = MessageBox.Show("¿Deseas salir del sistema?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (opcion == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProveedor frm = new FrmProveedor();
            frm.MdiParent = this;
            frm.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCliente frm = new FrmCliente();
            frm.MdiParent = this;
            frm.Show();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIngreso frm = new FrmIngreso();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVenta frm = new FrmVenta();
            frm.MdiParent = this;
            frm.Show();
        }

        private void consultaVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConsulta_Venta_Fechas frm = new FrmConsulta_Venta_Fechas();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
