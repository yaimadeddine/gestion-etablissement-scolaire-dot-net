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
    public partial class Prof : Form
    {
        int currRowIndex;
        int s = 0;
        string idactuelle = "0";
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=dotnetdb";
        MySqlConnection maconnexion;
        public Prof()
        {
            InitializeComponent();
            fetchdata();
            dataGridView1.ColumnHeadersHeight = 24;
            fetchmatiere();
        }

        public void fetchdata()
        {
            try
            {
                DataTable dataTable = new DataTable();

                this.dataGridView1.DataSource = null;


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select e.id,e.nom,e.prenom,e.echelle,e.salaire,d.libelle as 'departement' from enseignant e, departement d where e.departement_id = d.id";
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

        public void fetchmatiere()
        {
            try
            {
                DataTable dataTable = new DataTable();

                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select libelle from departement";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        comboBoxDepartement.Items.Add(item.ToString());
                    }
                }
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {

            if (textBoxNom.Text == "" || textBoxPrenom.Text == "" || textBoxEchelle.Text == "")
            {
                MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                try
                {
                    string salaire = numericUpDownSalaire.Value.ToString();
                    string dep = comboBoxDepartement.Text;
                    string Query = "insert into enseignant(id,nom,prenom,echelle,salaire,departement_id) values(null,'" + textBoxNom.Text + "','" + textBoxPrenom.Text + "','" + textBoxEchelle.Text + "','"+salaire+ "',(select id from departement where libelle='"+dep+"'))";

                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Enseignant bien ajouter");
                    fetchdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                textBoxNom.Text = "";
                textBoxPrenom.Text = "";
                textBoxEchelle.Text = "";
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            idactuelle = row.Cells[0].Value.ToString();
            s = 1;
            textBoxNom.Text = row.Cells[1].Value.ToString();
            textBoxPrenom.Text = row.Cells[2].Value.ToString();
            textBoxEchelle.Text = row.Cells[3].Value.ToString();
            numericUpDownSalaire.Value = decimal.Parse( row.Cells[4].Value.ToString());
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
                if (textBoxNom.Text == "" || textBoxPrenom.Text == "" || textBoxEchelle.Text == "")
                {
                    MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        string Query = "update enseigant set nom= '" + textBoxNom.Text + "' ,prenom='" + textBoxPrenom.Text + "',echelle='" + textBoxEchelle.Text + "', salaire="+numericUpDownSalaire.Value.ToString()+" where id= " + idactuelle + "";
                        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                        MyConn2.Open();
                        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                        MySqlDataReader MyReader2;
                        MyReader2 = MyCommand2.ExecuteReader();
                        MessageBox.Show("Enseignant bien modifier", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fetchdata();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    textBoxNom.Text = "";
                    textBoxPrenom.Text = "";
                    textBoxEchelle.Text = "";
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
                    string Query = "delete from enseignant where id=" + idactuelle + "";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Enseignant supprimé");
                    fetchdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                textBoxNom.Text = "";
                textBoxPrenom.Text = "";
                textBoxEchelle.Text = "";
            }
        }

        private void buttonDepartement_Click(object sender, EventArgs e)
        {
            Departement departement = new Departement();
            departement.Show();
        }
    }
}
