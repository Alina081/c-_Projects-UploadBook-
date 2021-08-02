using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UploadBook
{
    public partial class Form1 : Form
    {
        int i = 0;
        string imagePath,bookName,bookLink;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            imagePath = openFileDialog1.FileName.ToString();
            pictureBox1.Image = Image.FromFile(imagePath);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int n = 0;
            DataAccess dataAccess = new DataAccess();
            string bookName_,imagePath_,query3;
            query3 = "Select BookName,BookImage from BookList";
            SqlDataReader sqlDataReader = dataAccess.GetData(query3);
            while(sqlDataReader.Read())
            {
                bookName_ = sqlDataReader["BookName"].ToString();
                imagePath_ = sqlDataReader["BookImage"].ToString();

                imageList1.Images.Add(Image.FromFile(imagePath_));
                this.imageList1.ImageSize = new Size(250, 250);
                this.listView1.LargeImageList = this.imageList1;
                ListViewItem item = new ListViewItem();
                item.ImageIndex = n;
                item.Text = bookName_;
                this.listView1.Items.Add(item);
                n++;
            }
            if(n==0)
            {
                i = 0;
            }
            else
            {
                i = n;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            DataAccess dataAccess = new DataAccess();
            string selected, query2,linkToD;
            selected = listView1.SelectedItems[0].SubItems[0].Text.ToString();
            query2 = "Select BookLink from BookList where BookName = '"+selected+"'";
            SqlDataReader sqlDataReader = dataAccess.GetData(query2);
            if (sqlDataReader.Read())
            {
                linkToD = sqlDataReader["BookLink"].ToString();

                MessageBox.Show("Press OK to Download");

                try
                {
                    System.Diagnostics.Process.Start(linkToD);
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }

            }
            else
            {
                MessageBox.Show("Something Happen Wrong");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || openFileDialog1.FileName == "")
            {
                MessageBox.Show("Enter Valid Data");
            }
            else
            {
                pictureBox1.Image = null;
                textBox1.Text = ""; textBox2.Text = "";

                if (imagePath == string.Empty)
                {
                    MessageBox.Show("Choose Book Image");
                }
                else if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Enter Book Download Link");
                }
                else if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Enter Book Name");
                }
                else
                {
                    DataAccess dataAccess = new DataAccess();
                    string query1;
                    bookName = textBox2.Text.ToString();
                    bookLink = textBox1.Text.ToString();
                    query1 = "INSERT INTO BookList(BookName,BookImage,BookLink)VALUES('" + bookName + "','" + imagePath + "','" + bookLink + "')";
                    int result = dataAccess.UpdateDB(query1);

                    if (result > 0)
                    {
                        MessageBox.Show("'" + result + "' Book Inserted");
                    }
                    else
                    {
                        MessageBox.Show("Something Happen Wrong");
                    }

                    imageList1.Images.Add(Image.FromFile(imagePath));
                    this.imageList1.ImageSize = new Size(250, 250);
                    this.listView1.LargeImageList = this.imageList1;
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = i;
                    item.Text = bookName;
                    this.listView1.Items.Add(item);
                    i++;
                }
            }
        }
    }
}
