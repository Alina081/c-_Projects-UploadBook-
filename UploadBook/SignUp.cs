using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadBook
{
    public partial class SignUp : Form
    {
        private string id, pass,query1;
        bool passCheck = false;
        public SignUp()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (passCheck)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (passCheck)
            {
                textBox3.PasswordChar = '\0';
                passCheck = false;
            }
            else
            {
                textBox3.PasswordChar = '*';
                passCheck = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == ""||textBox3.Text == "")
            {
                MessageBox.Show("Enter Id/Password");
            }
            else
            {
                id = textBox1.Text.ToString();
                DataAccess dataAccess = new DataAccess();
                if (textBox2.Text == textBox3.Text)
                {
                    pass = textBox2.Text.ToString();
                    query1 = "Insert into UserLogin(UserId,Password)Values('" + id + "','" + pass + "')";
                    int result = dataAccess.UpdateDB(query1);
                    if (result > 0)
                    {
                        MessageBox.Show("User Created");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Something Happend Wrong");
                    }

                }
                else
                {
                    MessageBox.Show("Passwod Did Not Match");
                }
            }   
        }
    }
}
