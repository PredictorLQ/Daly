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
using System.IO;

namespace Daly
{
    public partial class Form1 : Form
    {


        private Excel.Application excel;
        private DataDaly DataDaly;
        private readonly string path_excel = "\\населения.xlsx";
        private readonly string path_data = "\\умершие по болезни.xlsx";
        private readonly string path_data_min = "\\ожидаемая продолжительность жизни.xlsx";
        private readonly string path_result = "\\Результаты";
        private readonly string[] elem_max = { "mx", "qx", "px", "l", "d", "L", "T", "e0", "mxl", "YLL", "Потери (руб.)" };
        private readonly string[] elem_min = { "e0", "mxl", "YLL", "Потери (руб.)" };
        public static bool Error_Excel = false;
        public bool ControlSave = false;
        public bool ControlWrite = false;
        public Form1()
        {
            InitializeComponent();
            button10.Enabled = ControlWrite;
            saveDALYDataToFileToolStripMenuItem.Enabled = ControlSave;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //excel = new Excel.Application();
            //excel2 = new Excel.Application();

            //excel.Visible = false;
            //excel2.Visible = true;
            //Excel.Workbook workBook = excel2.Workbooks.Add(Type.Missing);

            //Excel.Workbook book_excel = excel.Workbooks.Open(@Application.StartupPath.ToString() + path_new,
            //      Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            //      Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            //      Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            //      Type.Missing, Type.Missing);
            //int count = book_excel.Sheets.Count;
            //for (int z = 1; z <= book_excel.Sheets.Count; z++)
            //{
            //    Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)book_excel.Sheets[z];
            //    var xlNewSheet = (Excel.Worksheet)workBook.Sheets.Add(After: workBook.ActiveSheet);
            //    xlNewSheet.Name = ObjWorkSheet.Name;
            //    xlNewSheet.Cells.NumberFormat = "@";


            //    Excel.Range excelRange = ObjWorkSheet.UsedRange;
            //    int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count;
            //    List<string>[,] list = new List<string>[rows, colums];
            //    for (int i = 1, i2 = 1; i < rows; i++, i2++)
            //    {
            //        if (i == 1)
            //            xlNewSheet.get_Range("A1", "B1").Merge();
            //        if (i == 5)
            //        {
            //            string sex = excelRange.Cells[3, 2].Value2.ToString().ToLower(), read = "0";
            //            xlNewSheet.Cells[i2, 1] = "Чтение";
            //            if (sex.IndexOf("муж") > -1 || sex.IndexOf("жен") > -1)
            //                read = "1";
            //            xlNewSheet.Cells[i2, 2] = read;
            //            (xlNewSheet.Cells[i2, 1] as Microsoft.Office.Interop.Excel.Range).Font.Bold = true;
            //            (xlNewSheet.Cells[i2, 1] as Microsoft.Office.Interop.Excel.Range).Interior.Color = (excelRange.Cells[4, 1] as Microsoft.Office.Interop.Excel.Range).Interior.Color;
            //            i2++;
            //        }
            //        for (int j = 1; j <= colums; j++)
            //        {
            //            if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j].Value2 != null)
            //            {
            //                string text = excelRange.Cells[i, j].Value2.ToString();
            //                if (i == 6 && j == 1)
            //                    text = "1-Томская область";
            //                if (i == 4 && j == 2)
            //                    text = "7";
            //                xlNewSheet.Cells[i2, j] = text;
            //                (xlNewSheet.Cells[i2, j] as Microsoft.Office.Interop.Excel.Range).Interior.Color = (excelRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Interior.Color;
            //                (xlNewSheet.Cells[i2, j] as Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = (excelRange.Cells[i, j] as Microsoft.Office.Interop.Excel.Range).Borders.LineStyle;
            //                if (i < 5)
            //                    (xlNewSheet.Cells[i2, 1] as Microsoft.Office.Interop.Excel.Range).Font.Bold = true;
            //                if (i == 7 && j == 1)
            //                {
            //                    for (int k = 0; k < 5; k++, i2++)
            //                    {
            //                        xlNewSheet.Cells[i2, 1] = k.ToString();
            //                        (xlNewSheet.Cells[i2, 1] as Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = (excelRange.Cells[7, 1] as Microsoft.Office.Interop.Excel.Range).Borders.LineStyle;
            //                        for (int j2 = 2; j2 <= colums; j2++)
            //                        {
            //                            xlNewSheet.Cells[i2, j2] = "0";
            //                            (xlNewSheet.Cells[i2, j2] as Microsoft.Office.Interop.Excel.Range).Interior.Color = (excelRange.Cells[i, j2] as Microsoft.Office.Interop.Excel.Range).Interior.Color;
            //                            (xlNewSheet.Cells[i2, j2] as Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = (excelRange.Cells[i, j2] as Microsoft.Office.Interop.Excel.Range).Borders.LineStyle;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        (xlNewSheet.Cells[13, 1] as Microsoft.Office.Interop.Excel.Range).Borders.LineStyle = (excelRange.Cells[7, 1] as Microsoft.Office.Interop.Excel.Range).Borders.LineStyle;
            //        xlNewSheet.Cells[13, 1] = "0-4";
            //    }
            //}
            //excel.Quit();
            //GC.Collect();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            excel = new Excel.Application();
            DataDaly = new DataDaly();
            bool control = false;
            if (excel != null)
            {
                //try
                //{
                    excel.Visible = false;
                    Excel.Workbook book_excel = excel.Workbooks.Open(@Application.StartupPath.ToString() + path_excel,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing);
                    DataDaly.GetInfoData(book_excel, progressBar1);
                    book_excel.Close(false, Type.Missing, Type.Missing);
                    progressBar1.Value = progressBar1.Maximum;
                    control = true;
                    if (control)
                    {
                        //try
                        //{

                            progressBar2.Value = progressBar1.Minimum;
                            Excel.Workbook book_data = excel.Workbooks.Open(@Application.StartupPath.ToString() + path_data,
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                  Type.Missing, Type.Missing);
                            DataDaly.GetDataSetDaly(book_data, progressBar2);
                            progressBar2.Value = progressBar2.Maximum;
                            book_data.Close(false, Type.Missing, Type.Missing);
                        //}
                        //catch
                        //{
                        //    MessageBox.Show("Error: Загрузка данных популяции фатальна-1");
                        //    excel.Quit();
                        //}
                    }
                excel.Quit();
                GC.Collect();
                //}
                //catch
                //{
                //    Error_Excel = true;
                //    MessageBox.Show("Error: Загрузка данных популяции фатальна -2");
                //    excel.Quit();
                //}
                //finally
                //{
                //    excel.Quit();
                //    GC.Collect();
                //}
            }
            else
            {
                MessageBox.Show("Error: Экземпляр Excel не создан");
            }
            if (Error_Excel == false)
            {
                //Thread myThread = new Thread(new ParameterizedThreadStart(DataDaly.GetSurvival));
                //myThread.IsBackground = true;
                //myThread.Priority = ThreadPriority.Highest;
                //myThread.Start(progressBar3);
                listBox1.DataSource = DataDaly.DataDiases.Select(u => u.Name).ToList();
                listBox2.DataSource = DataDaly.DataRegion.Select(u => u.Name).ToList();
                listBox3.DataSource = DataDaly.DataYear;
                //myThread.Join();
            }
        }

