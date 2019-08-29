using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            frmLogin login = new frmLogin();
            login.ShowDialog();
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rpta;
            rpta = MessageBox.Show("Seguro que desea salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rpta == DialogResult.No)
                e.Cancel = true;
        }

        private void consultarBugsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaBugs frmDetalle = new frmConsultaBugs();
            frmDetalle.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
