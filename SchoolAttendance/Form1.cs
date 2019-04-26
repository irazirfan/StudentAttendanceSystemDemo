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
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace SchoolAttendance
{
    
    public partial class Form1 : Form
    {
        private bool mouseDown;
        private Point lastLocation;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = null;
            if (txtUserName.Text != null && txtPassword.Text != null)
            {
                string conString = "User Id=hr;Password=hr;" +
                    "Data Source= localhost:1521; Pooling=false";
                
                OracleConnection con = new OracleConnection();
                con.ConnectionString = conString;
                con.Open();

                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "select password from users where username='"+txtUserName.Text+"' ";

                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader.GetString(0);
                }

                if (txtPassword.Text == password)
                {
                    Form2 f2 = new Form2(txtUserName.Text);
                    f2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!");
                }
                

            }
            else
            {
                MessageBox.Show("type username and password properly");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '#';
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }


        
}
