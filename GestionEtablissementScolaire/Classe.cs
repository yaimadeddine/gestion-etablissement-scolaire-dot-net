using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GestionEtablissementScolaire
{
    public partial class Classe : Form
    {
        int currRowIndex;
        int s = 0;
        string idactuelle = "0";
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=dotnetdb";
        MySqlConnection maconnexion;
        public Classe()
        {
            InitializeComponent();
            fetchdata();
            guna2DataGridView1.ColumnHeadersHeight = 24;
        }

        public void fetchdata()
        {
            try
            {
                DataTable dataTable = new DataTable();

                this.guna2DataGridView1.DataSource = null;


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select * from classe";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                guna2DataGridView1.DataSource = dataTable;
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            idactuelle = row.Cells[0].Value.ToString();
            s = 1;
            guna2TextBox1.Text = row.Cells[1].Value.ToString();
            guna2NumericUpDown1.Value = decimal.Parse(row.Cells[2].Value.ToString());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string capacite = guna2NumericUpDown1.Value.ToString();
             
               
                string Query = "insert into classe(id,libelle,capacite) values(null,'" + guna2TextBox1.Text + "','" + capacite + "')";

                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MyConn2.Open();
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;

                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Classe bien ajouter");
                fetchdata();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (s == 0)
            {
                MessageBox.Show("Veuillez Selectionner Un Ligne ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                int rowIndex = guna2DataGridView1.CurrentCell.RowIndex;


                try
                {
                    string capacite = guna2NumericUpDown1.Value.ToString();
                
                    string Query = "update classe set libelle= '" + guna2TextBox1.Text + "' ,capacite='" + capacite + "' where id= " + idactuelle + "";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;
                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Classe bien modifier", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fetchdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int rowIndex = guna2DataGridView1.CurrentCell.RowIndex;
            if (s == 0)
            {
                MessageBox.Show("Veuillez Selectionner Un Ligne ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string Query = "delete from classe where id=" + idactuelle + "";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Classe supprimé");
                    fetchdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
              
            }
        }
    }
}
