﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace jobform
{
    
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void Swap(List<ManagerSales> list, int index1, int index2)
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
        private void QuickSort(List<ManagerSales> list, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(list, low, high);

                QuickSort(list, low, pi - 1);
                QuickSort(list, pi + 1, high);
            }
        }

        private int Partition(List<ManagerSales> list, int low, int high)
        {
            double pivot = list[high].TotalSales;
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (list[j].TotalSales > pivot)
                {
                    i++;
                    Swap(list, i, j);
                }
            }

            Swap(list, i + 1, high);
            return i + 1;
        }

        private void QuickSortWrapper(List<ManagerSales> list)
        {
            QuickSort(list, 0, list.Count - 1);
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            if(nametxt.Text == "" || pricetxt.Text == "" || nameclitxt.Text == "" || autotxt.Text == "")
            {
                MessageBox.Show("Заповніть всі поля", "Помилка.");
            }
            else
            {
                int n = dgv.Rows.Add();
                dgv.Rows[n].Cells[0].Value = nametxt.Text; 
                dgv.Rows[n].Cells[1].Value = nameclitxt.Text;
                dgv.Rows[n].Cells[2].Value = pricetxt.Text;
                dgv.Rows[n].Cells[3].Value = autotxt.Text;
                dgv.Rows[n].Cells[4].Value = numrewtxt.Text;

                listBox1.Items.Add(nametxt.Text);
                listBox2.Items.Add(pricetxt.Text);
            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count > 0)
            {
                int n = dgv.SelectedRows[0].Index;
                dgv.Rows[n].Cells[0].Value = nametxt.Text;
                dgv.Rows[n].Cells[1].Value = nameclitxt.Text;
                dgv.Rows[n].Cells[2].Value = pricetxt.Text;
                dgv.Rows[n].Cells[3].Value = autotxt.Text;
                dgv.Rows[n].Cells[4].Value = numrewtxt.Text;

                listBox1.Items[n] = nametxt.Text;
                listBox2.Items[n] = pricetxt.Text;
            }
            else
            {
                MessageBox.Show("Виберіть строчку для редагування.", "Помилка.");
            }
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count > 0)
            {
                int n = dgv.SelectedRows[0].Index; //////
                dgv.Rows.RemoveAt(dgv.SelectedRows[0].Index);

                listBox1.Items.RemoveAt(n);  
                listBox2.Items.RemoveAt(n);
            }
            else
            {
                MessageBox.Show("Виберіть строчку для видалення", "Помилка.");
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            nametxt.Text = dgv.SelectedRows[0].Cells[0].Value.ToString();
            nameclitxt.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
            pricetxt.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
            int n = Convert.ToInt32(dgv.SelectedRows[0].Cells[3].Value);
            numrewtxt.Value = n;
        }

        private void saveXmlbtn_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Managers";
                dt.Columns.Add("Name");
                dt.Columns.Add("NameCli");
                dt.Columns.Add("Price");
                dt.Columns.Add("Auto");
                dt.Columns.Add("Reward");
                ds.Tables.Add(dt);
                foreach(DataGridViewRow r in dgv.Rows)
                {
                    DataRow row = ds.Tables["Managers"].NewRow();
                    row["Name"] = r.Cells[0].Value;
                    row["NameCli"] = r.Cells[1].Value;
                    row["Price"] = r.Cells[2].Value;
                    row["Auto"] = r.Cells[3].Value;
                    row["Reward"] = r.Cells[4].Value;
                    ds.Tables["Managers"].Rows.Add(row);
                }
                ds.WriteXml("D:\\Data.xml");
                MessageBox.Show("XML файл збережено", "Виконано.");
            }
            catch
            {
                MessageBox.Show("Неможливо зберегти XML файл.", "Помилка.");
            }
        }

        private void loadXmlbtn_Click(object sender, EventArgs e)
        {
            if(dgv.Rows.Count > 1)
            {
                MessageBox.Show("Очистіть поле перед завантаженням нового файлу.", "Помилка.");
            }
            else
            {
                if (File.Exists("D:\\Data.xml"))
                {
                    DataSet ds =new DataSet();
                    ds.ReadXml("D:\\Data.xml");
                    foreach(DataRow item in ds.Tables["Managers"].Rows) 
                    {
                        int n = dgv.Rows.Add();
                        dgv.Rows[n].Cells[0].Value = item["Name"];
                        dgv.Rows[n].Cells[1].Value = item["NameCli"];
                        dgv.Rows[n].Cells[2].Value = item["Price"];
                        dgv.Rows[n].Cells[3].Value = item["Auto"];
                        dgv.Rows[n].Cells[4].Value = item["Reward"];

                        listBox1.Items.Add(item["Name"]);
                        listBox2.Items.Add(item["Price"]);
                    }

                }
                else
                {
                    MessageBox.Show("XML файл не знайдено", "Помилка");
                }
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows.Clear();
                listBox1.Items.Clear();  
                listBox2.Items.Clear();
            }
            else MessageBox.Show("Таблиця порожня", "Помилка");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < dgv.Rows.Count)
            {
                int n = listBox1.SelectedIndex;
                var manager = new Manager
                {
                    Name = dgv.Rows[n].Cells[0].Value.ToString(),
                    NameCli = dgv.Rows[n].Cells[1].Value.ToString(),
                    Price = dgv.Rows[n].Cells[2].Value.ToString(),
                    Auto = dgv.Rows[n].Cells[3].Value.ToString(),
                };
                pg1.SelectedObject = manager;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = listBox2.SelectedIndex;
            var sale = new Sale
            {
                cManager = dgv.Rows[n].Cells[0].Value.ToString(),
                cAuto = dgv.Rows[n].Cells[3].Value.ToString(),
                cPrice = dgv.Rows[n].Cells[2].Value.ToString(),
                cReward = dgv.Rows[n].Cells[4].Value.ToString()
            };
            pg2.SelectedObject = sale;
        }
        private List<ManagerSales> topManagers;

        private void uptop5btn_Click(object sender, EventArgs e)
        {
            List<ManagerSales> managerSalesList = new List<ManagerSales>();
           
            foreach (DataGridViewRow row in dgv.Rows)
            {
                string name = row.Cells[0].Value?.ToString();
                if (double.TryParse(row.Cells[2].Value?.ToString(), out double price))
                {
                    var managerSales = managerSalesList.FirstOrDefault(m => m.ManagerName == name);
                    if (managerSales == null)
                    {
                        managerSalesList.Add(new ManagerSales { ManagerName = name, TotalSales = price });
                    }
                    else
                    {
                        managerSales.TotalSales += price;
                    }
                }
            }
            QuickSortWrapper(managerSalesList);
            topManagers = managerSalesList.Take(5).ToList();

            listBox3.Items.Clear();
            foreach (var manager in topManagers)
            {
                listBox3.Items.Add(manager.ManagerName);
            }
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex >= 0 && listBox3.SelectedIndex < topManagers.Count)
            {
                int n = listBox3.SelectedIndex;
                pg3.SelectedObject = topManagers[n];
            }
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backbtn1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void backbtn2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void dgv_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                int n = dgv.SelectedRows[0].Index;
                nametxt.Text = dgv.Rows[n].Cells[0].Value.ToString();
                nameclitxt.Text = dgv.Rows[n].Cells[1].Value.ToString();
                pricetxt.Text = dgv.Rows[n].Cells[2].Value.ToString();
                autotxt.Text = dgv.Rows[n].Cells[3].Value.ToString();
            }
        }
    }
}
