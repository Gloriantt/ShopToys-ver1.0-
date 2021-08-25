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
    public partial class Sign : Form
    {
        private NpgsqlConnection conn = new NpgsqlConnection("Server=rogue.db.elephantsql.com;Port=5432;User Id=xxjquuqc;Password=9muAsldlU2ACYsrE95cEnJHrCduz6dBG;Database=xxjquuqc;");
        public Sign()
        {
            InitializeComponent();
        }

        private void Sign_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            Main f = new Main();
            try
            {
                conn.Open();
                string sql = $"select nameofseller from sellers where login='{textBox1.Text}'and password_seller='{textBox2.Text}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    label3.Text = dr[0].ToString();
                }
                conn.Close();
                if (label3.Text != "")
                {
                    MessageBox.Show($"Добро пожаловать {label3.Text}");
                    if(label3.Text=="Admin")
                    {
                        f.продавцыToolStripMenuItem.Enabled = true;
                    }
                    f.onchec();
                    f.panel1.Visible = true;
                    
                    Sign.ActiveForm.Visible = false;
                }
                else { MessageBox.Show("Ошибка введенных данных"); }
            }
            catch { MessageBox.Show("Возникла ошибка!"); }
        }
    }
}
