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
    public partial class Materiel : Form
    {
        int currRowIndex;
        int s = 0;
        string idactuelle = "0";
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=dotnetdb";
        MySqlConnection maconnexion;
        public Materiel()
        {
            InitializeComponent();
            fetchcombobox();
            fetchclasse();
            fetchdata();
            guna2DataGridView1.ColumnHeadersHeight = 24;
        }

        private void Materiel_Load(object sender, EventArgs e)
        {

        }
        public void fetchcombobox()
        {
            guna2ComboBox1.Items.Add("Ordinateur");
            guna2ComboBox1.Items.Add("Chaise");
            guna2ComboBox1.Items.Add("Table");
        }
        public void fetchdata()
        {
            try
            {
                DataTable dataTable = new DataTable();

                this.guna2DataGridView1.DataSource = null;


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select id,type,quantite,prix,classe_id as 'Classe' from matriel";
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

        public void fetchclasse()
        {
            try
            {
                DataTable dataTable = new DataTable();

                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select id from classe";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        guna2ComboBox2.Items.Add(item.ToString());
                    }
                }
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            idactuelle = row.Cells[0].Value.ToString();
            s = 1;
            guna2ComboBox1.Text = row.Cells[1].Value.ToString();
            guna2NumericUpDown1.Value = decimal.Parse(row.Cells[2].Value.ToString());
            guna2NumericUpDown2.Value = decimal.Parse(row.Cells[3].Value.ToString());
            guna2ComboBox2.Text = row.Cells[4].Value.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
                try
                {
                    string quantite = guna2NumericUpDown1.Value.ToString();
                    string prix = guna2NumericUpDown2.Value.ToString();
                    string classe = guna2ComboBox2.Text;
                    string type = guna2ComboBox1.Text;
                    string Query = "insert into matriel(id,type,quantite,prix,classe_id) values(null,'" + type + "','" + quantite + "','" + prix + "','" + classe + "')";

                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("materiel bien ajouter");
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
                    string quantite = guna2NumericUpDown1.Value.ToString();
                    string prix = guna2NumericUpDown2.Value.ToString();
                    string classe = guna2ComboBox2.Text;
                    string type = guna2ComboBox1.Text;
                    string Query = "update matriel set type= '" + type + "' ,quantite='" + quantite + "',prix='" + prix + "', classe_id=" + classe + " where id= " + idactuelle + "";
                        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                        MyConn2.Open();
                        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                        MySqlDataReader MyReader2;
                        MyReader2 = MyCommand2.ExecuteReader();
                        MessageBox.Show("Materiel bien modifier", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    string Query = "delete from matriel where id=" + idactuelle + "";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Materiel supprimé");
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
