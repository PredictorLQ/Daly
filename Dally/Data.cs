using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Daly
{
    public class DataDaly
    {
        static int id = 0;
        public static int SelectPaket { get; set; }
        public static string[] SelectPaketName { get; set; } = { "расчетная ОПЖ", "готовая ОПЖ" };
        public static List<DataRegion> DataRegion;
        public static List<DataDiases> DataDiases;
        public static List<DataPopulation> DataPopulation;
        public static List<DataVRP> DataVRP;
        public static List<int> DataYear;
        public static List<DataSetDaly> DataSetDaly;
        public static List<DataSurvivalPeriod_0_year> DataSurvivalPeriod_0_year;
        public static List<DataSurvivalPeriod_20_year> DataSurvivalPeriod_20_year;
        public static List<DataSurvivalPeriod_70_year> DataSurvivalPeriod_70_year;
        public static List<int> ActivDataYear_Id { get; set; }
        public static List<int> ActivDataRegion_Id { get; set; }
        public static List<int> ActivDataDiases_Id { get; set; }
        static void GetDataSetDalyItem(Excel.Worksheet ObjWorkSheet)
        {
            Excel.Range excelRange = ObjWorkSheet.UsedRange;
            string code_mcb10 = excelRange.Cells[2, 2].Value2.ToString();
            int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count, row_year = 0, region_id = 0, code_mcb10_id = DataDiases.First(u => u.MCB10 == code_mcb10).Id,
                row_start = Convert.ToInt32(excelRange.Cells[4, 2].Value2.ToString());
            bool control = true, Male = excelRange.Cells[3, 2].Value2.IndexOf("муж") > -1;
            for (int i = row_start; i <= rows; i++)
            {
                if (excelRange.Cells[i, 1] != null && excelRange.Cells[i, 1].Value2 != null)
                {
                    if (excelRange.Cells[i, 1].Value2.ToString() != "")
                    {
                        if (control == true)
                        {
                            control = false;
                            region_id = Convert.ToInt32(excelRange.Cells[i, 1].Value2.ToString().Split(new char[] { '-' })[0]);
                            row_year = i;
                        }
                        else
                        {
                            string NamePeriod = excelRange.Cells[i, 1].Value2.ToString();
                            DataPopulation population = DataPopulation.FirstOrDefault(u => u.Name == NamePeriod);
                            if (population != null)
                            {
                                for (int j = 2; j <= colums; j++)
                                {
                                    int year = Convert.ToInt32(excelRange.Cells[row_year, j].Value2);
                                    if (year > 0)
                                    {
                                        DataSetDaly data_dely = DataSetDaly.First(u => u.Year == year && u.DataPopulation_Id == population.Id && u.DataRegion_Id == region_id);
                                        DataSetDalyDiases DataSetDalyDiases = data_dely.DataSetDalyDiases.FirstOrDefault(u => u.DataDiases_Id == code_mcb10_id);
                                        if (DataSetDalyDiases != null)
                                        {
                                            if (Male == true)
                                                DataSetDalyDiases.MaleDied = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                            else
                                                DataSetDalyDiases.FemaleDied = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                        }
                                        else
                                        {
                                            DataSetDalyDiases = new DataSetDalyDiases
                                            {
                                                Id = id,
                                                DataDiases_Id = code_mcb10_id,
                                                DataSurvivalMale = new DataSurvival(),
                                                DataSurvivalFemale = new DataSurvival(),
                                                DataSurvivalSumm = new DataSurvival(),
                                            };
                                            if (Male == true)
                                                DataSetDalyDiases.MaleDied = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                            else
                                                DataSetDalyDiases.FemaleDied = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                            data_dely.DataSetDalyDiases.Add(DataSetDalyDiases);
                                            id++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        control = true;
                    }
                }
            }
            List<DataPopulation> _DataPopulation = DataPopulation.Where(u => u.Excel == false).ToList();
            foreach (var item in _DataPopulation)
            {
                foreach (var year in DataYear)
                {
                    id++;
                    DataSetDaly.Add(new DataSetDaly
                    {
                        Id = id,
                        Year = year,
                        DataRegion_Id = region_id,
                        DataPopulation_Id = item.Id,
                        TrueResult = true,
                        DataSetDalyDiases = new List<DataSetDalyDiases> {
                            new DataSetDalyDiases { Id = id, DataDiases_Id = code_mcb10_id}
                        }
                    });
                }
            }
        }
        static void GetDataSetDalyItemMin(Excel.Worksheet ObjWorkSheet)
        {
            Excel.Range excelRange = ObjWorkSheet.UsedRange;
            string code_mcb10 = excelRange.Cells[2, 2].Value2.ToString();
            int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count, row_year = 0, region_id = 0, code_mcb10_id = DataDiases.First(u => u.MCB10 == code_mcb10).Id,
                row_start = Convert.ToInt32(excelRange.Cells[4, 2].Value2.ToString()), id = 1, sex = excelRange.Cells[3, 2].Value2.IndexOf("муж") > -1 ? 1 : excelRange.Cells[3, 2].Value2.IndexOf("жен") > -1 ? 0 : 2;
            bool control = true;
            for (int i = row_start; i <= rows; i++)
            {
                if (excelRange.Cells[i, 1] != null && excelRange.Cells[i, 1].Value2 != null)
                {
                    if (excelRange.Cells[i, 1].Value2.ToString() != "")
                    {
                        if (control == true)
                        {
                            control = false;
                            region_id = Convert.ToInt32(excelRange.Cells[i, 1].Value2.ToString().Split(new char[] { '-' })[0]);
                            row_year = i;
                        }
                        else
                        {
                            string NamePeriod = excelRange.Cells[i, 1].Value2.ToString();
                            DataPopulation population = DataPopulation.FirstOrDefault(u => u.Name == NamePeriod);
                            if (population != null)
                            {
                                for (int j = 2; j <= colums; j++)
                                {
                                    int year = Convert.ToInt32(excelRange.Cells[row_year, j].Value2);
                                    if (year > 0)
                                    {
                                        DataSetDaly data_dely = DataSetDaly.First(u => u.Year == year && u.DataPopulation_Id == population.Id && u.DataRegion_Id == region_id);
                                        DataSetDalyDiases DataSetDalyDiases = data_dely.DataSetDalyDiases.FirstOrDefault(u => u.DataDiases_Id == code_mcb10_id);
                                        if (DataSetDalyDiases != null)
                                        {
                                            if (sex == 1)
                                                DataSetDalyDiases.DataSurvivalMale.e0 = Convert.ToDouble(excelRange.Cells[i, j].Value2);
                                            else if (sex == 0)
                                                DataSetDalyDiases.DataSurvivalFemale.e0 = Convert.ToDouble(excelRange.Cells[i, j].Value2);
                                            else
                                                DataSetDalyDiases.DataSurvivalSumm.e0 = Convert.ToDouble(excelRange.Cells[i, j].Value2);
                                        }
                                        else
                                        {
                                            DataSetDalyDiases = new DataSetDalyDiases
                                            {
                                                Id = id,
                                                DataDiases_Id = code_mcb10_id,
                                                DataSurvivalMale = new DataSurvival(),
                                                DataSurvivalFemale = new DataSurvival(),
                                                DataSurvivalSumm = new DataSurvival(),
                                            };
                                            if (sex == 1)
                                                DataSetDalyDiases.DataSurvivalMale.e0 = Convert.ToDouble(excelRange.Cells[i, j].Value2);
                                            else if (sex == 0)
                                                DataSetDalyDiases.DataSurvivalFemale.e0 = Convert.ToDouble(excelRange.Cells[i, j].Value2);
                                            else
                                                DataSetDalyDiases.DataSurvivalSumm.e0 = Convert.ToDouble(excelRange.Cells[i, j].Value2);
                                            data_dely.DataSetDalyDiases.Add(DataSetDalyDiases);
                                            id++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        control = true;
                }
            }
            List<DataPopulation> _DataPopulation = DataPopulation.Where(u => u.Excel == false).ToList();
            foreach (var item in _DataPopulation)
            {
                foreach (var year in DataYear)
                {
                    if (DataSetDaly.Any(u => u.Id == id && u.Year == year && u.DataRegion_Id == region_id && u.DataPopulation_Id == item.Id) == false)
                        DataSetDaly.Add(new DataSetDaly
                        {
                            Id = id,
                            Year = year,
                            DataRegion_Id = region_id,
                            DataPopulation_Id = item.Id,
                            TrueResult = true,
                            DataSetDalyDiases = new List<DataSetDalyDiases> {
                            new DataSetDalyDiases { Id = id, DataDiases_Id = code_mcb10_id}
                        }
                        });
                }
            }
        }
        public void GetDataSetDaly(Excel.Workbook book_data, ProgressBar ProgressBar)
        {
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = book_data.Sheets.Count * 2;
            for (int i = 1; i <= book_data.Sheets.Count; i++)
            {
                ProgressBar.Value++;
                Excel.Worksheet list_data = (Excel.Worksheet)book_data.Sheets[i];
                GetDataSetDalyItem(list_data);
                ProgressBar.Value++;
            }
            ProgressBar.Value = ProgressBar.Maximum;
        }
        public void GetDataSetDalyMin(Excel.Workbook book_data, ProgressBar ProgressBar)
        {
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = book_data.Sheets.Count * 2;
            for (int i = 1; i <= book_data.Sheets.Count; i++)
            {
                ProgressBar.Value++;
                Excel.Worksheet list_data = (Excel.Worksheet)book_data.Sheets[i];
                GetDataSetDalyItemMin(list_data);
                ProgressBar.Value++;
            }
            ProgressBar.Value = ProgressBar.Maximum;
        }
        public void GetInfo(Excel.Worksheet ObjWorkSheet)
        {
            Excel.Range excelRange = ObjWorkSheet.UsedRange;
            int row_start = Convert.ToInt32(excelRange.Cells[1, 6].Value2),
                colum_region = Convert.ToInt32(excelRange.Cells[2, 3].Value2),
                colum_population = Convert.ToInt32(excelRange.Cells[3, 3].Value2),
                colum_years = Convert.ToInt32(excelRange.Cells[4, 3].Value2),
                colum_diases = Convert.ToInt32(excelRange.Cells[5, 3].Value2),
                rows = excelRange.Rows.Count;
            DataRegion = new List<DataRegion>();
            DataPopulation = new List<DataPopulation>();
            DataDiases = new List<DataDiases>();
            DataYear = new List<int>();
            for (int i = row_start; i <= rows; i++)
            {
                if (excelRange.Cells[i, colum_region] != null && excelRange.Cells[i, colum_region + 1].Value2 != null)
                {
                    try
                    {
                        if (excelRange.Cells[i, colum_region].Value2.ToString() != "" && excelRange.Cells[i, colum_region + 1].Value2.ToString() != "")
                        {
                            DataRegion.Add(new DataRegion
                            {
                                Id = Convert.ToInt32(excelRange.Cells[i, colum_region].Value2),
                                Name = excelRange.Cells[i, colum_region + 1].Value2.ToString()
                            });
                        }
                    }
                    catch { }
                }
                if (excelRange.Cells[i, colum_years] != null)
                {
                    try
                    {
                        if (excelRange.Cells[i, colum_years].Value2.ToString() != "")
                        {
                            DataYear.Add(Convert.ToInt32(excelRange.Cells[i, colum_years].Value2));
                        }
                        DataYear = DataYear.OrderBy(u => u).ToList();
                    }
                    catch { }
                }
                if (excelRange.Cells[i, colum_diases] != null)
                {
                    try
                    {
                        if (excelRange.Cells[i, colum_diases].Value2.ToString() != "" && excelRange.Cells[i, colum_diases + 1].Value2.ToString() != "")
                        {
                            DataDiases.Add(new DataDiases
                            {
                                Id = i,
                                Name = excelRange.Cells[i, colum_diases + 1].Value2.ToString(),
                                MCB10 = excelRange.Cells[i, colum_diases].Value2.ToString()
                            });
                        }
                    }
                    catch { }
                }
                if (excelRange.Cells[i, colum_population] != null)
                {
                    try
                    {
                        if (excelRange.Cells[i, colum_population].Value2.ToString() != "")
                        {
                            DataPopulation.Add(new DataPopulation
                            {
                                Id = Convert.ToInt32(excelRange.Cells[i, colum_population].Value2),
                                Name = excelRange.Cells[i, colum_population + 4].Value2.ToString(),
                                Excel = Convert.ToInt32(excelRange.Cells[i, colum_population + 1].Value2) == 1,
                                Start_Daly = Convert.ToInt32(excelRange.Cells[i, colum_population + 2].Value2),
                                Start_Daly_Bool = Convert.ToInt32(excelRange.Cells[i, colum_population + 2].Value2) != -1,
                                PeriodDied = Convert.ToDouble(excelRange.Cells[i, colum_population + 3].Value2),
                                WHO = Convert.ToDouble(excelRange.Cells[i, colum_population + 5].Value2)
                            });
                        }
                    }
                    catch { }
                }
            }

        }
        public int GetInfoDataItem(Excel.Worksheet ObjWorkSheet)
        {
            Excel.Range excelRange = ObjWorkSheet.UsedRange;
            string[] world = ObjWorkSheet.Name.Split(new char[] { '-' });
            int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count, region = Convert.ToInt32(world[0]), num = 0, row_year = 2;
            bool control = true, Male = excelRange.Cells[1, 1].Value2.IndexOf("муж") > -1;
            for (int i = 1; i <= rows; i++)
            {
                if (excelRange.Cells[i, 1] != null && excelRange.Cells[i, 1].Value2 != null)
                {
                    if (excelRange.Cells[i, 1].Value2.ToString() != "")
                    {
                        if (control == true)
                        {
                            control = false;
                            num++;
                            Male = excelRange.Cells[i, 1].Value2.ToString().IndexOf("муж") > -1;
                            i++;
                            row_year = i;
                        }
                        else
                        {
                            if (num == 5)
                            {
                                row_year--;
                                for (int j = 2; j <= colums; j++)
                                {
                                    int year = Convert.ToInt32(excelRange.Cells[row_year, j].Value2);
                                    try
                                    {
                                        DataVRP.Add(new DataVRP
                                        {
                                            Year = year,
                                            DataRegion_Id = region,
                                            VRP = Convert.ToDouble(excelRange.Cells[i + 1, j].Value2)
                                        });
                                    }
                                    catch { }
                                    List<DataSetDaly> data_dely = DataSetDaly.Where(u => u.Year == year && u.DataRegion_Id == region).ToList();
                                    if (data_dely.Any() == false)
                                    {
                                        DataSetDaly data_new_setdaly = new DataSetDaly
                                        {
                                            Id = id,
                                            Year = year,
                                            DataRegion_Id = region,
                                            MaleBirth = Convert.ToInt32(excelRange.Cells[i - 1, j].Value2),
                                            FemaleBirth = Convert.ToInt32(excelRange.Cells[i, j].Value2),
                                            DataSetDalyDiases = new List<DataSetDalyDiases>(),
                                            TrueResult = false
                                        };
                                        DataSetDaly.Add(data_new_setdaly);
                                        data_dely.Add(data_new_setdaly);
                                    }
                                    else
                                    {
                                        for (int k = 0; k < data_dely.Count(); k++)
                                        {
                                            data_dely[k].MaleBirth = Convert.ToInt32(excelRange.Cells[i - 1, j].Value2);
                                            data_dely[k].FemaleBirth = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                        }
                                    }
                                }
                                i++;
                            }
                            else
                            {

                                string NamePeriod = excelRange.Cells[i, 1].Value2.ToString();
                                DataPopulation population = DataPopulation.FirstOrDefault(u => u.Name == NamePeriod);
                                if (population != null)
                                {

                                    for (int j = 2; j <= colums; j++)
                                    {
                                        int year = Convert.ToInt32(excelRange.Cells[row_year, j].Value2);
                                        if (num > 1)
                                        {
                                            DataSetDaly data_dely = DataSetDaly.First(u => u.Year == year && u.DataPopulation_Id == population.Id && u.DataRegion_Id == region);
                                            if (num == 2)
                                                data_dely.FemaleLife = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                            else
                                            {
                                                if (Male == true)
                                                    data_dely.MaleDied = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                                else
                                                    data_dely.FemaleDied = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                            }

                                        }
                                        else
                                        {
                                            DataSetDaly.Add(new DataSetDaly
                                            {
                                                Id = id,
                                                Year = year,
                                                DataRegion_Id = region,
                                                DataPopulation_Id = population.Id,
                                                MaleLife = Convert.ToInt32(excelRange.Cells[i, j].Value2),
                                                TrueResult = true,
                                                DataSetDalyDiases = new List<DataSetDalyDiases>()
                                            });
                                        }
                                    }

                                }
                            }
                        }
                    }
                    else control = true;
                }
                else control = true;
            }
            return 1;
        }
        public void GetInfoData(Excel.Workbook excel_data, ProgressBar ProgressBar)
        {
            DataSetDaly = new List<DataSetDaly>();
            DataVRP = new List<DataVRP>();
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = 1;

            ProgressBar.Value++;
            GetInfo((Excel.Worksheet)excel_data.Sheets[1]);
            excel_data.Close(false, Type.Missing, Type.Missing);
            ProgressBar.Value = ProgressBar.Maximum;
        }
        public int GetInfoDataDiedItem(Excel.Worksheet ObjWorkSheet)
        {
            Excel.Range excelRange = ObjWorkSheet.UsedRange;
            string[] world = ObjWorkSheet.Name.Split(new char[] { '-' });
            int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count, region = Convert.ToInt32(world[0]), num = 0, row_year = 2;
            bool control = true, Male = excelRange.Cells[1, 1].Value2.IndexOf("муж") > -1;
            for (int i = 1; i <= rows; i++)
            {
                if (excelRange.Cells[i, 1] != null && excelRange.Cells[i, 1].Value2 != null)
                {
                    if (excelRange.Cells[i, 1].Value2.ToString() != "")
                    {
                        if (control == true)
                        {
                            control = false;
                            num++;
                            Male = excelRange.Cells[i, 1].Value2.ToString().IndexOf("муж") > -1;
                            i++;
                            row_year = i;
                            if (num > 2) break;
                        }
                        else
                        {
                            string NamePeriod = excelRange.Cells[i, 1].Value2.ToString();
                            DataPopulation population = DataPopulation.FirstOrDefault(u => u.Name == NamePeriod);
                            if (population != null)
                            {
                                for (int j = 2; j <= colums; j++)
                                {
                                    int year = Convert.ToInt32(excelRange.Cells[row_year, j].Value2);
                                    DataSetDaly data_dely = DataSetDaly.First(u => u.Year == year && u.DataPopulation_Id == population.Id && u.DataRegion_Id == region);
                                    if (Male == true)
                                        data_dely.MaleDied = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                    else
                                        data_dely.FemaleDied = Convert.ToInt32(excelRange.Cells[i, j].Value2);

                                }

                            }
                        }
                    }
                    else control = true;
                }
                else control = true;
            }
            return 1;
        }
        public void GetInfoDataDied(Excel.Workbook excel_data, ProgressBar ProgressBar)
        {
            List<Task> tasks1 = new List<Task>();
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = excel_data.Sheets.Count + 1;

            ProgressBar.Value++;
            for (int i = 1; i <= excel_data.Sheets.Count; i++)
            {
                Task<int> task = new Task<int>(() => GetInfoDataDiedItem((Excel.Worksheet)excel_data.Sheets[i]));
                tasks1.Add(task);
                task.Start();
                ProgressBar.Value += task.Result;
            }
            Task.WaitAll(tasks1.ToArray());
            ProgressBar.Value = ProgressBar.Maximum;
        }
        public int GetInfoDataPeopleItem(Excel.Worksheet ObjWorkSheet)
        {
            Excel.Range excelRange = ObjWorkSheet.UsedRange;
            string[] world = ObjWorkSheet.Name.Split(new char[] { '-' });
            int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count, region = Convert.ToInt32(world[0]), num = 0, row_year = 2;
            bool control = true, Male = excelRange.Cells[1, 1].Value2.IndexOf("муж") > -1;
            for (int i = 1; i <= rows; i++)
            {
                if (excelRange.Cells[i, 1] != null && excelRange.Cells[i, 1].Value2 != null)
                {
                    if (excelRange.Cells[i, 1].Value2.ToString() != "")
                    {
                        if (control)
                        {
                            control = false;
                            num++;
                            Male = excelRange.Cells[i, 1].Value2.ToString().IndexOf("муж") > -1;
                            i++;
                            row_year = i;
                            if (num > 2) break;
                        }
                        else
                        {
                            string NamePeriod = excelRange.Cells[i, 1].Value2.ToString();
                            DataPopulation population = DataPopulation.FirstOrDefault(u => u.Name == NamePeriod);
                            if (population != null)
                            {
                                for (int j = 2; j <= colums; j++)
                                {
                                    int year = Convert.ToInt32(excelRange.Cells[row_year, j].Value2);
                                    if (num == 2)
                                        DataSetDaly.First(u => u.Year == year && u.DataPopulation_Id == population.Id && u.DataRegion_Id == region).FemaleLife = Convert.ToInt32(excelRange.Cells[i, j].Value2);
                                    else
                                    {
                                        id++;
                                        DataSetDaly.Add(new DataSetDaly
                                        {
                                            Id = id,
                                            Year = year,
                                            DataRegion_Id = region,
                                            DataPopulation_Id = population.Id,
                                            MaleLife = Convert.ToInt32(excelRange.Cells[i, j].Value2),
                                            TrueResult = true,
                                            DataSetDalyDiases = new List<DataSetDalyDiases>()
                                        });
                                    }
                                }

                            }
                        }
                    }
                    else control = true;
                }
                else control = true;
            }
            return 1;
        }
        public void GetInfoDataPeople(Excel.Workbook excel_data, ProgressBar ProgressBar)
        {
            List<Task> tasks1 = new List<Task>();
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = excel_data.Sheets.Count + 1;

            ProgressBar.Value++;
            for (int i = 1; i <= excel_data.Sheets.Count; i++)
            {
                Task<int> task = new Task<int>(() => GetInfoDataPeopleItem((Excel.Worksheet)excel_data.Sheets[i]));
                tasks1.Add(task);
                task.Start();
                ProgressBar.Value += task.Result;
            }
            Task.WaitAll(tasks1.ToArray());
            ProgressBar.Value = ProgressBar.Maximum;
        }
        public int GetInfoDataVRPItem(Excel.Worksheet ObjWorkSheet)
        {
            Excel.Range excelRange = ObjWorkSheet.UsedRange;
            string[] world = ObjWorkSheet.Name.Split(new char[] { '-' });
            int colums = excelRange.Columns.Count, region = Convert.ToInt32(world[0]);
            for (int j = 2; j <= colums; j++)
            {
                //try
                //{
                int year = Convert.ToInt32(excelRange.Cells[1, j].Value2);
                DataVRP.Add(new DataVRP
                {
                    Year = year,
                    DataRegion_Id = region,
                    VRP = Convert.ToDouble(excelRange.Cells[2, j].Value2)
                });
                //}
                //catch { }
            }
            return 1;
        }
        public int GetInfoDataBirthItem(Excel.Worksheet ObjWorkSheet)
        {
            Excel.Range excelRange = ObjWorkSheet.UsedRange;
            string[] world = ObjWorkSheet.Name.Split(new char[] { '-' });
            int colums = excelRange.Columns.Count, region = Convert.ToInt32(world[0]);
            for (int j = 2; j <= colums; j++)
            {
                //try
                //{
                int year = Convert.ToInt32(excelRange.Cells[1, j].Value2);
                List<DataSetDaly> data_dely = DataSetDaly.Where(u => u.Year == year && u.DataRegion_Id == region).ToList();
                for (int k = 0; k < data_dely.Count; k++)
                {
                    data_dely[k].MaleBirth = Convert.ToInt32(excelRange.Cells[2, j].Value2);
                    data_dely[k].FemaleBirth = Convert.ToInt32(excelRange.Cells[3, j].Value2);
                }
                //}
                //catch { }
            }
            return 1;
        }
        public void GetInfoDataVRP(Excel.Workbook excel_data, ProgressBar ProgressBar)
        {
            List<Task> tasks1 = new List<Task>();
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = excel_data.Sheets.Count + 1;

            ProgressBar.Value++;
            for (int i = 1; i <= excel_data.Sheets.Count; i++)
            {
                Task<int> task = new Task<int>(() => GetInfoDataVRPItem((Excel.Worksheet)excel_data.Sheets[i]));
                tasks1.Add(task);
                task.Start();
                ProgressBar.Value += task.Result;
            }
            Task.WaitAll(tasks1.ToArray());
            excel_data.Close(false, Type.Missing, Type.Missing);
            ProgressBar.Value = ProgressBar.Maximum;
        }
        public void GetInfoDataBirth(Excel.Workbook excel_data, ProgressBar ProgressBar)
        {
            List<Task> tasks1 = new List<Task>();
            ProgressBar.Minimum = 0;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = excel_data.Sheets.Count + 1;
            ProgressBar.Value++;
            for (int i = 1; i <= excel_data.Sheets.Count; i++)
            {
                Task<int> task = new Task<int>(() => GetInfoDataBirthItem((Excel.Worksheet)excel_data.Sheets[i]));
                tasks1.Add(task);
                task.Start();
                ProgressBar.Value += task.Result;
            }
            Task.WaitAll(tasks1.ToArray());
            excel_data.Close(false, Type.Missing, Type.Missing);
            ProgressBar.Value = ProgressBar.Maximum;
        }
        public void GetSurvival(object obj)
        {
            ProgressBar ProgressBar = (ProgressBar)obj;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = DataSetDaly.Count() * 8 + 2;
            ProgressBar.Value++;
            DataFunction DataFunction = new DataFunction();
            DataSurvivalPeriod_0_year = new List<DataSurvivalPeriod_0_year>();
            DataSurvivalPeriod_20_year = new List<DataSurvivalPeriod_20_year>();
            DataSurvivalPeriod_70_year = new List<DataSurvivalPeriod_70_year>();

            DataSetDaly = DataSetDaly.OrderBy(u => u.DataRegion_Id).ThenBy(u => u.DataPopulation_Id).ThenBy(u => u.Year).ToList();
            ProgressBar.Value++;
            foreach (var items in DataSetDaly)
            {
                ProgressBar.Value++;
                if (items.TrueResult == true)
                {
                    double mx_male = DataFunction.GetSurvival_mx(items.MaleDied, items.MaleLife),
                        mx_female = DataFunction.GetSurvival_mx(items.FemaleDied, items.FemaleLife),
                        mx_summ = DataFunction.GetSurvival_mx(items.MaleDied + items.FemaleDied, items.MaleLife + items.FemaleLife);
                    foreach (var item in items.DataSetDalyDiases)
                    {
                        item.DataSurvivalMale = new DataSurvival
                        {
                            mx = mx_male,
                        };
                        item.DataSurvivalFemale = new DataSurvival
                        {
                            mx = mx_female
                        };
                        item.DataSurvivalSumm = new DataSurvival
                        {
                            mx = mx_summ
                        };
                        if (items.DataPopulation_Id >= 6 && items.DataPopulation_Id < 23)
                        {
                            try
                            {
                                (double, double, double) data_qx = DataFunction.GetSurvival_qx(items, item.DataDiases_Id);
                                item.DataSurvivalMale.qx = data_qx.Item1;
                                item.DataSurvivalFemale.qx = data_qx.Item2;
                                item.DataSurvivalSumm.qx = data_qx.Item3;
                            }
                            catch { }
                            item.DataSurvivalMale.px = DataFunction.GetSurvival_px(item.DataSurvivalMale.qx);
                            item.DataSurvivalFemale.px = DataFunction.GetSurvival_px(item.DataSurvivalFemale.qx);
                            item.DataSurvivalSumm.px = DataFunction.GetSurvival_px(item.DataSurvivalSumm.qx);
                        }
                        else if (items.DataPopulation_Id >= 23)
                        {
                            try
                            {
                                DataSetDaly data70 = DataSetDaly.First(u => u.Year == items.Year && u.DataPopulation_Id == 21 && u.DataRegion_Id == items.DataRegion_Id);
                                int diases_id = data70.DataSetDalyDiases.First(u => u.DataDiases_Id == item.DataDiases_Id).Id;
                                (double, double, double) data_px = DataFunction.GetSurvival_px_two(diases_id, items.DataPopulation_Id);
                                item.DataSurvivalMale.px = data_px.Item1;
                                item.DataSurvivalFemale.px = data_px.Item2;
                                item.DataSurvivalSumm.px = data_px.Item3;
                            }
                            catch { }
                            item.DataSurvivalMale.qx = DataFunction.GetSurvival_px(item.DataSurvivalMale.px);
                            item.DataSurvivalFemale.qx = DataFunction.GetSurvival_px(item.DataSurvivalFemale.px);
                            item.DataSurvivalSumm.qx = DataFunction.GetSurvival_px(item.DataSurvivalSumm.px);
                        }
                        if (items.DataPopulation_Id == 9)
                        {
                            DataSurvivalPeriod_20_year _DataSurvivalPeriod_20_year = new DataSurvivalPeriod_20_year
                            {
                                DataSetDalyDiases_Id = item.Id
                            };
                            DataSurvivalPeriod_20_year.Add(_DataSurvivalPeriod_20_year);
                        }
                        if (items.DataPopulation_Id == 21)
                        {
                            (double, double, double) px_year_60 = (0, 0, 0), px_year_65 = (0, 0, 0);
                            try
                            {
                                DataSetDaly data60 = DataSetDaly.First(u => u.Year == items.Year && u.DataPopulation_Id == 19 && u.DataRegion_Id == items.DataRegion_Id);
                                DataSetDalyDiases Diases_60_year = data60.DataSetDalyDiases.First(u => u.DataDiases_Id == item.DataDiases_Id);
                                px_year_60 = (Diases_60_year.DataSurvivalMale.px, Diases_60_year.DataSurvivalFemale.px, Diases_60_year.DataSurvivalSumm.px);
                            }
                            catch { }
                            try
                            {
                                DataSetDaly data65 = DataSetDaly.First(u => u.Year == items.Year && u.DataPopulation_Id == 20 && u.DataRegion_Id == items.DataRegion_Id);
                                DataSetDalyDiases Diases_65_year = data65.DataSetDalyDiases.First(u => u.DataDiases_Id == item.DataDiases_Id);
                                px_year_65 = (Diases_65_year.DataSurvivalMale.px, Diases_65_year.DataSurvivalFemale.px, Diases_65_year.DataSurvivalSumm.px);
                            }
                            catch { }
                            DataSurvivalPeriod_70_year _DataSurvivalPeriod_70_year = new DataSurvivalPeriod_70_year
                            {
                                DataSetDalyDiases_Id = item.Id,
                                male = new DataSurvivalPeriod_70_year_sex
                                {
                                    log_60_year = DataFunction.GetSurvival_log10(px_year_60.Item1),
                                    log_65_year = DataFunction.GetSurvival_log10(px_year_65.Item1),
                                    log_70_year = DataFunction.GetSurvival_log10(item.DataSurvivalMale.px)
                                },
                                female = new DataSurvivalPeriod_70_year_sex
                                {
                                    log_60_year = DataFunction.GetSurvival_log10(px_year_60.Item2),
                                    log_65_year = DataFunction.GetSurvival_log10(px_year_65.Item2),
                                    log_70_year = DataFunction.GetSurvival_log10(item.DataSurvivalFemale.px)
                                },
                                summ = new DataSurvivalPeriod_70_year_sex
                                {
                                    log_60_year = DataFunction.GetSurvival_log10(px_year_60.Item3),
                                    log_65_year = DataFunction.GetSurvival_log10(px_year_65.Item3),
                                    log_70_year = DataFunction.GetSurvival_log10(item.DataSurvivalSumm.px)
                                }
                            };
                            _DataSurvivalPeriod_70_year.male.c = DataFunction.GetSurvival_c(_DataSurvivalPeriod_70_year.male);
                            _DataSurvivalPeriod_70_year.female.c = DataFunction.GetSurvival_c(_DataSurvivalPeriod_70_year.female);
                            _DataSurvivalPeriod_70_year.summ.c = DataFunction.GetSurvival_c(_DataSurvivalPeriod_70_year.summ);

                            _DataSurvivalPeriod_70_year.male.b = DataFunction.GetSurvival_b(_DataSurvivalPeriod_70_year.male);
                            _DataSurvivalPeriod_70_year.female.b = DataFunction.GetSurvival_b(_DataSurvivalPeriod_70_year.female);
                            _DataSurvivalPeriod_70_year.summ.b = DataFunction.GetSurvival_b(_DataSurvivalPeriod_70_year.summ);

                            _DataSurvivalPeriod_70_year.male.a = DataFunction.GetSurvival_a(_DataSurvivalPeriod_70_year.male);
                            _DataSurvivalPeriod_70_year.female.a = DataFunction.GetSurvival_a(_DataSurvivalPeriod_70_year.female);
                            _DataSurvivalPeriod_70_year.summ.a = DataFunction.GetSurvival_a(_DataSurvivalPeriod_70_year.summ);

                            DataSurvivalPeriod_70_year.Add(_DataSurvivalPeriod_70_year);
                        }
                    }
                }
                ProgressBar.Value++;
            }

            foreach (var items in DataSetDaly)
            {
                ProgressBar.Value++;
                if (items.TrueResult == true)
                {
                    foreach (var item in items.DataSetDalyDiases)
                    {
                        if (items.DataPopulation_Id < 6)
                        {
                            try
                            {
                                (double, double, double) data_qx = DataFunction.GetSurvival_qx(items, item.DataDiases_Id);
                                item.DataSurvivalMale.qx = data_qx.Item1;
                                item.DataSurvivalFemale.qx = data_qx.Item2;
                                item.DataSurvivalSumm.qx = data_qx.Item3;
                            }
                            catch { }
                            item.DataSurvivalMale.px = DataFunction.GetSurvival_px(item.DataSurvivalMale.qx);
                            item.DataSurvivalFemale.px = DataFunction.GetSurvival_px(item.DataSurvivalFemale.qx);
                            item.DataSurvivalSumm.px = DataFunction.GetSurvival_px(item.DataSurvivalSumm.qx);
                        }
                        try
                        {
                            (double, double, double) data_l = DataFunction.GetSurvival_l(items, item.DataDiases_Id);
                            item.DataSurvivalMale.l = data_l.Item1;
                            item.DataSurvivalFemale.l = data_l.Item2;
                            item.DataSurvivalSumm.l = data_l.Item3;
                        }
                        catch { }
                        if (items.DataPopulation_Id <= 20)
                        {
                            item.DataSurvivalMale.d = DataFunction.GetSurvival_d(item.DataSurvivalMale.qx, item.DataSurvivalMale.l);
                            item.DataSurvivalFemale.d = DataFunction.GetSurvival_d(item.DataSurvivalFemale.qx, item.DataSurvivalFemale.l);
                            item.DataSurvivalSumm.d = DataFunction.GetSurvival_d(item.DataSurvivalSumm.qx, item.DataSurvivalSumm.l);
                        }
                    }
                }
                ProgressBar.Value++;
            }

            for (int i = DataSetDaly.Count - 1; i >= 0; i--)
            {
                ProgressBar.Value++;
                if (DataSetDaly[i].TrueResult == true)
                {
                    foreach (var item in DataSetDaly[i].DataSetDalyDiases)
                    {

                        try
                        {
                            (double, double, double) data_L = DataFunction.GetSurvival_L(DataSetDaly[i], item.DataDiases_Id);
                            item.DataSurvivalMale.L = data_L.Item1;
                            item.DataSurvivalFemale.L = data_L.Item2;
                            item.DataSurvivalSumm.L = data_L.Item3;
                        }
                        catch { }

                        if (DataSetDaly[i].DataPopulation_Id == 1)
                        {
                            DataSurvivalPeriod_0_year _DataSurvivalPeriod_0_year = new DataSurvivalPeriod_0_year
                            {
                                DataSetDalyDiases_Id = item.Id
                            };
                            try
                            {

                                (double, double, double) data_vx = DataFunction.GetSurvival_vx(DataSetDaly[i].DataRegion_Id, DataSetDaly[i].Year, item.DataDiases_Id);
                                _DataSurvivalPeriod_0_year.male_vx = data_vx.Item1;
                                _DataSurvivalPeriod_0_year.female_vx = data_vx.Item2;
                                _DataSurvivalPeriod_0_year.summ_vx = data_vx.Item3;
                            }
                            catch { }
                            DataSurvivalPeriod_0_year.Add(_DataSurvivalPeriod_0_year);
                        }

                        if (DataSetDaly[i].DataPopulation_Id > 20)
                        {
                            try
                            {

                                (double, double, double) data_d = DataFunction.GetSurvival_d_two(DataSetDaly[i], item.DataDiases_Id);
                                item.DataSurvivalMale.d = data_d.Item1;
                                item.DataSurvivalFemale.d = data_d.Item2;
                                item.DataSurvivalSumm.d = data_d.Item3;
                            }
                            catch { }
                        }
                    }
                }
                ProgressBar.Value++;
            }

            for (int i = DataSetDaly.Count - 1; i >= 0; i--)
            {
                ProgressBar.Value++;
                if (DataSetDaly[i].TrueResult == true)
                {
                    bool daly = DataPopulation.First(u => u.Id == DataSetDaly[i].DataPopulation_Id).Start_Daly_Bool;
                    foreach (var item in DataSetDaly[i].DataSetDalyDiases)
                    {
                        try
                        {
                            (double, double, double) data_T = DataFunction.GetSurvival_T(DataSetDaly[i], item.DataDiases_Id);
                            item.DataSurvivalMale.T = data_T.Item1;
                            item.DataSurvivalFemale.T = data_T.Item2;
                            item.DataSurvivalSumm.T = data_T.Item3;
                        }
                        catch { }

                        item.DataSurvivalMale.e0 = DataFunction.GetSurvival_e0(item.DataSurvivalMale.T, item.DataSurvivalMale.l);
                        item.DataSurvivalFemale.e0 = DataFunction.GetSurvival_e0(item.DataSurvivalFemale.T, item.DataSurvivalFemale.l);
                        item.DataSurvivalSumm.e0 = DataFunction.GetSurvival_e0(item.DataSurvivalSumm.T, item.DataSurvivalSumm.l);

                        item.DataSurvivalMale.mxl = DataFunction.GetSurvival_mx1(item.DataSurvivalMale.e0);
                        item.DataSurvivalFemale.mxl = DataFunction.GetSurvival_mx1(item.DataSurvivalFemale.e0);
                        item.DataSurvivalSumm.mxl = DataFunction.GetSurvival_mx1(item.DataSurvivalSumm.e0);

                        if (DataSetDaly[i].DataPopulation_Id == 9)
                        {
                            DataSurvivalPeriod_20_year _DataSurvivalPeriod_20_year = DataSurvivalPeriod_20_year.First(u => u.DataSetDalyDiases_Id == item.Id);
                            try
                            {
                                _DataSurvivalPeriod_20_year.male_ke0_20 = DataFunction.GetSurvival_ke0_20(DataSetDaly[i].DataRegion_Id, DataSetDaly[i].Year, item.DataDiases_Id, 0, item.DataSurvivalMale.T);
                                _DataSurvivalPeriod_20_year.male_ke_20 = DataFunction.GetSurvival_ke_20(_DataSurvivalPeriod_20_year.male_ke0_20 * 100000.0, item.DataSurvivalMale.l);
                                _DataSurvivalPeriod_20_year.male_F = DataFunction.GetSurvival_F(_DataSurvivalPeriod_20_year.male_ke0_20, _DataSurvivalPeriod_20_year.male_ke_20);
                            }
                            catch { }
                            try
                            {
                                _DataSurvivalPeriod_20_year.female_ke0_20 = DataFunction.GetSurvival_ke0_20(DataSetDaly[i].DataRegion_Id, DataSetDaly[i].Year, item.DataDiases_Id, 1, item.DataSurvivalFemale.T);
                                _DataSurvivalPeriod_20_year.female_ke_20 = DataFunction.GetSurvival_ke_20(_DataSurvivalPeriod_20_year.female_ke0_20 * 100000.0, item.DataSurvivalFemale.l);
                                _DataSurvivalPeriod_20_year.female_F = DataFunction.GetSurvival_F(_DataSurvivalPeriod_20_year.female_ke0_20, _DataSurvivalPeriod_20_year.female_ke_20);
                            }
                            catch { }
                        }
                        if (daly == true)
                        {
                            (double, double, double) data_e0_two = DataFunction.GetSurvival_e0_daly(DataSetDaly[i], item.DataDiases_Id);
                            item.DataSurvivalMale.e0_2 = data_e0_two.Item1;
                            item.DataSurvivalFemale.e0_2 = data_e0_two.Item2;
                            item.DataSurvivalSumm.e0_2 = data_e0_two.Item3;

                            try
                            {
                                (double, double, double) data_YLL = DataFunction.GetSurvival_YLL(DataSetDaly[i], item.DataDiases_Id);
                                item.DataSurvivalMale.YLL = data_YLL.Item1;
                                item.DataSurvivalFemale.YLL = data_YLL.Item2;
                                item.DataSurvivalSumm.YLL = data_YLL.Item3;

                                item.DataSurvivalMale.YLL100000 = DataFunction.GetSurvival_YLL100000(DataSetDaly[i].MaleLife, data_YLL.Item1);
                                item.DataSurvivalFemale.YLL100000 = DataFunction.GetSurvival_YLL100000(DataSetDaly[i].FemaleLife, data_YLL.Item2);
                                item.DataSurvivalSumm.YLL100000 = DataFunction.GetSurvival_YLL100000(DataSetDaly[i].MaleLife + DataSetDaly[i].FemaleLife, data_YLL.Item3);

                            }
                            catch { }
                            try
                            {
                                item.DataSurvivalMale.YLLWHO = DataFunction.GetSurvival_WHO(item.DataSurvivalMale.YLL, DataSetDaly[i].DataPopulation_Id);
                                item.DataSurvivalFemale.YLLWHO = DataFunction.GetSurvival_WHO(item.DataSurvivalFemale.YLL, DataSetDaly[i].DataPopulation_Id);
                                item.DataSurvivalSumm.YLLWHO = DataFunction.GetSurvival_WHO(item.DataSurvivalSumm.YLL, DataSetDaly[i].DataPopulation_Id);
                            }
                            catch { }
                            try
                            {
                                item.DataSurvivalMale.VRP = DataFunction.GetSurvival_VRP(item.DataSurvivalMale.YLL, DataSetDaly[i].Year, DataSetDaly[i].DataRegion_Id);
                                item.DataSurvivalFemale.VRP = DataFunction.GetSurvival_VRP(item.DataSurvivalFemale.YLL, DataSetDaly[i].Year, DataSetDaly[i].DataRegion_Id);
                                item.DataSurvivalSumm.VRP = DataFunction.GetSurvival_VRP(item.DataSurvivalSumm.YLL, DataSetDaly[i].Year, DataSetDaly[i].DataRegion_Id);
                            }
                            catch { }

                        }
                    }
                }
                ProgressBar.Value++;
            }

            ProgressBar.Value = ProgressBar.Maximum;
        }
        public void GetSurvivalMin(object obj)
        {
            ProgressBar ProgressBar = (ProgressBar)obj;
            ProgressBar.Value = 0;
            ProgressBar.Maximum = DataSetDaly.Count() * 8 + 2;
            ProgressBar.Value++;
            DataFunction DataFunction = new DataFunction();
            DataSurvivalPeriod_0_year = new List<DataSurvivalPeriod_0_year>();
            DataSurvivalPeriod_20_year = new List<DataSurvivalPeriod_20_year>();
            DataSurvivalPeriod_70_year = new List<DataSurvivalPeriod_70_year>();

            DataSetDaly = DataSetDaly.OrderBy(u => u.DataRegion_Id).ThenBy(u => u.DataPopulation_Id).ThenBy(u => u.Year).ToList();
            ProgressBar.Value++;

            for (int i = 0; i < DataSetDaly.Count; i++)
            {
                ProgressBar.Value++;
                if (DataSetDaly[i].TrueResult == true)
                {
                    bool daly = DataPopulation.First(u => u.Id == DataSetDaly[i].DataPopulation_Id).Start_Daly_Bool;
                    foreach (var item in DataSetDaly[i].DataSetDalyDiases)
                    {
                        try
                        {
                            item.DataSurvivalMale.mxl = DataFunction.GetSurvival_mx1(item.DataSurvivalMale.e0);
                            item.DataSurvivalFemale.mxl = DataFunction.GetSurvival_mx1(item.DataSurvivalFemale.e0);
                            item.DataSurvivalSumm.mxl = DataFunction.GetSurvival_mx1(item.DataSurvivalSumm.e0);

                            if (daly == true)
                            {
                                (double, double, double) data_e0_two = DataFunction.GetSurvival_e0_daly(DataSetDaly[i], item.DataDiases_Id);
                                item.DataSurvivalMale.e0_2 = data_e0_two.Item1;
                                item.DataSurvivalFemale.e0_2 = data_e0_two.Item2;
                                item.DataSurvivalSumm.e0_2 = data_e0_two.Item3;

                                try
                                {
                                    (double, double, double) data_YLL = DataFunction.GetSurvival_YLL(DataSetDaly[i], item.DataDiases_Id);
                                    item.DataSurvivalMale.YLL = data_YLL.Item1;
                                    item.DataSurvivalFemale.YLL = data_YLL.Item2;
                                    item.DataSurvivalSumm.YLL = data_YLL.Item3;

                                    item.DataSurvivalMale.YLL100000 = DataFunction.GetSurvival_YLL100000(DataSetDaly[i].MaleLife, data_YLL.Item1);
                                    item.DataSurvivalFemale.YLL100000 = DataFunction.GetSurvival_YLL100000(DataSetDaly[i].FemaleLife, data_YLL.Item2);
                                    item.DataSurvivalSumm.YLL100000 = DataFunction.GetSurvival_YLL100000(DataSetDaly[i].MaleLife + DataSetDaly[i].FemaleLife, data_YLL.Item3);
                                }
                                catch { }
                                try
                                {
                                    item.DataSurvivalMale.VRP = DataFunction.GetSurvival_VRP(item.DataSurvivalMale.YLL, DataSetDaly[i].Year, DataSetDaly[i].DataRegion_Id);
                                    item.DataSurvivalFemale.VRP = DataFunction.GetSurvival_VRP(item.DataSurvivalFemale.YLL, DataSetDaly[i].Year, DataSetDaly[i].DataRegion_Id);
                                    item.DataSurvivalSumm.VRP = DataFunction.GetSurvival_VRP(item.DataSurvivalSumm.YLL, DataSetDaly[i].Year, DataSetDaly[i].DataRegion_Id);
                                }
                                catch { }
                                try
                                {
                                    item.DataSurvivalMale.YLLWHO = DataFunction.GetSurvival_WHO(item.DataSurvivalMale.YLL, DataSetDaly[i].DataPopulation_Id);
                                    item.DataSurvivalFemale.YLLWHO = DataFunction.GetSurvival_WHO(item.DataSurvivalFemale.YLL, DataSetDaly[i].DataPopulation_Id);
                                    item.DataSurvivalSumm.YLLWHO = DataFunction.GetSurvival_WHO(item.DataSurvivalSumm.YLL, DataSetDaly[i].DataPopulation_Id);
                                }
                                catch { }

                            }
                        }
                        catch { }
                    }
                }
                ProgressBar.Value++;
            }

            ProgressBar.Value = ProgressBar.Maximum;
        }
    }

}
