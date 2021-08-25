using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace ShopToys_ver1._0_
{
    public partial class Changes : Form
    {
        private Main m;
        public int id = 0;
        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        private NpgsqlConnection conn = new NpgsqlConnection("Server=rogue.db.elephantsql.com;Port=5432;User Id=xxjquuqc;Password=9muAsldlU2ACYsrE95cEnJHrCduz6dBG;Database=xxjquuqc;");
        public Changes()
        {
            InitializeComponent();
        }

        public Changes(Main main)
        {
            InitializeComponent();
            this.m = main;
        }
       
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand($"update Products set nameofproduct='{textBox1.Text}', descriptionproduct='{textBox2.Text}', category='{textBox3.Text}' where idproduct={id}", conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch { MessageBox.Show("Что-то пошло не так"); }
        }

        private void Changes_Load(object sender, EventArgs e)
        {
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand($"update sellers set nameofseller='{textBox4.Text}', phone='{textBox5.Text}' where idseller={id}", conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch { MessageBox.Show("Что-то пошло не так"); }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            int a1 = 0;
            try
            {
                conn.Open();
                string sql = $"select idproduct from products where nameofproduct='{comboBox1.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a1 = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
            try
            {
                string numberofproduct = a1.ToString();
                string cost = textBox6.Text;
                string percentofallowance = textBox7.Text;
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand($"update procurement set idproduct={numberofproduct}, cost={cost}, percentageofallowance={percentofallowance} where idprocurement={id}", conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch { MessageBox.Show("Что-то пошло не так"); }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            int a1 = 0,a2=0,a3=0;
            try
            {
                conn.Open();
                string sql = $"select idproduct from products where nameofproduct='{comboBox2.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a1 = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
            
            try
            {
                conn.Open();
                string sql = $"select idseller from sellers where nameofseller='{comboBox3.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a2 = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
            try
            {
                conn.Open();
                string sql = $"select idshop from tshops where adress='{comboBox4.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a3 = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
            try
            {
                string numberofproduct = a1.ToString();
                string numberofseller = a2.ToString();
                string count = textBox8.Text;
                string numbershop = a3.ToString();
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand($"update sales set idproduct={numberofproduct}, idseller={numberofseller}, count={count},idshop={numbershop} where idsales={id}", conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch { MessageBox.Show("Что-то пошло не так"); }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            int a1 = 0, a2 = 0;
            try
            {
                conn.Open();
                string sql = $"select idseller from sellers where nameofseller='{comboBox6.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a1 = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
            try
            {
                conn.Open();
                string sql = $"select idshop from tshops where adress='{comboBox7.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a2 = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
            try
            {
                MessageBox.Show(a1.ToString());
                MessageBox.Show(a2.ToString());
                string a = dateTimePicker1.Value.ToString();
                var A = a.Split('.', ' ');
                a = A[2] + '-' + A[1] + '-' + A[0];
                string numberofshop = a2.ToString();
                string numberofseller = a1.ToString();
                string date = a;
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand($"update scheduleofworks set dateofscheduleofworks='{date}', idshop={numberofshop}, idseller={numberofseller} where id_scheduleofworks={id}", conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch { MessageBox.Show("Что-то пошло не так"); }
        }
    }
}
