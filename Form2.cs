using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using MySql.Data.MySqlClient;
namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        string server = "server=localhost;database=tugasPL;userid=root;password=''";    

        public Form2()
        {
            InitializeComponent();
        }
        string Jenis_kelamin;
        string imglocation = "";
        SqlCommand cmd;
        private void Form2_Load(object sender, EventArgs e)
        {
            string connection = "server=localhost;database=tugasPL;userid=root;password=''";
            string query = "SELECT * FROM siswa";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.Add("Photo", Type.GetType("System.Byte[]"));
            foreach (DataRow row in dt.Rows)
            {
                row["Photo"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["Image"].ToString()));
            }
            dataGridView1.DataSource = dt;
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

      
            string connection = "server=localhost;database=tugasPL;userid=root;password=''";
            string query = "insert into siswa values('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" +this.textBox3.Text + "', '"+this.textBox4.Text+ "','" + this.textBox5.Text + "','" + this.dateTimePicker1.Value.ToString("yyyyMMdd")+"','"+this.Jenis_kelamin+"','"+this.textBox6.Text+"','"+Path.GetFileName(pictureBox1.ImageLocation)+"')";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("sucess Save");
            conn.Close();
            File.Copy(textBox8.Text, Application.StartupPath + @"\Image\" + Path.GetFileName(pictureBox1.ImageLocation));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg| All files(*.*)|*.*";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                imglocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imglocation;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Jenis_kelamin = "Pria";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Jenis_kelamin = "Wanita";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfd = new OpenFileDialog();
            openfd.Filter = "Image Files(*.jpg;*.jpeg;*.gif;*.png;) | *.jpg;*.jpeg*.gif;*.png";
            if (openfd.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = openfd.FileName;
                pictureBox1.Image = new Bitmap(openfd.FileName);
                pictureBox1.ImageLocation = openfd.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string connection = "server=localhost;database=tugasPL;userid=root;password=''";
            string query = "DELETE FROM siswa where NIS='" + this.textBox1.Text + "'";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("data telah dihapus");
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;user id=root;password=;database=tugaspl";
            string query = "SELECT * FROM siswa";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns.Add("PICTURE", Type.GetType("System.Byte[]"));
            foreach (DataRow row in dt.Rows)
            {
                row["PICTURE"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["Photo"].ToString()));
            }

            dataGridView1.DataSource = dt;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string connection = "server=localhost;database=tugasPL;userid=root;password=''";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            con.Open();
            da = new MySqlDataAdapter("SELECT * FROM siswa WHERE Nama LIKE'"+ this.textBox2.Text + "%'",con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost;database=tugasPL;userid=root;password=''";
            string query = "UPDATE siswa SET Nama='" + this.textBox2.Text + "',Kelas='" + this.textBox3.Text + "',Jurusan='" + this.textBox4.Text + "',GENDER='" + this.Jenis_kelamin + "',Alamat='" + this.textBox6.Text + "',Photo='" + Path.GetFileName(pictureBox1.ImageLocation) + "' WHERE NIS='" + this.textBox1.Text + "'";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Record has been updated successfully");
            conn.Close();
                File.Copy(textBox8.Text, Application.StartupPath + @"\Image\" + Path.GetFileName(pictureBox1.ImageLocation));
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {
            string connection = "server=localhost;database=tugasPL;userid=root;password=''";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlDataAdapter da;
            DataTable dt;
            conn.Open();
            da = new MySqlDataAdapter("SELECT * FROM siswa WHERE Nama LIKE'" + this.textBox2.Text + "%", conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
    }
    
}
