using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadBook
{
    public partial class Login : Form
    {
        bool passCheck = false;
        string query1,id,pass;
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.ShowDialog(); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(passCheck)
            {
                textBox2.PasswordChar = '\0';
                passCheck = false;
            }
            else
            {
                textBox2.PasswordChar = '*';
                passCheck = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox1.Text==""||textBox2.Text == "")
            {
                MessageBox.Show("Enter Id/Password");
            }
            else
            {
                DataAccess dataAccess = new DataAccess();
                id = textBox1.Text.ToString();
                pass = textBox2.Text.ToString();
                query1 = "Select * From UserLogin where UserId = '" + id + "' AND Password = '" + pass + "' ";

                SqlDataReader reader = dataAccess.GetData(query1);

                if (reader.Read())
                {
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Show();
                }
                else
                {
                    MessageBox.Show("No Id Found/Wrong Password");
                }
            }   
        }
    }
}
