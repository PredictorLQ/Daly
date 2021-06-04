using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daly
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            dataGridView6.Rows.Clear();
            label8.Text = $"( {DataDaly.SelectPaketName[DataDaly.SelectPaket - 1]} )";
            label8.Visible = true;
            List<DataSetDaly> DataSetDaly = DataDaly.DataSetDaly.Where(u => DataDaly.ActivDataYear_Id.Any(t => t == u.Year) == true
            && DataDaly.ActivDataRegion_Id.Any(t => t == u.DataRegion_Id) == true).ToList();
            (double, double, double) vx = (0, 0, 0), ke0_20 = (0, 0, 0), ke_20 = (0, 0, 0), F = (0, 0, 0);
            foreach (var item in DataDaly.DataPopulation)
            {
                List<DataSetDalyDiases> diases = new List<DataSetDalyDiases>();
                List<DataSetDaly> data = DataSetDaly.Where(u => u.DataPopulation_Id == item.Id).ToList();
                for (int i = 0; i < data.Count; i++)
                {
                    List<DataSetDalyDiases> DataSetDalyDiases = data[i].DataSetDalyDiases.Where(u => DataDaly.ActivDataDiases_Id.Any(t => t == u.DataDiases_Id)).ToList();
                    diases.AddRange(DataSetDalyDiases);

                    if (item.Id == 1)
                    {
                        List<DataSurvivalPeriod_0_year> DataSurvivalPeriod_0_year = DataDaly.DataSurvivalPeriod_0_year.Where(u => DataSetDalyDiases.Any(t => t.Id == u.DataSetDalyDiases_Id) == true).ToList();
                        vx.Item1 = DataSurvivalPeriod_0_year.Average(t => t.male_vx);
                        vx.Item2 = DataSurvivalPeriod_0_year.Average(t => t.female_vx);
                    }
                    if (item.Id == 9)
                    {
                        List<DataSurvivalPeriod_20_year> DataSurvivalPeriod_20_year = DataDaly.DataSurvivalPeriod_20_year.Where(u => DataSetDalyDiases.Any(t => t.Id == u.DataSetDalyDiases_Id) == true).ToList();
                        ke0_20.Item1 = DataSurvivalPeriod_20_year.Average(t => t.male_ke0_20);
                        ke0_20.Item2 = DataSurvivalPeriod_20_year.Average(t => t.female_ke0_20);
                        ke_20.Item1 = DataSurvivalPeriod_20_year.Average(t => t.male_ke_20);
                        ke_20.Item2 = DataSurvivalPeriod_20_year.Average(t => t.female_ke_20);
                        ke_20.Item2 = DataSurvivalPeriod_20_year.Average(t => t.female_ke_20);
                        F.Item1 = DataSurvivalPeriod_20_year.Average(t => t.male_F);
                        F.Item2 = DataSurvivalPeriod_20_year.Average(t => t.female_F);
                    }
                    if (item.Id == 21)
                    {
                        List<DataSurvivalPeriod_70_year> data_70 = DataDaly.DataSurvivalPeriod_70_year.Where(u => DataSetDalyDiases.Any(t => t.Id == u.DataSetDalyDiases_Id) == true).ToList();

                        dataGridView5.Rows.Add(data_70.Average(t => t.male.c), data_70.Average(t => t.male.b), data_70.Average(t => t.male.a));
                        dataGridView6.Rows.Add(data_70.Average(t => t.female.c), data_70.Average(t => t.female.b), data_70.Average(t => t.female.a));
                    }
                }
                dataGridView1.Rows.Add(item.Name, diases.Average(t => t.DataSurvivalMale.mx),
                   diases.Average(t => t.DataSurvivalMale.qx), diases.Average(t => t.DataSurvivalMale.px), diases.Average(t => t.DataSurvivalMale.l),
                   diases.Average(t => t.DataSurvivalMale.d), diases.Average(t => t.DataSurvivalMale.L), diases.Average(t => t.DataSurvivalMale.T), diases.Average(t => t.DataSurvivalMale.e0),
                   diases.Average(t => t.DataSurvivalMale.mxl));

                dataGridView2.Rows.Add(item.Id + " " + item.Name, diases.Average(t => t.DataSurvivalFemale.mx),
                     diases.Average(t => t.DataSurvivalFemale.qx), diases.Average(t => t.DataSurvivalFemale.px), diases.Average(t => t.DataSurvivalFemale.l),
                    diases.Average(t => t.DataSurvivalFemale.d), diases.Average(t => t.DataSurvivalFemale.L), diases.Average(t => t.DataSurvivalFemale.T), diases.Average(t => t.DataSurvivalFemale.e0),
                    diases.Average(t => t.DataSurvivalFemale.mxl));

                dataGridView8.Rows.Add(item.Name, diases.Average(t => t.DataSurvivalSumm.mx),
                     diases.Average(t => t.DataSurvivalSumm.qx), diases.Average(t => t.DataSurvivalSumm.px), diases.Average(t => t.DataSurvivalSumm.l),
                    diases.Average(t => t.DataSurvivalSumm.d), diases.Average(t => t.DataSurvivalSumm.L), diases.Average(t => t.DataSurvivalSumm.T), diases.Average(t => t.DataSurvivalSumm.e0),
                    diases.Average(t => t.DataSurvivalSumm.mxl));
            }
            dataGridView3.Rows.Add(vx.Item1, ke0_20.Item1, ke_20.Item1, F.Item1);
            dataGridView4.Rows.Add(vx.Item2, ke0_20.Item2, ke_20.Item2, F.Item2);
            List<int> max_count = new List<int>{
            DataDaly.ActivDataYear_Id.Count(),
            DataDaly.ActivDataRegion_Id.Count(),
            DataDaly.ActivDataDiases_Id.Count()
            };
            int max = max_count.Max();
            for (int i = 0; i < max; i++)
            {
                (string, string, string) elem = ("", "", "");
                try
                {
                    DataDiases DataDiases = DataDaly.DataDiases.First(u => u.Id == DataDaly.ActivDataDiases_Id[i]);
                    elem.Item1 = $"{DataDiases.MCB10} {DataDiases.Name}";
                }
                catch { }
                try
                {
                    elem.Item2 = DataDaly.DataRegion.First(u => u.Id == DataDaly.ActivDataRegion_Id[i]).Name;
                }
                catch { }
                try
                {
                    elem.Item3 = DataDaly.ActivDataYear_Id[i].ToString();
                }
                catch { }
                dataGridView7.Rows.Add(elem.Item1, elem.Item2, elem.Item3);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
