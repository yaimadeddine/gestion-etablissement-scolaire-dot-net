using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionEtablissementScolaire
{
    public partial class Home : Form
    {
        private Form activeForm;
        public Home()
        {
            InitializeComponent();
            openChildForm(new HomeIntern());
        }
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(childForm);
            this.panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void buttonStudent_Click(object sender, EventArgs e)
        {
            openChildForm(new Student());
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Prof());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            openChildForm(new Materiel());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            openChildForm(new Classe());
            
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            openChildForm(new HomeIntern());
        }
    }
}
