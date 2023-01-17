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

namespace GestionEtablissementScolaire
{
    public partial class Student : Form
    {
        int currRowIndex;
        int s = 0;
        string idactuelle = "0";
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=dotnetdb";
        MySqlConnection maconnexion;
        public Student()
        {
            InitializeComponent();
            fetchdata();
            dataGridView1.ColumnHeadersHeight = 24;
        }
        public void fetchdata()
        {
            try
            {
                DataTable dataTable = new DataTable();

                this.dataGridView1.DataSource = null;


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select * from etudiant";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {

            if (textBoxName.Text == "" || textBoxCne.Text == "" || textBoxPrenom.Text == "")
            {
                MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                try
                {
                    string Query = "insert into etudiant(id,nom,prenom,cne,datenaissance) values(null,'" + textBoxName.Text + "','" + textBoxPrenom.Text + "','" + textBoxCne.Text + "','" + dateTimePicker1.Value.ToString("yyy-MM-d ") + "')";

                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Etudiant bien ajouter");
                    fetchdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                textBoxName.Text = "";
                textBoxCne.Text = "";
                textBoxPrenom.Text = "";
            }
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            if (s == 0)
            {
                MessageBox.Show("Veuillez Selectionner Un Ligne ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                if (textBoxName.Text == "" || textBoxPrenom.Text == "" || textBoxCne.Text == "")
                {
                    MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        string Query = "update etudiant set nom= '" + textBoxName.Text + "' ,prenom='" + textBoxPrenom.Text + "',cne='" + textBoxCne.Text + "', datenaissance= '" + dateTimePicker1.Value.ToString("yyy-MM-d ") + "' where id= " + idactuelle + "";
                        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                        MyConn2.Open();
                        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                        MySqlDataReader MyReader2;
                        MyReader2 = MyCommand2.ExecuteReader();
                        MessageBox.Show("etudiant bien modifier", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fetchdata();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    textBoxName.Text = "";
                    textBoxPrenom.Text = "";
                    textBoxCne.Text = "";
                }
            }
        }

        private void buttonSupprimer_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            if (s == 0)
            {
                MessageBox.Show("Veuillez Selectionner Un Ligne ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    string Query = "delete from etudiant where id=" + idactuelle + "";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Etudiant supprimé");
                    fetchdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                textBoxName.Text = "";
                textBoxPrenom.Text = "";
                textBoxCne.Text = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            idactuelle = row.Cells[0].Value.ToString();
            s = 1;
            textBoxName.Text = row.Cells[1].Value.ToString();
            textBoxPrenom.Text = row.Cells[2].Value.ToString();
            textBoxCne.Text = row.Cells[3].Value.ToString();
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idactuelle);
            if (id != 0) { 
            StudentDetails sd = new StudentDetails(id);
            sd.Show();
            }
            else {
                MessageBox.Show("Veuillez Selectionner Un etudiant ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
