using Guna.UI2.WinForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionEtablissementScolaire
{
    public partial class StudentDetails : Form
    {
        int currRowIndex;
        int s = 0;
        int id = 0;
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=dotnetdb";
        MySqlConnection maconnexion;
        public StudentDetails(int id)
        {
            this.id = id;
            InitializeComponent();
            fetchdata(id);
            fetchdatastudent(id);
            dataGridView1.ColumnHeadersHeight = 24;
            fetchmatiere();
            calcultotal();
        }

        public void fetchdata(int id) {
                try
                {
                    //DataTable dataTable = new DataTable();

                    maconnexion = new MySqlConnection(MyConnection2);
                    maconnexion.Open();
                    string request = "select nom,prenom,cne from etudiant where id= '"+id+"'";
                    MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                    MySqlDataReader mr = cmd.ExecuteReader();
                    while (mr.Read())
                    {
                    //Console.WriteLine("{0} {1} {2}", mr.GetInt32(0), mr.GetString(1),
                    //        mr.GetString(2));
                    label2.Text = mr.GetString("nom") + " " + mr.GetString("prenom");
                    label4.Text = mr.GetString("cne");
                }
                    maconnexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }

        public void fetchdatastudent(int id)
        {
            try
            {
                DataTable dataTable = new DataTable();

                this.dataGridView1.DataSource = null;


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select m.libelle as 'Matiere',n.valeur as 'Note' from note n,matiere m where n.etudiant_id='" + id+"' and n.matiere_id=m.id ";
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
                string request = "select libelle from matiere";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                foreach(DataRow row in dataTable.Rows)
                {
                    foreach(var item in row.ItemArray)
                    {
                        comboBox1.Items.Add(item.ToString());
                    }
                }
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void calcultotal()
        {
            int i = 0;
            int note = 0;
            try
            {

                
                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select valeur from note where etudiant_id= '" + id + "'";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataReader mr = cmd.ExecuteReader();
                while (mr.Read())
                {
                    note += mr.GetInt32(0);
                    i++;
                }
                
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if(i == 0)
            {
                float total = 0;
                labelnote.Text = " ";
            }
            else
            {
                float total = note / i;
                labelnote.Text = total.ToString();
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            
            try
                {
                string matiere = comboBox1.Text;
                string value = numericUpDown1.Value.ToString();
                string Query = "insert into note(id,etudiant_id,matiere_id,valeur) values(null,'"+id+"',(select id from matiere where libelle='"+matiere+"'),'"+value+"')";

                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                //MessageBox.Show("note bien ajouter");
                fetchdatastudent(id);
                calcultotal();
                maconnexion.Close();
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }            
        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\yoaim\\OneDrive\\Bureau\\bulletin.pdf",FileMode.Create));

            doc.Open();
            iTextSharp.text.Font titrefont = FontFactory.GetFont("Calibri", 24, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(40, 40, 43));
            Paragraph titre = new Paragraph("Bulletin",titrefont);
            titre.Alignment = Element.ALIGN_CENTER;
            doc.Add(titre);

            iTextSharp.text.Font fontStitre = FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(40, 40, 43));
            try
            {
                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select nom,prenom,cne from etudiant where id= '" + id + "'";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataReader mr = cmd.ExecuteReader();
                while (mr.Read())
                {
                    doc.Add(new Paragraph("Etudiant :   "+mr.GetString(0)+" "+mr.GetString(1),fontStitre));
                    doc.Add(new Paragraph("CNE :" + mr.GetString(2),fontStitre));
                }
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            doc.Add(new Paragraph("\n"));
            doc.Add(new Paragraph("\n"));
            doc.Add(new Paragraph("\n"));
            doc.Add(new Paragraph("\n"));
            iTextSharp.text.Font font1 = FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(40, 40, 43));
            doc.Add(new Paragraph("Les notes de l'etudiant :\n",font1));
            iTextSharp.text.Font font2 = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(40, 40, 43));
            try
            {
              

                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select m.libelle as 'Matiere',n.valeur as 'Note' from note n,matiere m where n.etudiant_id='" + id + "' and n.matiere_id=m.id ";
                MySqlCommand cmd = new MySqlCommand(request, maconnexion);
                MySqlDataReader mr = cmd.ExecuteReader();
                while (mr.Read())
                {
                    doc.Add(new Paragraph("                      "+mr.GetString(0)+" : "+mr.GetString(1)+"\n",font2));
                }
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            doc.Add(new Paragraph("La moyenne est : ", font2));
            iTextSharp.text.Font font3 = FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD, new iTextSharp.text.BaseColor(255, 0, 0));
            doc.Add(new Paragraph(labelnote.Text,font3));
             doc.Close();

        }
    }
}
