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
using Microsoft.Office.Interop.Word;

namespace ShopToys_ver1._0_
{
    public partial class Add : Form
    {
        private NpgsqlConnection conn1 = new NpgsqlConnection("Server=rogue.db.elephantsql.com;Port=5432;User Id=xxjquuqc;Password=9muAsldlU2ACYsrE95cEnJHrCduz6dBG;Database=xxjquuqc;");
        private Main m;
        public System.Data.DataTable dt = new System.Data.DataTable();
        public DataSet ds = new DataSet();
        public Add()
        {
            InitializeComponent();
        }

        public Add(Main main)
        {
            InitializeComponent();
            this.m = main;
        }

       
        private void Button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox12.Text!=""))
            {
                string name = textBox1.Text;
                string description = textBox2.Text;
                string category = textBox3.Text;
                string count = textBox12.Text;
                try
                {
                    conn1.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"call OnAddproduct('{name}','{description}','{category}',{count});", conn1);
                    command.ExecuteNonQuery();
                    conn1.Close();
                    m.Output_table_Products();
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
            }
            else { MessageBox.Show("Пустые поля!"); }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if ((textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != "") && (textBox7.Text != ""))
            {
                string name = textBox4.Text;
                string phone = textBox5.Text;
                string login = textBox6.Text;
                string password = textBox7.Text;
                try
                {
                    conn1.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"call OnAddSeller('{name}','{phone}','{login}','{password}');", conn1);
                    command.ExecuteNonQuery();
                    conn1.Close();
                    m.Output_table_Sellers();
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
            }
            else { MessageBox.Show("Пустые поля!"); }
        }

        private void Add_Load(object sender, EventArgs e)
        {
            try
            {
                int a = 0, b = 0; ;
                string sql = " select nameofseller from sellers;";
                string sql1 = "select count(nameofseller) from sellers;";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn1);
                conn1.Open();
                NpgsqlDataReader d1 = s1.ExecuteReader();
                while (d1.Read())
                {
                    a = Convert.ToInt32(d1[0].ToString());
                }
                conn1.Close();
                conn1.Open();
                string[] mas = new string[a];
                NpgsqlDataReader d = s.ExecuteReader();
                while (d.Read())
                {
                    mas[b] = d[0].ToString();
                    b++;
                }
                conn1.Close();
                for (int i = 0; i < mas.Length; i++)
                {
                    comboBox5.Items.Add(mas[i]);
                    comboBox3.Items.Add(mas[i]);
                }
            }
            catch { MessageBox.Show("Что-то пошло не так"); }
            try
            {
                int a = 0, b = 0; ;
                string sql = " select nameofproduct from products;";
                string sql1 = "select count(nameofproduct) from products;";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn1);
                conn1.Open();
                NpgsqlDataReader d1 = s1.ExecuteReader();
                while (d1.Read())
                {
                    a = Convert.ToInt32(d1[0].ToString());
                }
                conn1.Close();
                conn1.Open();
                string[] mas = new string[a];
                NpgsqlDataReader d = s.ExecuteReader();
                while (d.Read())
                {
                    mas[b] = d[0].ToString();
                    b++;
                }
                conn1.Close();
                for (int i = 0; i < mas.Length; i++)
                {
                    comboBox1.Items.Add(mas[i]);
                    comboBox2.Items.Add(mas[i]);
                }
            }
            catch { MessageBox.Show("Что-то пошло не так"); }

            //try
            //{
            //    int a = 0, b = 0; ;
            //    string sql = " select idprocurement from procurement;";
            //    string sql1 = "select count(idprocurement) from procurement;";
            //    NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
            //    NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn1);
            //    conn1.Open();
            //    NpgsqlDataReader d1 = s1.ExecuteReader();
            //    while (d1.Read())
            //    {
            //        a = Convert.ToInt32(d1[0].ToString());
            //    }
            //    conn1.Close();
            //    conn1.Open();
            //    string[] mas = new string[a];
            //    NpgsqlDataReader d = s.ExecuteReader();
            //    while (d.Read())
            //    {
            //        mas[b] = d[0].ToString();
            //        b++;
            //    }
            //    conn1.Close();
            //    for (int i = 0; i < mas.Length; i++)
            //    {

            //    }
            //}
            //catch { MessageBox.Show("Что-то пошло не так"); }

            try
            {
                int a = 0, b = 0; ;
                string sql = " select adress from tshops;";
                string sql1 = "select count(adress) from tshops;";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlCommand s1 = new NpgsqlCommand(sql1, conn1);
                conn1.Open();
                NpgsqlDataReader d1 = s1.ExecuteReader();
                while (d1.Read())
                {
                    a = Convert.ToInt32(d1[0].ToString());
                }
                conn1.Close();
                conn1.Open();
                string[] mas = new string[a];
                NpgsqlDataReader d = s.ExecuteReader();
                while (d.Read())
                {
                    mas[b] = d[0].ToString();
                    b++;
                }
                conn1.Close();
                for (int i = 0; i < mas.Length; i++)
                {
                    comboBox6.Items.Add(mas[i]);
                    comboBox4.Items.Add(mas[i]);
                }
            }
            catch { MessageBox.Show("Что-то пошло не так"); }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int a1=0;
            try
            {
                conn1.Open();
                string sql = $"select idproduct from products where nameofproduct='{comboBox1.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a1 = Convert.ToInt32(dr[0].ToString());
                }
                conn1.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
           
            if ((textBox8.Text != "") && (textBox9.Text != "") && (textBox10.Text != "")&&(comboBox1.Text!=""))
            {
                string a = dateTimePicker1.Value.ToString();
                var A = a.Split('.', ' ');
                a = A[2] + '-' + A[1] + '-' + A[0];
                string numberofproduct = a1.ToString();
                string dateofprocurement = a;
                string count = textBox8.Text;
                string cost = textBox9.Text;
                string percentageofallowance = textBox10.Text;
                try
                {
                    conn1.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"call OnAddProcurement({numberofproduct},'{dateofprocurement}',{count},{cost},{percentageofallowance});", conn1);
                    command.ExecuteNonQuery();
                    conn1.Close();
                    m.Output_table_Procurement();
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
            }
            else { MessageBox.Show("Пустые поля!"); }
        }
        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    }
                }

                Document oDoc = new Document();
                oDoc.Application.Visible = true;

                oDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //table format 
                oRange.Text = oTemp;

                object Separator = WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                Type.Missing, Type.Missing, ref ApplyBorders,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Calibri light";

                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                oDoc.SaveAs2(filename);
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            int a1 = 0, a2 = 0, a3 = 0;
            try
            {
                conn1.Open();
                string sql = $"select idseller from sellers where  nameofseller='{comboBox3.SelectedItem.ToString()}'; ";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a2 = Convert.ToInt32(dr[0].ToString());
                }
                conn1.Close();
                MessageBox.Show(a2.ToString());
            }
            catch { MessageBox.Show("Возникла ошибка!"); }
            try
            {
                conn1.Open();
                string sql = $"select idshop from tshops where adress='{comboBox6.SelectedItem.ToString()}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a1 = Convert.ToInt32(dr[0].ToString());
                } 
                conn1.Close();
                MessageBox.Show(a1.ToString());
            }
            catch { MessageBox.Show("Возникла ошибка!"); }
            try
            {
                conn1.Open();
                string sql = $"select idproduct from products where nameofproduct='{comboBox2.SelectedItem.ToString()}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a3 = Convert.ToInt32(dr[0].ToString());
                }
                conn1.Close();
            }
            catch { MessageBox.Show("Возникла ошибка!"); }


            if (a1 != 0 && a2 != 0)
            {
                if ((comboBox2.Text != "") && (textBox11.Text != "")&& (comboBox3.Text  != "")&&(comboBox6.Text!=""))
                {
                    string a = dateTimePicker1.Value.ToString();
                    var A = a.Split('.', ' ');
                    a = A[2] + '-' + A[1] + '-' + A[0];
                    string numberofshop = a1.ToString();
                    string numberofproduct = a3.ToString(); 
                    string dateofsale = a; 
                    string count = textBox11.Text;
                    string numberofseller = a2.ToString();
                    try
                    {
                        conn1.Close();
                        conn1.Open();
                        NpgsqlCommand command = new NpgsqlCommand($"call OnAddSale({numberofproduct},{numberofseller},{count},{numberofshop},'{dateofsale}');", conn1);
                        command.ExecuteNonQuery();
                        conn1.Close();
                        try
                        {
                            conn1.Open();
                            string sql = ("select products.nameofproduct,sellers.nameofseller,sales.count,tshops.adress,sales.dateofsales from sales inner join " +
                                "sellers on sellers.idseller=sales.idseller inner join products on products.idproduct=sales.idproduct " +
                                $"inner join tshops on tshops.idshop=sales.idshop where sales.idproduct={numberofproduct} and sales.idseller={numberofseller} and sales.count={count} and sales.dateofsales='{dateofsale}';");
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn1);
                            ds.Reset();
                            da.Fill(ds);
                            dt = ds.Tables[0];
                            dataGridView1.DataSource = dt;
                            conn1.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Возникла ошибка!");
                        }
                        SaveFileDialog sfd = new SaveFileDialog();

                        sfd.Filter = "Word Documents (*.docx)|*.docx";

                        sfd.FileName = "export.docx";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            Export_Data_To_Word(dataGridView1, sfd.FileName);
                        }
                        m.Output_table_Sales();
                }
                    catch { MessageBox.Show("Что-то пошло не так"); }
            }
                else { MessageBox.Show("Пустые поля!"); }
            }
            else { MessageBox.Show("В эту дату никто не работает!"); }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            int a1 = 0,a2=0;
            try
            {
                conn1.Open();
                string sql = $"select idshop from tshops where adress='{comboBox4.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a1 = Convert.ToInt32(dr[0].ToString());
                }
                conn1.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
            try
            {
                conn1.Open();
                string sql = $"select idseller from sellers where nameofseller='{comboBox5.SelectedItem}';";
                NpgsqlCommand s = new NpgsqlCommand(sql, conn1);
                NpgsqlDataReader dr = s.ExecuteReader();
                while (dr.Read())
                {
                    a2 = Convert.ToInt32(dr[0].ToString());
                }
                conn1.Close();
            }
            catch { MessageBox.Show("Возникла ошибка"); }
            if ((comboBox4.Text != "")&& (comboBox5.Text != ""))
            {
                string a = dateTimePicker3.Value.ToString();
                var A = a.Split('.', ' ');
                a = A[2] + '-' + A[1] + '-' + A[0];
                string numberofshop = a1.ToString();
                string dateofwork = a;
                string numberofseller = a2.ToString();
                try
                {
                    conn1.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"call onAddScheduleofWork('{dateofwork}',{numberofshop},{numberofseller});", conn1);
                    command.ExecuteNonQuery();
                    conn1.Close();
                    m.Output_table_ScheduleOfWorks();
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
            }
            else { MessageBox.Show("Пустые поля!"); }
        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (e.KeyChar <= 47 || e.KeyChar >= 58)
            {
                e.Handled = true;
            }
        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (e.KeyChar <= 47 || e.KeyChar >= 58)
            {
                e.Handled = true;
            }
        }

        private void TextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (e.KeyChar <= 47 || e.KeyChar >= 58)
            {
                e.Handled = true;
            }
        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (e.KeyChar <= 47 || e.KeyChar >= 58)
            {
                e.Handled = true;
            }
        }

        private void TextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (e.KeyChar <= 47 || e.KeyChar >= 58)
            {
                e.Handled = true;
            }
        }
    }
}