        private void полныйПакетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Error_Excel == false)
            {
                try
                {
                    progressBar3.Value = progressBar1.Minimum;
                    DataDaly.SelectPaket = 1;

                    DataDaly.GetSurvival(progressBar3);
                    progressBar3.Value = progressBar3.Maximum;
                    MessageBox.Show("Данные успешно преробразованы");
                    ControlSave = true; ControlWrite = true; button10.Enabled = ControlWrite;
                    label8.Text = DataDaly.SelectPaketName[DataDaly.SelectPaket - 1];
                    label8.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Error: Преобразование фатально");
                }
                finally
                {
                    excel.Quit();
                    GC.Collect();
                }
            }
        }

        private void частичныйПакетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Error_Excel == false)
            {
                try
                {
                    DataDaly.SelectPaket = 2;
                    progressBar2.Value = progressBar1.Minimum;
                    progressBar3.Value = progressBar1.Minimum;
                    Excel.Workbook book_data = excel.Workbooks.Open(@Application.StartupPath.ToString() + path_data_min,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                          Type.Missing, Type.Missing);
                    DataDaly.GetDataSetDalyMin(book_data, progressBar2);
                    progressBar1.Value = progressBar1.Maximum;
                    book_data.Close(false, Type.Missing, Type.Missing);

                    DataDaly.GetSurvivalMin(progressBar3);
                    progressBar3.Value = progressBar3.Maximum;
                    MessageBox.Show("Данные успешно получены и преробразованы");
                    ControlSave = true; ControlWrite = true; button10.Enabled = ControlWrite;
                    label8.Text = DataDaly.SelectPaketName[DataDaly.SelectPaket - 1];
                    label8.Visible = true;
                }
                catch
                {
                    ControlSave = false; ControlWrite = false; button10.Enabled = ControlWrite;
                    MessageBox.Show("Error: Загрузка данных фатальна");
                }
                finally
                {
                    excel.Quit();
                    GC.Collect();
                }
            }
        }

        private void fileToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            saveDALYDataToFileToolStripMenuItem.Enabled = ControlSave;
        }

        private void таблицыToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            if (DataDaly.SelectPaket == 1)
                дожитиеToolStripMenuItem.Enabled = true;
            else
                дожитиеToolStripMenuItem.Enabled = false;
        }
        private bool ControlSelectLitbox()
        {
            bool control = true;
            DataDaly.ActivDataRegion_Id = new List<int>();
            DataDaly.ActivDataYear_Id = new List<int>();
            DataDaly.ActivDataDiases_Id = new List<int>();

            for (int i = 0; i < listBox2.SelectedItems.Count; i++)
            {
                DataRegion DataRegion = DataDaly.DataRegion.FirstOrDefault(u => u.Name == listBox2.SelectedItems[i].ToString());
                if (DataRegion != null)
                    DataDaly.ActivDataRegion_Id.Add(DataRegion.Id);
            }

            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                DataDiases DataDiases = DataDaly.DataDiases.FirstOrDefault(u => u.Name == listBox1.SelectedItems[i].ToString());
                if (DataDiases != null)
                    DataDaly.ActivDataDiases_Id.Add(DataDiases.Id);
            }
            for (int i = 0; i < listBox3.SelectedItems.Count; i++)
                DataDaly.ActivDataYear_Id.Add(Convert.ToInt32(listBox3.SelectedItems[i]));


            if (DataDaly.ActivDataYear_Id.Count == 0)
            {
                control = false;
                MessageBox.Show("Не выбран хоть 1 год");
            }
            if (DataDaly.ActivDataDiases_Id.Count == 0)
            {
                control = false;
                MessageBox.Show("Не выбран хоть 1 заболевание");
            }
            if (DataDaly.ActivDataRegion_Id.Count == 0)
            {
                control = false;
                MessageBox.Show("Не выбран хоть 1 регион");
            }
            if (DataDaly.SelectPaket > 0) { ControlWrite = control; button10.Enabled = ControlWrite; }
            return control;
        }
        private void saveDALYDataToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ControlSelectLitbox() == true)
            {
                bool max = DataDaly.SelectPaket == 1;
                int interval = max ? 13 : 6, start = 1, intrevla2 = max ? 8 : 1;
                string[] elem = max ? elem_max : elem_min;
                string path = @Application.StartupPath.ToString() + path_result, prepend = DataDaly.SelectPaketName[DataDaly.SelectPaket - 1] + " - ";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                excel = new Excel.Application();
                excel.Visible = true;
                Excel.Workbook workBook = excel.Workbooks.Add(Type.Missing);
                label7.Visible = true;
                progressBar4.Visible = true;
                progressBar4.Minimum = 0;
                progressBar4.Value = 0;
                progressBar4.Maximum = DataDaly.ActivDataRegion_Id.Count * DataDaly.ActivDataDiases_Id.Count * DataDaly.ActivDataYear_Id.Count * DataDaly.DataPopulation.Count;
                int count_year = DataDaly.ActivDataYear_Id.Count;
                List<DataPopulation> count_popul = DataDaly.DataPopulation.Where(u => (u.Start_Daly_Bool == true && max == false) || max == true).ToList();
                for (int i = 0; i < DataDaly.ActivDataRegion_Id.Count; i++)
                {
                    DataRegion DataRegion = DataDaly.DataRegion.FirstOrDefault(u => u.Id == DataDaly.ActivDataRegion_Id[i]);
                    if (DataRegion != null)
                    {
                        var xlNewSheet = (Excel.Worksheet)workBook.Sheets.Add(workBook.Sheets[1], Type.Missing, Type.Missing, Type.Missing);
                        xlNewSheet.Name = $"{DataRegion.Id}-{DataRegion.Name}";
                        xlNewSheet.Cells.NumberFormat = "@";
                        int start_row = 1;
                        for (int j = 0; j < DataDaly.ActivDataDiases_Id.Count; j++)
                        {
                            DataDiases DataDiases = DataDaly.DataDiases.FirstOrDefault(u => u.Id == DataDaly.ActivDataDiases_Id[i]);
                            if (DataDiases != null)
                            {
                                for (int l = 0; l < count_year; l++)
                                {
                                    xlNewSheet.Cells[start_row, start] = $"{DataDiases.MCB10} - {DataDiases.Name}";
                                    xlNewSheet.Cells[start_row, start + 1] = DataDaly.ActivDataYear_Id[l];
                                    start_row++;
                                    xlNewSheet.Cells[start_row, start] = "Мужчины";
                                    xlNewSheet.Cells[start_row, start + interval] = "Женщины";
                                    xlNewSheet.Cells[start_row, start + interval * 2] = "Мужчины+Женщины";
                                    start_row++;
                                    for (int z = 0; z < elem.Length; z++)
                                    {
                                        xlNewSheet.Cells[start_row, start + 1 + z] = elem[z];
                                        xlNewSheet.Cells[start_row, start + interval + 1 + z] = elem[z];
                                        xlNewSheet.Cells[start_row, start + interval * 2 + z] = elem[z];
                                    }

                                    (double, double, double) vrp_all = (0, 0, 0);
                                    for (int k = 0; k < count_popul.Count; k++)
                                    {
                                        try
                                        {
                                            start_row++;
                                            DataSetDaly DataSetDaly = DataDaly.DataSetDaly.FirstOrDefault(u => u.DataPopulation_Id == count_popul[k].Id
                                            && u.Year == DataDaly.ActivDataYear_Id[l]
                                            && u.DataRegion_Id == DataDaly.ActivDataRegion_Id[i]);
                                            var diases = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == DataDaly.ActivDataDiases_Id[j]);

                                            xlNewSheet.Cells[start_row, start] = count_popul[k].Name;
                                            xlNewSheet.Cells[start_row, start + interval] = count_popul[k].Name;
                                            xlNewSheet.Cells[start_row, start + interval * 2] = count_popul[k].Name;
                                            if (max)
                                            {
                                                xlNewSheet.Cells[start_row, start + 1] = diases.DataSurvivalMale.mx;
                                                xlNewSheet.Cells[start_row, start + 2] = diases.DataSurvivalMale.qx;
                                                xlNewSheet.Cells[start_row, start + 3] = diases.DataSurvivalMale.px;
                                                xlNewSheet.Cells[start_row, start + 4] = diases.DataSurvivalMale.l;
                                                xlNewSheet.Cells[start_row, start + 5] = diases.DataSurvivalMale.d;
                                                xlNewSheet.Cells[start_row, start + 6] = diases.DataSurvivalMale.L;
                                                xlNewSheet.Cells[start_row, start + 7] = diases.DataSurvivalMale.T;

                                                xlNewSheet.Cells[start_row, start + interval + 1] = diases.DataSurvivalFemale.mx;
                                                xlNewSheet.Cells[start_row, start + interval + 2] = diases.DataSurvivalFemale.qx;
                                                xlNewSheet.Cells[start_row, start + interval + 3] = diases.DataSurvivalFemale.px;
                                                xlNewSheet.Cells[start_row, start + interval + 4] = diases.DataSurvivalFemale.l;
                                                xlNewSheet.Cells[start_row, start + interval + 5] = diases.DataSurvivalFemale.d;
                                                xlNewSheet.Cells[start_row, start + interval + 6] = diases.DataSurvivalFemale.L;
                                                xlNewSheet.Cells[start_row, start + interval + 7] = diases.DataSurvivalFemale.T;

                                                xlNewSheet.Cells[start_row, start + interval * 2 + 1] = diases.DataSurvivalSumm.mx;
                                                xlNewSheet.Cells[start_row, start + interval * 2 + 2] = diases.DataSurvivalSumm.qx;
                                                xlNewSheet.Cells[start_row, start + interval * 2 + 3] = diases.DataSurvivalSumm.px;
                                                xlNewSheet.Cells[start_row, start + interval * 2 + 4] = diases.DataSurvivalSumm.l;
                                                xlNewSheet.Cells[start_row, start + interval * 2 + 5] = diases.DataSurvivalSumm.d;
                                                xlNewSheet.Cells[start_row, start + interval * 2 + 6] = diases.DataSurvivalSumm.L;
                                                xlNewSheet.Cells[start_row, start + interval * 2 + 7] = diases.DataSurvivalSumm.T;
                                            }
                                            xlNewSheet.Cells[start_row, start + intrevla2] = diases.DataSurvivalMale.e0;
                                            xlNewSheet.Cells[start_row, start + intrevla2 + 1] = diases.DataSurvivalMale.mxl;

                                            xlNewSheet.Cells[start_row, start + interval + intrevla2] = diases.DataSurvivalMale.e0;
                                            xlNewSheet.Cells[start_row, start + interval + intrevla2 + 1] = diases.DataSurvivalMale.mxl;

                                            xlNewSheet.Cells[start_row, start + interval * 2 + intrevla2] = diases.DataSurvivalSumm.e0;
                                            xlNewSheet.Cells[start_row, start + interval * 2 + intrevla2 + 1] = diases.DataSurvivalSumm.mxl;

                                            if (DataDaly.DataPopulation[k].Start_Daly_Bool == true)
                                            {
                                                (double, double, double) vrp = (diases.DataSurvivalMale.VRP, diases.DataSurvivalFemale.VRP, diases.DataSurvivalSumm.VRP);
                                                if (DataDaly.DataPopulation[k].Id == 19)
                                                {
                                                    vrp.Item1 /= 2.0;
                                                    vrp.Item2 /= 2.0;
                                                    vrp.Item3 /= 2.0;
                                                }

                                                vrp_all.Item1 += vrp.Item1;
                                                vrp_all.Item2 += vrp.Item2;
                                                vrp_all.Item3 += vrp.Item3;

                                                xlNewSheet.Cells[start_row, interval - 2] = diases.DataSurvivalMale.YLL;
                                                xlNewSheet.Cells[start_row, interval - 1] = vrp.Item1;

                                                xlNewSheet.Cells[start_row, interval * 2 - 2] = diases.DataSurvivalFemale.YLL;
                                                xlNewSheet.Cells[start_row, interval * 2 - 1] = vrp.Item2;

                                                xlNewSheet.Cells[start_row, interval * 3 - 3] = diases.DataSurvivalSumm.YLL;
                                                xlNewSheet.Cells[start_row, interval * 3 - 2] = vrp.Item3;
                                            }
                                        }
                                        catch { }
                                    }
                                    start_row++;
                                    xlNewSheet.Cells[start_row, start] = "Итого";
                                    xlNewSheet.Cells[start_row, start + interval] = "Итого";
                                    xlNewSheet.Cells[start_row, start + interval * 2] = "Итого";

                                    xlNewSheet.Cells[start_row, interval - 1] = vrp_all.Item1;
                                    xlNewSheet.Cells[start_row, interval * 2 - 1] = vrp_all.Item2;
                                    xlNewSheet.Cells[start_row, interval * 3 - 2] = vrp_all.Item3;

                                    start_row += 2;
                                    progressBar4.Value++;
                                }
                                start_row += 1;
                            }
                            progressBar4.Value++;
                        }
                    }
                    progressBar4.Value++;
                }
                string path_ex = path + "\\" + prepend + DateTime.Now.ToString().Replace(":", "-") + ".xlsx";
                excel.Application.ActiveWorkbook.SaveAs(path_ex, Type.Missing,
      Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
      Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //excel.Quit();
                progressBar4.Value = progressBar1.Maximum;
                label7.Visible = false;
                progressBar4.Visible = false;
            }
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
            checkBox3.Checked = !(listBox3.SelectedItems.Count < listBox3.Items.Count);
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !(listBox2.SelectedItems.Count < listBox2.Items.Count);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !(listBox1.SelectedItems.Count < listBox1.Items.Count);
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
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                listBox3.SetSelected(i, all);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
