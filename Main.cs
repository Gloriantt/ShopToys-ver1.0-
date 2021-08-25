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
using System.Net.NetworkInformation;

namespace ShopToys_ver1._0_
{
    public partial class Main : Form
    {
        public int Table = -1;
        public bool Ping = false;
        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        private NpgsqlConnection conn = new NpgsqlConnection("Server=rogue.db.elephantsql.com;Port=5432;User Id=xxjquuqc;Password=9muAsldlU2ACYsrE95cEnJHrCduz6dBG;Database=xxjquuqc;");
        public Main()
        {
            InitializeComponent();
        }
        public void Output_table_Sellers()
        {
            try
            {
                conn.Open();
                string sql = ("SELECT * from Sellers;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
        public void Output_table_Products()
        {
            try
            {
                conn.Open();
                string sql = ("SELECT idproduct,nameofproduct,DescriptionProduct,Category,count from Products;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
        }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
        public void Output_table_Procurement()
        {
            try
            {
                conn.Open();
                string sql = ("SELECT procurement.idprocurement,Products.Nameofproduct,Procurement.DateOfProcurement,Procurement.Count,Procurement.Cost,Procurement.PercentageOfAllowance from Procurement inner join Products on Procurement.IdProduct=Products.IdProduct;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }

        public void Output_table_Sales()
        {
            try
            {
                conn.Open();
                string sql = ("select sales.idsales,products.nameofproduct,sellers.nameofseller,sales.count,tshops.adress,sales.dateofsales from sales inner join " +
                    "sellers on sellers.idseller=sales.idseller inner join products on products.idproduct=sales.idproduct " +
                    "inner join tshops on tshops.idshop=sales.idshop;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
        public void Output_table_ScheduleOfWorks()
        {
            try
            {
                conn.Open();
                string sql = ("select Scheduleofworks.id_scheduleofworks,Sellers.NameOfSeller,tshops.Adress,ScheduleOfWorks.DateOfScheduleOfWorks from ScheduleOfWorks inner join tshops on Tshops.IdShop=ScheduleOfWorks.IdShop inner join sellers on ScheduleOfWorks.IdSeller=Sellers.IdSeller;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
        public void Search()
        {
            try
            {
                conn.Open();
                string sql = ($"SELECT idproduct,nameofproduct,DescriptionProduct,Category from " +
                    $"Products where nameofproduct like '{textBox1.Text}%';");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                panel2.Visible = true;
                panel3.Visible = false;
                Table = 0;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TestPing();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void ПродуктыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Output_table_Products();
            Table = 0;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            редактироватьToolStripMenuItem.Enabled = true;
            удалитьЗаписьToolStripMenuItem.Enabled = true;
            panel2.Visible = true;
            panel3.Visible = false;
            comboBox1.Items.Clear();
            try
            {
                int a = 0, b = 0; ;
                string sql = " select category from products group by category;";
                string sql1 = "select count(category) from products;";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                conn.Open();
                NpgsqlDataReader d1 = s1.ExecuteReader();
                while (d1.Read())
                {
                    a = Convert.ToInt32(d1[0].ToString());
                }
                conn.Close();
                conn.Open();
                string[] mas = new string[a];
                NpgsqlDataReader d = s.ExecuteReader();
                while (d.Read())
                {
                    mas[b] = d[0].ToString();
                    b++;
                }
                conn.Close();
                for (int i = 0; i < mas.Length; i++)
                {
                    comboBox1.Items.Add(mas[i]);
                }
        }
            catch {  }
}

        private void ПродавцыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Output_table_Sellers();
            Table = 1;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            редактироватьToolStripMenuItem.Enabled = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void ПоставкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Output_table_Procurement();
            Table = 2;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            редактироватьToolStripMenuItem.Enabled = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void ПродажиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Output_table_Sales();
            Table = 3;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            редактироватьToolStripMenuItem.Enabled = true;
            panel3.Visible = true;
            panel2.Visible = false;
        }

        private void ГрафикРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Output_table_ScheduleOfWorks();
            Table = 4;
            добавитьЗаписьToolStripMenuItem.Enabled = true;
            редактироватьToolStripMenuItem.Enabled = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }
        public void onchec()
        {
            menuStrip1.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
        }
        public void TestPing()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                if (reply.Status == IPStatus.Success)
                {
                    Ping = true;
                }
            }
            catch
            {
                MessageBox.Show("Отсутствует интернет соединение!");
                Ping = false;
            }
            if (Ping == true)
            {
                menuStrip1.Enabled = true;
            }
            else { menuStrip1.Enabled = false; }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ТаблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ДобавитьЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Add f = new Add(this);
            if (Table == 0)
            {
                f.panel1.Visible = true;
                f.Show();
                
            }else if(Table==1)
            {
                f.panel2.Visible = true;
                f.Show();
            }else if(Table==2)
            {
                f.panel3.Visible = true;
                f.Show();
            }else if(Table==3)
            {
                f.panel4.Visible = true;
                f.Show();
            }else if(Table==4)
            {
                f.panel5.Visible = true;
                f.Show();
            }
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void Button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string sql = ($"select 'fdsfs',products.nameofproduct,sum(sales.count) as salescount from sales inner join products on sales.idproduct=products.idproduct group by products.nameofproduct order by koof limit 5;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                try
                {
                    conn.Open();
                    string sql = ($"select idproduct,nameofproduct,DescriptionProduct,category from products where category='{comboBox1.SelectedItem.ToString()}';");
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    conn.Close();
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка!");
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();
                string sql = ($"select sellers.idseller,sellers.nameofseller,sum(sales.count) as Count_of_sale,sum((((procurement.cost*procurement.percentageofallowance)/100)+procurement.cost)*sales.count) as Sum_of_Day from Sales inner join sellers on sales.idseller=sellers.idseller inner join products on products.idproduct=sales.idproduct inner join procurement on procurement.idproduct=products.idproduct where sales.dateofsales=date(now()) group by sellers.idseller ;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            string a = dateTimePicker1.Value.ToString();
            var A = a.Split('.', ' ');
            a = A[2] + '-' + A[1] + '-' + A[0];

            string b = dateTimePicker2.Value.ToString();
            var B = b.Split('.', ' ');
            b = B[2] + '-' + B[1] + '-' + B[0];


            try
            {
                conn.Open();
                string sql = ($"select sales.idsales,products.nameofproduct,sellers.nameofseller,sales.count,tshops.adress,sales.dateofsales from sales inner join " +
                       $"sellers on sellers.idseller=sales.idseller inner join products on products.idproduct=sales.idproduct " +
                       $"inner join tshops on tshops.idshop=sales.idshop where sales.dateofsales>'{a}' and '{b}'>sales.dateofsales;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();

            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {

            string a = dateTimePicker1.Value.ToString();
            var A = a.Split('.', ' ');
            a = A[2] + '-' + A[1] + '-' + "01";

            string b = dateTimePicker1.Value.ToString();
            var B = b.Split('.', ' ');
            b = B[2] + '-' + B[1] + '-' + "28";


            try
            {
                conn.Open();
                string sql = ($"select 'IDSELLERSS' AS SLLL,sellers.nameofseller,sum(sales.count) as Count_of_sale," +
                $"sum((((procurement.cost*procurement.percentageofallowance)/100)+procurement.cost)*sales.count) as Sum_of_Month" +
                $" from sales inner join sellers on sellers.idseller=sales.idseller inner join products" +
                $" on products.idproduct=sales.idproduct inner join procurement on products.idproduct=procurement.idproduct" +
                $" where sales.dateofsales>'{a}' and '{b}'>sales.dateofsales group by sellers.nameofseller;");
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                conn.Close();

            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }

        private void РедактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Changes f = new Changes();
            if (Table == 0)
            {
                f.Show();
                f.panel1.Visible = true;
                try
                {
                    int a = 0, b = 0; ;
                    string sql = $"select nameofproduct from products where idproduct={dataGridView1.CurrentRow.Cells[0].Value};";
                    string sql1 = $"select count(nameofproduct) from products where idproduct={dataGridView1.CurrentRow.Cells[0].Value};";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.textBox1.Text = mas[i].ToString();
                    }

                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = $"select descriptionproduct from products where idproduct={dataGridView1.CurrentRow.Cells[0].Value};";
                    string sql1 = $"select count(descriptionproduct) from products where idproduct={dataGridView1.CurrentRow.Cells[0].Value};";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.textBox2.Text = mas[i].ToString();
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = $"select category from products where idproduct={dataGridView1.CurrentRow.Cells[0].Value};";
                    string sql1 = $"select count(category) from products where idproduct={dataGridView1.CurrentRow.Cells[0].Value};";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.textBox3.Text = mas[i].ToString();
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                f.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            }
            else if (Table == 1)
            {
                try
                {
                    int a = 0, b = 0; ;
                    string sql = $"select nameofseller from sellers where idseller={dataGridView1.CurrentRow.Cells[0].Value};";
                    string sql1 = $"select count(nameofseller) from sellers where idseller={dataGridView1.CurrentRow.Cells[0].Value};";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.textBox4.Text = mas[i].ToString();
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = $"select phone from sellers where idseller={dataGridView1.CurrentRow.Cells[0].Value};";
                    string sql1 = $"select count(phone) from sellers where idseller={dataGridView1.CurrentRow.Cells[0].Value};";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.textBox5.Text = mas[i].ToString();
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                f.Show();
                f.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                f.panel2.Visible = true;
            }
            else if (Table == 2)
            {
                try
                {
                    int a = 0, b = 0; ;
                    string sql = " select nameofproduct from products;";
                    string sql1 = "select count(nameofproduct) from products;";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.comboBox1.Items.Add(mas[i]);
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = $"select cost from procurement where idprocurement={dataGridView1.CurrentRow.Cells[0].Value};";
                    string sql1 = $"select count(cost) from procurement where idprocurement={dataGridView1.CurrentRow.Cells[0].Value};";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.textBox6.Text = mas[i].ToString();
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = $"select percentageofallowance from procurement where idprocurement={dataGridView1.CurrentRow.Cells[0].Value};";
                    string sql1 = $"select count(percentageofallowance) from procurement where idprocurement={dataGridView1.CurrentRow.Cells[0].Value};";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.textBox7.Text = mas[i].ToString();
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                f.Show();
                f.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                f.panel3.Visible = true;
            }
            else if (Table == 3)
            {
                try
                {
                    int a = 0, b = 0; ;
                    string sql = " select nameofproduct from products ;";
                    string sql1 = "select count(nameofproduct) from products;";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.comboBox2.Items.Add(mas[i]);
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = " select nameofseller from sellers;";
                    string sql1 = "select count(nameofseller) from sellers;";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.comboBox3.Items.Add(mas[i]);
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = " select count from sales;";
                    string sql1 = "select count(count) from sales;";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.textBox8.Text = mas[i];
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = " select adress from tshops;";
                    string sql1 = "select count(adress) from tshops;";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.comboBox4.Items.Add(mas[i]);
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                f.Show();
                f.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                f.panel4.Visible = true;
            }else if(Table==4)
            {
                try
                {
                    int a = 0, b = 0; ;
                    string sql = " select adress from tshops;";
                    string sql1 = "select count(adress) from tshops;";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.comboBox7.Items.Add(mas[i]);
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                try
                {
                    int a = 0, b = 0; ;
                    string sql = " select nameofseller from sellers;";
                    string sql1 = "select count(nameofseller) from sellers;";
                    NpgsqlCommand s = new NpgsqlCommand(sql, conn);
                    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn);
                    conn.Open();
                    NpgsqlDataReader d1 = s1.ExecuteReader();
                    while (d1.Read())
                    {
                        a = Convert.ToInt32(d1[0].ToString());
                    }
                    conn.Close();
                    conn.Open();
                    string[] mas = new string[a];
                    NpgsqlDataReader d = s.ExecuteReader();
                    while (d.Read())
                    {
                        mas[b] = d[0].ToString();
                        b++;
                    }
                    conn.Close();
                    for (int i = 0; i < mas.Length; i++)
                    {
                        f.comboBox6.Items.Add(mas[i]);
                    }
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
                f.Show();
                f.id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                f.panel5.Visible = true;
            }

        }

        private void УдалитьЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Changes f = new Changes();
            if (Table == 0)
            {
                try
                {
                    conn.Open();
                    string sql = ($"delete from products where idproduct={dataGridView1.CurrentRow.Cells[0].Value};");
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    Output_table_Products();
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка!");
                }
            }else if (Table==1)
            {
                try
                {
                    conn.Open();
                    string sql = ($"delete from sellers where idseller={dataGridView1.CurrentRow.Cells[0].Value};");
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    Output_table_Sellers();
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка!");
                }
            }else if(Table==2)
            {
                try
                {
                    conn.Open();
                    string sql = ($"delete from procurement where idprocurement={dataGridView1.CurrentRow.Cells[0].Value};");
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    Output_table_Procurement();
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка!");
                }
            }else if(Table==3)
            {
                try
                {
                    conn.Open();
                    string sql = ($"delete from sales where idsales={dataGridView1.CurrentRow.Cells[0].Value};");
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    Output_table_Sales();
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка!");
                }
            }else if(Table==4)
            {
                try
                {
                    conn.Open();
                    string sql = ($"delete from scheduleofworks where id_scheduleofworks={dataGridView1.CurrentRow.Cells[0].Value};");
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    Output_table_ScheduleOfWorks();
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка!");
                }
            }
        }
    }
}
