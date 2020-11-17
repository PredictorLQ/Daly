﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Threading;

namespace Daly
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();

        private Excel.Application excel;
        private DataDaly DataDaly;
        private readonly string path_excel = "\\excel.xlsx";
        private readonly string path_data = "\\data.xlsx";
        public static ProgressBar _progressBar1;
        public static bool Error_Excel = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            excel = new Excel.Application();
            DataDaly = new DataDaly();
            bool control = false, control_excel = false;
            if (excel != null)
            {
                try
                {
                    excel.Visible = false;
                    Excel.Workbook book_excel = excel.Workbooks.Open(@Application.StartupPath.ToString() + path_excel,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing);
                    DataDaly.GetInfoData(book_excel, progressBar1);
                    book_excel.Close(false, Type.Missing, Type.Missing);
                    progressBar1.Value = progressBar1.Maximum;
                    control_excel = true;
                }
                catch
                {
                    MessageBox.Show("Error: Загрузка данных популяции фатальна");
                    excel.Quit();
                }
                finally
                {
                    GC.Collect();
                }
            }
            else
            {
                MessageBox.Show("Error: Экземпляр Excel не создан");
            }
            if (control_excel == true)
            {

                try
                {
                    Excel.Workbook book_data = excel.Workbooks.Open(@Application.StartupPath.ToString() + path_data,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing);
                    DataDaly.GetDataSetDaly(book_data, progressBar2);
                    progressBar1.Value = progressBar1.Maximum;
                    book_data.Close(false, Type.Missing, Type.Missing);
                    control = true;
                }
                catch
                {
                    MessageBox.Show("Error: Загрузка данных заболеваний фатальна");
                }
                finally
                {
                    excel.Quit();
                    GC.Collect();
                }
            }
            if (control)
            {
                //Thread myThread = new Thread(new ParameterizedThreadStart(DataDaly.GetSurvival));
                //myThread.IsBackground = true;
                //myThread.Priority = ThreadPriority.Highest;
                //myThread.Start(progressBar3);
                DataDaly.GetSurvival(progressBar3);
                listBox1.DataSource = DataDaly.DataDiases.Select(u => u.Name).ToList();
                listBox2.DataSource = DataDaly.DataRegion.Select(u => u.Name).ToList();
                listBox3.DataSource = DataDaly.DataYear;
                //myThread.Join();
                progressBar3.Value = progressBar3.Maximum;
                MessageBox.Show("Загрузка и преобразование данных успешно завершены");
            }
        }
        private bool ControlSelectLitbox()
        {
            bool control = true;
            DataDaly.ActivDataRegion_Id = new List<int>();
            DataDaly.ActivDataYear_Id = new List<int>();
            DataDaly.ActivDataDiases_Id = new List<int>();

            for (int i=0; i< listBox2.SelectedItems.Count; i++)
            {
                DataRegion DataRegion = DataDaly.DataRegion.FirstOrDefault(u => u.Name == listBox2.SelectedItems[i].ToString());
                if (DataRegion != null)
                {
                    DataDaly.ActivDataRegion_Id.Add(DataRegion.Id);
                }
            }

            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                DataDiases DataDiases = DataDaly.DataDiases.FirstOrDefault(u => u.Name == listBox1.SelectedItems[i].ToString());
                if (DataDiases != null)
                {
                    DataDaly.ActivDataDiases_Id.Add(DataDiases.Id);
                }
            }
            for (int i = 0; i < listBox3.SelectedItems.Count; i++)
            {
                DataDaly.ActivDataYear_Id.Add(Convert.ToInt32(listBox3.SelectedItems[i]));
            }


            if (DataDaly.ActivDataYear_Id.Count() == 0)
            {
                control = false;
                MessageBox.Show("Не выбран хоть 1 год");
            }
            if (DataDaly.ActivDataDiases_Id.Count() == 0)
            {
                control = false;
                MessageBox.Show("Не выбран хоть 1 заболевание");
            }
            if (DataDaly.ActivDataRegion_Id.Count() == 0)
            {
                control = false;
                MessageBox.Show("Не выбран хоть 1 регион");
            }

            return control;
        }
        private void loadDALYDataFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveDALYDataToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resetDALYCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void constantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Constant Constant = new Constant();
            Constant.Show();
        }

        private void населениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ControlSelectLitbox() == true)
            {
                Population Population = new Population();
                Population.Show();
            }
        }

        private void дожитиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ControlSelectLitbox() == true)
            {
                Form2 Form2 = new Form2();
                Form2.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bool all_diases = checkBox1.Checked, all_region = checkBox2.Checked, all_years = checkBox3.Checked;
            if (ControlSelectLitbox() == true)
            {
                Calculator Calculator = new Calculator();
                Calculator.Show();
            }
        }

        private void progressBar1_DockChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            bool all = checkBox.Checked;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SetSelected(i, all);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            bool all = checkBox.Checked;
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                listBox2.SetSelected(i, all);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            bool all = checkBox.Checked;
            for(int i=0; i< listBox3.Items.Count; i++)
            {
                listBox3.SetSelected(i, all);
            }
        }
    }
}