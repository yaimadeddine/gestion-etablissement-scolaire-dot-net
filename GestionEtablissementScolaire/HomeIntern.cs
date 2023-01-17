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
    public partial class HomeIntern : Form
    {
        int currRowIndex;
        int s = 0;
        int id = 0;
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=dotnetdb";
        MySqlConnection maconnexion;
        public HomeIntern()
        {
            InitializeComponent();
            fetchdata();
        }

        public void fetchdata()
        {
            try
            {
                //DataTable dataTable = new DataTable();

                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select count(id) from etudiant";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataReader mr = cmd.ExecuteReader();
                while (mr.Read())
                {
                    labeletud.Text = mr.GetString(0);
                }
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                //DataTable dataTable = new DataTable();

                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select count(id) from enseignant";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataReader mr = cmd.ExecuteReader();
                while (mr.Read())
                {
                    labelenseig.Text = mr.GetString(0);
                }
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
