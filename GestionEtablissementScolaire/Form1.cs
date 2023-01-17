using MySql.Data.MySqlClient;

namespace GestionEtablissementScolaire
{
    public partial class Form1 : Form
    {
        int currRowIndex;
        int s = 0;
        int id = 0;
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=dotnetdb";
        MySqlConnection maconnexion;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select * from admin where login= '" + textBox1.Text + "' and password='"+textBox2.Text+"'";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataReader mr = cmd.ExecuteReader();
                if (mr.Read())
                {
                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }
                else MessageBox.Show("login ou mot de passe incorrecte", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Iscription ins = new Iscription();
            ins.Show();
        }
    }
}