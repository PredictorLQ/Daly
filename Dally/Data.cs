using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Daly
{
    public class DataDalyConstant
    {
        public static double Discount_rate = 0.03;
        public static double Beta = 0.04;
        public static double Constant_C = 0.1658;
        public static double Constant_K = 1.0;
        public static double Constant_N = 1.0;
    }
    public class DataDaly
    {
        public static int SelectPaket { get; set; }
        public static string[] SelectPaketName { get; set; } = { "готовая ОПЖ", "расчетная ОПЖ" };
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
                row_start = Convert.ToInt32(excelRange.Cells[4, 2].Value2.ToString()), id = 1;
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
                                            if (population.Id > 19)
                                            {
                                                int kk = 0;
                                            }
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
                                            if (population.Id > 19)
                                            {
                                                int kk = 0;
                                            }
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
                                PeriodDied = Convert.ToDouble(excelRange.Cells[i, colum_population + 3].Value2)
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
            int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count, region = Convert.ToInt32(world[0]), num = 0, row_year = 2, id = 1;
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
            int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count, region = Convert.ToInt32(world[0]), num = 0, row_year = 2, id = 1;
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
            int rows = excelRange.Rows.Count, colums = excelRange.Columns.Count, region = Convert.ToInt32(world[0]), num = 0, row_year = 2, id = 1;
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
                        if (items.DataPopulation_Id >= 6 && items.DataPopulation_Id < 21)
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
                        else if (items.DataPopulation_Id >= 21)
                        {
                            try
                            {
                                DataSetDaly data70 = DataSetDaly.First(u => u.Year == items.Year && u.DataPopulation_Id == 19 && u.DataRegion_Id == items.DataRegion_Id);
                                int diases_id = data70.DataSetDalyDiases.First(u => u.DataDiases_Id == item.DataDiases_Id).Id;
                                (double, double, double) data_px = DataFunction.GetSurvival_px_two(diases_id, items.DataPopulation_Id);
                                item.DataSurvivalMale.px = data_px.Item1;
                                item.DataSurvivalFemale.px = data_px.Item2;
                                item.DataSurvivalSumm.px = data_px.Item3;
                            }
                            catch { }
                            item.DataSurvivalMale.qx = DataFunction.GetSurvival_px(item.DataSurvivalMale.px);
                            item.DataSurvivalFemale.qx = DataFunction.GetSurvival_px(item.DataSurvivalFemale.px);
                            item.DataSurvivalFemale.qx = DataFunction.GetSurvival_px(item.DataSurvivalSumm.px);
                        }
                        if (items.DataPopulation_Id == 9)
                        {
                            DataSurvivalPeriod_20_year _DataSurvivalPeriod_20_year = new DataSurvivalPeriod_20_year
                            {
                                DataSetDalyDiases_Id = item.Id
                            };
                            DataSurvivalPeriod_20_year.Add(_DataSurvivalPeriod_20_year);
                        }
                        if (items.DataPopulation_Id == 19)
                        {
                            (double, double, double) px_year_60 = (0, 0, 0), px_year_65 = (0, 0, 0);
                            try
                            {
                                DataSetDaly data60 = DataSetDaly.First(u => u.Year == items.Year && u.DataPopulation_Id == 17 && u.DataRegion_Id == items.DataRegion_Id);
                                DataSetDalyDiases Diases_60_year = data60.DataSetDalyDiases.First(u => u.DataDiases_Id == item.DataDiases_Id);
                                px_year_60 = (Diases_60_year.DataSurvivalMale.px, Diases_60_year.DataSurvivalFemale.px, Diases_60_year.DataSurvivalSumm.px);
                            }
                            catch { }
                            try
                            {
                                DataSetDaly data65 = DataSetDaly.First(u => u.Year == items.Year && u.DataPopulation_Id == 18 && u.DataRegion_Id == items.DataRegion_Id);
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
                        catch { }
                    }
                }
                ProgressBar.Value++;
            }

            ProgressBar.Value = ProgressBar.Maximum;
        }
    }
    public class DataFunction
    {
        //показатель смертности
        public double GetSurvival_mx(int died, int all_died)
        {
            if (all_died == 0)
                return 0;
            return (double)died / (double)all_died;
        }
        //вероятность умереть в данном возрасте
        public (double, double, double) GetSurvival_qx(DataSetDaly DataSetDaly, int diaes)
        {
            int year = DataSetDaly.Year,
                DataPopulation_Id = DataSetDaly.DataPopulation_Id;
            if (DataPopulation_Id == 20)
                return (1, 1, 1);
            if (DataPopulation_Id < 6)
            {
                DataSubFunction DataSubFunction = new DataSubFunction();
                DataSetDaly data2 = DataDaly.DataSetDaly.First(u => u.Year == DataSetDaly.Year + 1 && u.DataPopulation_Id == DataSetDaly.DataPopulation_Id && u.DataRegion_Id == DataSetDaly.DataRegion_Id);
                int data1_died_male = data2.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).MaleDied,
                    data1_died_female = data2.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).FemaleDied,
                    data_all_died_male = DataSetDaly.MaleDied + data1_died_male,
                    data_all_died_female = DataSetDaly.FemaleDied + data1_died_female;

                if (DataPopulation_Id == 1)
                {
                    (int, int) data_1_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 1, DataSetDaly.DataRegion_Id),
                        data1_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year + 1, DataSetDaly.DataRegion_Id);
                    int data_1_birth_male = data_1_birth.Item1, data_1_birth_female = data_1_birth.Item2,
                        data1_birth_male = data1_birth.Item1, data1_birth_female = data1_birth.Item2;
                    double znam_male = (double)1 / 3 * (double)data_1_birth_male + (double)DataSetDaly.MaleBirth + (double)2 / 3 * (double)data1_birth_male,
                        znam_emale = (double)1 / 3 * (double)data_1_birth_female + (double)DataSetDaly.FemaleBirth + (double)2 / 3 * (double)data1_birth_female,
                        znam_summ = (double)1 / 3 * (double)(data_1_birth_female + data_1_birth_male) + (double)(DataSetDaly.FemaleBirth + DataSetDaly.MaleBirth) + (double)2 / 3 * (double)(data1_birth_female + data1_birth_male);

                    return ((double)data_all_died_male / znam_male,
                        (double)data_all_died_female / znam_emale,
                        (double)(data_all_died_female + data_all_died_male) / znam_summ);
                }
                else
                {
                    DataSetDaly dataIn1 = DataDaly.DataSetDaly.First(u => u.Year == DataSetDaly.Year && u.DataPopulation_Id == 1 && u.DataRegion_Id == DataSetDaly.DataRegion_Id);
                    double data_p0_male = dataIn1.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalMale.px,
                        data_p0_female = dataIn1.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalFemale.px,
                        data_p0_summ = dataIn1.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalSumm.px;

                    if (DataPopulation_Id == 2)
                    {
                        (int, int) data_1_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 1, DataSetDaly.DataRegion_Id),
                        data_2_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 2, DataSetDaly.DataRegion_Id);

                        int data_1_birth_male = data_1_birth.Item1, data_1_birth_female = data_1_birth.Item2,
                           data_2_birth_male = data_2_birth.Item1, data_2_birth_female = data_2_birth.Item2;

                        return (data_p0_male * ((double)data_all_died_male / ((double)data_1_birth_male + 0.5 * (double)DataSetDaly.MaleBirth + 0.5 * (double)data_2_birth_male)),
                            (data_p0_female * (double)data_all_died_female / ((double)data_1_birth_female + 0.5 * (double)DataSetDaly.FemaleBirth + 0.5 * (double)data_2_birth_female)),
                            data_p0_summ * (double)(data_all_died_female + data_all_died_male) / ((double)(data_1_birth_male + data_1_birth_female) + 0.5 * (double)(DataSetDaly.MaleBirth + DataSetDaly.FemaleBirth) + 0.5 * (double)(data_2_birth_male + data_2_birth_female)));
                    }
                    else
                    {
                        DataSetDaly dataIn2 = DataDaly.DataSetDaly.First(u => u.Year == DataSetDaly.Year && u.DataPopulation_Id == 2 && u.DataRegion_Id == DataSetDaly.DataRegion_Id);
                        double data_p1_male = dataIn2.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalMale.px,
                            data_p1_female = dataIn2.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalFemale.px,
                            data_p1_summ = dataIn2.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalSumm.px;

                        if (DataPopulation_Id == 3)
                        {
                            (int, int) data_1_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 1, DataSetDaly.DataRegion_Id),
                            data_2_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 2, DataSetDaly.DataRegion_Id),
                            data_3_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 3, DataSetDaly.DataRegion_Id);

                            int data_1_birth_male = data_1_birth.Item1, data_1_birth_female = data_1_birth.Item2,
                               data_2_birth_male = data_2_birth.Item1, data_2_birth_female = data_2_birth.Item2,
                               data_3_birth_male = data_3_birth.Item1, data_3_birth_female = data_3_birth.Item2;

                            return (data_p0_male * data_p1_male * ((double)data_all_died_male / (0.5 * (double)data_1_birth_male + (double)data_2_birth_male + 0.5 * (double)data_3_birth_male)),
                               (data_p0_female * data_p1_female * (double)data_all_died_female / (0.5 * (double)data_1_birth_female + (double)data_2_birth_female + 0.5 * (double)data_3_birth_female)),
                               data_p0_summ * data_p1_summ * (double)(data_all_died_female + data_all_died_male) / (0.5 * (double)(data_1_birth_female + data_1_birth_male) + (double)(data_2_birth_female + data_2_birth_male) + 0.5 * (double)(data_3_birth_female + data_3_birth_male)));
                        }
                        else
                        {
                            DataSetDaly dataIn3 = DataDaly.DataSetDaly.First(u => u.Year == DataSetDaly.Year && u.DataPopulation_Id == 3 && u.DataRegion_Id == DataSetDaly.DataRegion_Id);
                            double data_p2_male = dataIn3.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalMale.px,
                                data_p2_female = dataIn3.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalFemale.px,
                                data_p2_summ = dataIn3.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalSumm.px;

                            if (DataPopulation_Id == 4)
                            {
                                (int, int) data_2_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 2, DataSetDaly.DataRegion_Id),
                                data_3_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 3, DataSetDaly.DataRegion_Id),
                                data_4_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 4, DataSetDaly.DataRegion_Id);

                                int data_2_birth_male = data_2_birth.Item1, data_2_birth_female = data_2_birth.Item2,
                                   data_3_birth_male = data_3_birth.Item1, data_3_birth_female = data_3_birth.Item2,
                                   data_4_birth_male = data_4_birth.Item1, data_4_birth_female = data_4_birth.Item2;

                                return (data_p0_male * data_p1_male * data_p2_male * ((double)data_all_died_male / (0.5 * (double)data_4_birth_male + (double)data_3_birth_male + 0.5 * (double)data_2_birth_male)),
                                   (data_p0_female * data_p1_female * data_p2_female * (double)data_all_died_female / (0.5 * (double)data_4_birth_female + (double)data_3_birth_female + 0.5 * (double)data_2_birth_female)),
                                   data_p0_summ * data_p1_summ * data_p2_summ * (double)(data_all_died_female + data_all_died_male) / (0.5 * (double)(data_4_birth_female + data_4_birth_male) + (double)(data_3_birth_female + data_3_birth_male) + 0.5 * (double)(data_2_birth_female + data_2_birth_male)));
                            }
                            else
                            {
                                DataSetDaly dataIn4 = DataDaly.DataSetDaly.First(u => u.Year == DataSetDaly.Year && u.DataPopulation_Id == 4 && u.DataRegion_Id == DataSetDaly.DataRegion_Id);
                                double data_p3_male = dataIn4.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalMale.px,
                                    data_p3_female = dataIn4.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalFemale.px,
                                    data_p3_summ = dataIn4.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalSumm.px;

                                (int, int) data_3_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 3, DataSetDaly.DataRegion_Id),
                                    data_4_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 4, DataSetDaly.DataRegion_Id),
                                    data_5_birth = DataSubFunction.GetCountBirth(DataSetDaly.Year - 5, DataSetDaly.DataRegion_Id);

                                int data_3_birth_male = data_3_birth.Item1, data_3_birth_female = data_3_birth.Item2,
                                   data_4_birth_male = data_4_birth.Item1, data_4_birth_female = data_4_birth.Item2,
                                   data_5_birth_male = data_5_birth.Item1, data_5_birth_female = data_5_birth.Item2;

                                return (data_p0_male * data_p1_male * data_p2_male * data_p3_male * ((double)data_all_died_male / (0.5 * (double)data_5_birth_male + (double)data_4_birth_male + 0.5 * (double)data_3_birth_male)),
                                   (data_p0_female * data_p1_female * data_p2_female * data_p3_female * (double)data_all_died_female / (0.5 * (double)data_5_birth_female + (double)data_4_birth_female + 0.5 * (double)data_3_birth_female)),
                                   data_p0_summ * data_p1_summ * data_p2_summ * data_p3_summ * (double)(data_all_died_female + data_all_died_male) / (0.5 * (double)(data_5_birth_female + data_5_birth_male) + (double)(data_4_birth_female + data_4_birth_male) + 0.5 * (double)(data_3_birth_female + data_3_birth_male)));

                            }

                        }
                    }
                }
            }
            double data_mx_male = 0, data_mx_female = 0, data_mx_summ = 0;
            try { data_mx_male = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalMale.mx; }
            catch { }
            try { data_mx_female = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalFemale.mx; }
            catch { }
            try { data_mx_summ = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes).DataSurvivalSumm.mx; }
            catch { }
            return (1.0 - Math.Pow(Math.E, -5 * data_mx_male), 1.0 - Math.Pow(Math.E, -5 * data_mx_female), 1.0 - Math.Pow(Math.E, -5 * data_mx_summ));
        }
        //число доживающих до данного возраста (на 100000 родившихся);
        public (double, double, double) GetSurvival_l(DataSetDaly DataSetDaly, int diaes)
        {
            if (DataSetDaly.DataPopulation_Id == 1)
                return (100000, 100000, 100000);
            DataSetDaly data = DataDaly.DataSetDaly.First(u => u.DataPopulation_Id == DataSetDaly.DataPopulation_Id - 1 && u.Year == DataSetDaly.Year && u.DataRegion_Id == DataSetDaly.DataRegion_Id);
            DataSetDalyDiases DataSetDalyDiases = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diaes);
            if (DataSetDaly.DataPopulation_Id == 21)
                return (DataSetDalyDiases.DataSurvivalMale.l, DataSetDalyDiases.DataSurvivalFemale.l, DataSetDalyDiases.DataSurvivalSumm.l);
            double data_male = DataSetDalyDiases.DataSurvivalMale.px * DataSetDalyDiases.DataSurvivalMale.l,
                data_female = DataSetDalyDiases.DataSurvivalFemale.px * DataSetDalyDiases.DataSurvivalFemale.l,
                data_summ = DataSetDalyDiases.DataSurvivalSumm.px * DataSetDalyDiases.DataSurvivalSumm.l;
            return (data_male, data_female, data_summ);
        }
        //вероятность умереть в данном возрасте
        public double GetSurvival_px(double GetSurvival_qx_val)
        {
            return 1.0 - GetSurvival_qx_val;
        }
        //вероятность умереть в данном возрасте возраст больше 75 лет
        public (double, double, double) GetSurvival_px_two(int diases_id, int population_id)
        {
            DataSurvivalPeriod_70_year data = DataDaly.DataSurvivalPeriod_70_year.First(u => u.DataSetDalyDiases_Id == diases_id);
            int[] arr_population = { 21, 22, 23, 24 };
            int coeff = Array.IndexOf(arr_population, population_id);
            if (coeff == -1)
                return (0, 0, 0);
            coeff += 3;
            return (Math.Pow(10, (data.male.a + data.male.b * Math.Pow(data.male.c, coeff))),
                Math.Pow(10, (data.female.a + data.female.b * Math.Pow(data.female.c, coeff))),
                Math.Pow(10, (data.summ.a + data.summ.b * Math.Pow(data.summ.c, coeff))));
        }
        //число умерших в данном возрасте
        public double GetSurvival_d(double GetSurvival_qx, double GetSurvival_l)
        {
            return GetSurvival_qx * GetSurvival_l;
        }
        //число умерших в данном возрасте старше 75 лет
        public (double, double, double) GetSurvival_d_two(DataSetDaly DataSetDaly, int diases)
        {
            int period = DataSetDaly.DataPopulation_Id, period_next = period + 1;
            double data_male = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.l, data_female = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.l;
            DataSetDaly data = DataDaly.DataSetDaly.Where(u => u.DataPopulation_Id == period_next && u.DataRegion_Id == DataSetDaly.DataRegion_Id && u.Year == DataSetDaly.Year).FirstOrDefault();
            if (data == null)
                return (0, 0, 0);
            double data_male_two = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.l, data_female_two = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.l;
            return (data_male - data_male_two, data_female - data_female_two, (data_male + data_female) - (data_female_two + data_male_two));
        }
        //ожидаемая продолжительность жизни
        public double GetSurvival_e0(double GetSurvival_T_val, double GetSurvival_I_l)
        {
            if (GetSurvival_I_l == 0)
                return 0;
            return GetSurvival_T_val / GetSurvival_I_l;
        }
        //показывает число человеко-лет, которое прожито совокупностью родившихся в течении любого интервала при данных уровнях смертности  
        public (double, double, double) GetSurvival_L(DataSetDaly DataSetDaly, int diases)
        {
            int period = DataSetDaly.DataPopulation_Id, period_next = period + 1;
            double data_male = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.l,
                data_female = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.l,
                data_summ = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.l;
            if (period == 20)
                return (data_male, data_female, data_summ);
            if (period == 25)
                return (3.5 * data_male, 3.5 * data_female, 3.5 * data_summ);
            DataSetDaly data = DataDaly.DataSetDaly.Where(u => u.DataPopulation_Id == period_next && u.DataRegion_Id == DataSetDaly.DataRegion_Id && u.Year == DataSetDaly.Year).FirstOrDefault();
            if (data == null)
                return (0, 0, 0);
            double data_male_two = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.l,
                data_female_two = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.l,
                data_summ_two = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.l;
            if (period == 1)
                return (0.35 * data_male + 0.65 * data_male_two,
                    0.35 * data_female + 0.65 * data_female_two,
                    0.35 * data_summ + 0.65 * data_summ_two);
            if (period < 6)
                return (0.5 * (data_male + data_male_two),
                    0.5 * (data_female + data_female_two),
                    0.5 * (data_summ + data_summ_two));
            return (2.5 * (data_male + data_male_two),
                2.5 * (data_female + data_female_two),
                2.5 * (data_summ + data_summ_two));
        }
        //общее число человеко-лет, которое предстоит прожить от текущего возраста до предельного (на 100000 родившихся)
        public (double, double, double) GetSurvival_T(DataSetDaly DataSetDaly, int diases)
        {
            int period = DataSetDaly.DataPopulation_Id, period_next = period + 1;
            double data_l_male = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.L,
                data_l_female = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.L,
                data_l_summ = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.L;

            if (period == 25)
                return (data_l_male, data_l_female, data_l_summ);
            DataSetDaly data = DataDaly.DataSetDaly.Where(u => u.DataPopulation_Id == period_next && u.DataRegion_Id == DataSetDaly.DataRegion_Id && u.Year == DataSetDaly.Year).FirstOrDefault();
            if (data == null)
                return (0, 0, 0);
            double data_male_two = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.T,
                data_female_two = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.T,
                data_summ_two = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.T;
            if (period == 20)
                return (data_male_two, data_female_two, data_summ_two);
            return ((data_l_male + data_male_two), (data_l_female + data_female_two), data_l_summ + data_summ_two);
        }
        //табличный коэффициент смертности
        public double GetSurvival_mx1(double GetSurvival_e0_val)
        {
            if (GetSurvival_e0_val == 0)
                return 0;
            return 1000.0 / GetSurvival_e0_val;
        }
        //вероятная продолжительность жизни для новорожденных
        public (double, double, double) GetSurvival_vx(int region, int year, int diases)
        {
            DataSetDaly data_period_17 = DataDaly.DataSetDaly.Where(u => u.DataPopulation_Id == 17 && u.DataRegion_Id == region && u.Year == year).FirstOrDefault(),
                data_period_18 = DataDaly.DataSetDaly.Where(u => u.DataPopulation_Id == 18 && u.DataRegion_Id == region && u.Year == year).FirstOrDefault();
            double data_l17_male = data_period_17.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.l,
                data_l17_female = data_period_17.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.l,
                data_l17_summ = data_period_17.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.l,
                data_l18_male = data_period_18.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.l,
                data_l18_female = data_period_18.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.l,
                data_l18_summ = data_period_18.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.l;

            return ((60.0 + 5.0 * (data_l17_male - 50000.0) * (data_l17_male - data_l18_male)),
                (60.0 + 5.0 * (data_l17_female - 50000.0) * (data_l17_female - data_l18_female)),
                (60.0 + 5.0 * (data_l17_summ - 50000.0) * (data_l17_summ - data_l18_summ)));
        }
        //отсроченная временная средней продолжительности жизни новорожденного в трудоспособном возрасте 
        public double GetSurvival_ke0_20(int region, int year, int diases, int sex, double T)
        {
            int period = 16;
            double data_T = 0.0;
            if (sex == 1)
                period = 15;
            DataSetDaly data_period = DataDaly.DataSetDaly.Where(u => u.DataPopulation_Id == period && u.DataRegion_Id == region && u.Year == year).FirstOrDefault();
            if (sex == 1)
                data_T = data_period.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.T;
            else
                data_T = data_period.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.T;

            return (T - data_T) / 100000.0;
        }
        //средняя продолжительность предстоящего периода трудоспособности
        public double GetSurvival_ke_20(double T, double data_l)
        {
            if (data_l == 0)
                return 0;
            return T / data_l;
        }
        //количество лет трудовой жизни, которое в среднем теряет одно лицо из-за смертей в детском возрасте
        public double GetSurvival_F(double GetSurvival_ke0_20_val, double GetSurvival_ke_20_val)
        {
            return GetSurvival_ke0_20_val - GetSurvival_ke_20_val;
        }
        public double GetSurvival_log10(double GetSurvival_px_val)
        {
            return Math.Log10(GetSurvival_px_val);
        }
        public double GetSurvival_c(DataSurvivalPeriod_70_year_sex data)
        {
            try
            {
                return (data.log_70_year - data.log_65_year) / (data.log_65_year - data.log_60_year);
            }
            catch
            {
                return 0;
            }
        }
        public double GetSurvival_b(DataSurvivalPeriod_70_year_sex data)
        {
            try
            {
                return (data.log_65_year - data.log_60_year) / (data.c - 1.0);
            }
            catch
            {
                return 0;
            }
        }
        public double GetSurvival_a(DataSurvivalPeriod_70_year_sex data)
        {
            return data.log_60_year - data.b;
        }
        //Ожидаемая продолжительность жизни
        public (double, double, double) GetSurvival_e0_daly(DataSetDaly DataSetDaly, int diases)
        {
            int period = DataSetDaly.DataPopulation_Id, period_next = period + 1;
            if (period == 1)
                period_next = 6;
            if (period == 20)
                period_next = 22;
            DataSetDaly data = DataDaly.DataSetDaly.FirstOrDefault(u => u.DataPopulation_Id == period_next && u.DataRegion_Id == DataSetDaly.DataRegion_Id && u.Year == DataSetDaly.Year);
            if (data == null)
                return (0, 0, 0);
            DataPopulation popul_period = DataDaly.DataPopulation.First(u => u.Id == DataSetDaly.DataPopulation_Id),
                popul_period_next = DataDaly.DataPopulation.First(u => u.Id == period_next);

            double male_period = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.e0,
                female_period = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.e0,
                summ_period = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.e0,
                male_period_next = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.e0,
                female_period_next = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.e0,
                summ_period_next = data.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.e0;

            double period_daly = popul_period.PeriodDied - popul_period.Start_Daly,
                start_daly = popul_period_next.Start_Daly - popul_period.Start_Daly;
            if (period_next == 22)
                start_daly = 5;
            double data_e0_male = male_period + period_daly * (male_period_next - male_period) / start_daly,
                data_e0_female = female_period + period_daly * (female_period_next - female_period) / start_daly,
                data_e0_summ = summ_period + period_daly * (summ_period_next - summ_period) / start_daly;
            if (period_next == 22)
                return (data_e0_male, data_e0_female, data_e0_summ);

            return (data_e0_male, data_e0_female, data_e0_summ);
        }
        public (double, double, double) GetSurvival_YLL(DataSetDaly DataSetDaly, int diases)
        {
            DataSetDalyDiases d_diases = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases);
            DataPopulation popul = DataDaly.DataPopulation.First(u => u.Id == DataSetDaly.DataPopulation_Id);
            double K = DataDalyConstant.Constant_K, C = DataDalyConstant.Constant_C, r = DataDalyConstant.Discount_rate,
                beta = DataDalyConstant.Beta, e = Math.E, rpb = r + beta, rmb = r - beta, a = popul.PeriodDied,
                N = DataDalyConstant.Constant_N,
                L_m = d_diases.DataSurvivalMale.e0_2,
                L_f = d_diases.DataSurvivalFemale.e0_2,
                L_s = d_diases.DataSurvivalSumm.e0_2,
                coef_1 = K * C * Math.Pow(e, r * a) / Math.Pow(rpb, 2),
                coef_2 = Math.Pow(e, -1.0 * rpb * a) * (-1.0 * rpb * a - 1.0),
                coef_3 = (1.0 - K) / r;

            double male = d_diases.MaleDied * N * (coef_1 * (Math.Pow(e, -1.0 * rpb * (L_m + a))
                * (-1.0 * rmb * (L_m + a) - 1.0) - coef_2) + coef_3 * (1.0 - Math.Pow(e, -1.0 * r * L_m))),

                female = d_diases.FemaleDied * N * (coef_1 * (Math.Pow(e, -1.0 * rpb * (L_f + a))
                * (-1.0 * rmb * (L_f + a) - 1.0) - coef_2) + coef_3 * (1.0 - Math.Pow(e, -1.0 * r * L_f))),

                summ = (d_diases.MaleDied + d_diases.FemaleDied) * N * (coef_1 * (Math.Pow(e, -1.0 * rpb * (L_s + a))
                * (-1.0 * rmb * (L_s + a) - 1.0) - coef_2) + coef_3 * (1.0 - Math.Pow(e, -1.0 * r * L_s)));

            return (male, female, summ);
        }
        //расчет экономического ущерба
        public double GetSurvival_VRP(double GetSurvival_YLL_val, int year, int region)
        {
            double vrp = DataDaly.DataVRP.First(u => u.DataRegion_Id == region && u.Year == year).VRP;
            return GetSurvival_YLL_val * vrp;
        }
    }
    public class DataSubFunction
    {
        public (int, int) GetCountBirth(int year, int region_id)
        {
            DataSetDaly DataSetDaly = DataDaly.DataSetDaly.First(u => u.Year == year && u.DataRegion_Id == region_id);
            return (DataSetDaly.MaleBirth, DataSetDaly.FemaleBirth);
        }
    }
    public class DataRegion
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class DataPopulation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Excel { get; set; }
        public bool Start_Daly_Bool { get; set; }
        public int Start_Daly { get; set; }
        public double PeriodDied { get; set; }
    }
    public class DataDiases
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MCB10 { get; set; }
    }
    public class DataVRP
    {
        public int DataRegion_Id { get; set; }
        public int Year { get; set; }
        public double VRP { get; set; }
    }
    public class DataSetDaly
    {
        public int Id { get; set; }
        public int DataPopulation_Id { get; set; }
        public int DataRegion_Id { get; set; }
        public int Year { get; set; }
        public int MaleLife { get; set; }
        public int FemaleLife { get; set; }
        public int MaleBirth { get; set; }
        public int FemaleBirth { get; set; }
        public int MaleDied { get; set; }
        public int FemaleDied { get; set; }
        public bool TrueResult { get; set; }
        public List<DataSetDalyDiases> DataSetDalyDiases { get; set; }
    }
    public class DataSetDalyDiases
    {
        public int Id { get; set; }
        public int DataDiases_Id { get; set; }
        public int MalePain { get; set; }
        public int FemalePain { get; set; }
        public int MaleDied { get; set; }
        public int FemaleDied { get; set; }
        public DataSurvival DataSurvivalMale { get; set; } = new DataSurvival();
        public DataSurvival DataSurvivalFemale { get; set; } = new DataSurvival();
        public DataSurvival DataSurvivalSumm { get; set; } = new DataSurvival();
    }
    public class DataSurvival
    {
        public double mx { get; set; }
        public double L { get; set; }
        public double d { get; set; }
        public double qx { get; set; }
        public double px { get; set; }
        public double l { get; set; }
        public double T { get; set; }
        public double e0 { get; set; }
        public double mxl { get; set; }
        public double e0_2 { get; set; }
        public double YLL { get; set; }
        public double VRP { get; set; }
    }
    public class DataSurvivalPeriod_0_year
    {
        public int DataSetDalyDiases_Id { get; set; }
        public double male_vx { get; set; }
        public double female_vx { get; set; }
        public double summ_vx { get; set; }
    }
    public class DataSurvivalPeriod_20_year
    {
        public int DataSetDalyDiases_Id { get; set; }
        public double male_ke0_20 { get; set; }
        public double male_ke_20 { get; set; }
        public double male_F { get; set; }
        public double male_A { get; set; }
        public double female_ke0_20 { get; set; }
        public double female_ke_20 { get; set; }
        public double female_F { get; set; }
        public double female_A { get; set; }
    }
    public class DataSurvivalPeriod_70_year
    {
        public int DataSetDalyDiases_Id { get; set; }
        public DataSurvivalPeriod_70_year_sex male { get; set; }
        public DataSurvivalPeriod_70_year_sex female { get; set; }
        public DataSurvivalPeriod_70_year_sex summ { get; set; } = new DataSurvivalPeriod_70_year_sex();
    }
    public class DataSurvivalPeriod_70_year_sex
    {
        public double log_60_year { get; set; }
        public double log_65_year { get; set; }
        public double log_70_year { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double c { get; set; }
    }
}
