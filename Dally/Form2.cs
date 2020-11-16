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
            List<DataSetDaly> DataSetDaly = DataDaly.DataSetDaly.Where(u => u.Year == DataDaly.ActivDataYear_Id && u.DataRegion_Id == DataDaly.ActivDataRegion_Id).ToList();
            (double, double) vx = (0, 0), ke0_20 = (0, 0), ke_20 = (0, 0), F = (0, 0);
            foreach (var item in DataDaly.DataPopulation)
            {
                DataSetDaly data = DataSetDaly.First(u => u.DataPopulation_Id == item.Id);
                DataSetDalyDiases diases = data.DataSetDalyDiases.First(u => u.DataDiases_Id == DataDaly.ActivDataDiases_Id);
                dataGridView1.Rows.Add(item.Name, diases.DataSurvivalMale.mx,
                    diases.DataSurvivalMale.qx, diases.DataSurvivalMale.px, diases.DataSurvivalMale.l,
                    diases.DataSurvivalMale.d, diases.DataSurvivalMale.L, diases.DataSurvivalMale.T, diases.DataSurvivalMale.e0,
                    diases.DataSurvivalMale.mxl);

                dataGridView2.Rows.Add(diases.Id, item.Name, diases.DataSurvivalFemale.mx,
                    diases.DataSurvivalFemale.qx, diases.DataSurvivalFemale.px, diases.DataSurvivalFemale.l,
                    diases.DataSurvivalFemale.d, diases.DataSurvivalFemale.L, diases.DataSurvivalFemale.T, diases.DataSurvivalFemale.e0,
                     diases.DataSurvivalFemale.mxl);

                if (item.Id == 1)
                {
                    DataSurvivalPeriod_0_year DataSurvivalPeriod_0_year = DataDaly.DataSurvivalPeriod_0_year.First(u => u.DataSetDalyDiases_Id == diases.Id);
                    vx.Item1 = DataSurvivalPeriod_0_year.male_vx;
                    vx.Item2 = DataSurvivalPeriod_0_year.female_vx;
                }
                if (item.Id == 9)
                {
                    DataSurvivalPeriod_20_year DataSurvivalPeriod_20_year = DataDaly.DataSurvivalPeriod_20_year.First(u => u.DataSetDalyDiases_Id == diases.Id);
                    ke0_20.Item1 = DataSurvivalPeriod_20_year.male_ke0_20;
                    ke0_20.Item2 = DataSurvivalPeriod_20_year.female_ke0_20;
                    ke_20.Item1 = DataSurvivalPeriod_20_year.male_ke_20;
                    ke_20.Item2 = DataSurvivalPeriod_20_year.female_ke_20;
                    F.Item1 = DataSurvivalPeriod_20_year.male_F;
                    F.Item2 = DataSurvivalPeriod_20_year.female_F;
                }
                if(item.Id == 19)
                {
                    DataSurvivalPeriod_70_year data_70 = DataDaly.DataSurvivalPeriod_70_year.First(u => u.DataSetDalyDiases_Id == diases.Id);

                    dataGridView5.Rows.Add(data_70.male.c, data_70.male.b, data_70.male.a);
                    dataGridView6.Rows.Add(data_70.female.c, data_70.female.b, data_70.female.a);
                }
            }
            dataGridView3.Rows.Add(vx.Item1, ke0_20.Item1, ke_20.Item1, F.Item1);
            dataGridView4.Rows.Add(vx.Item2, ke0_20.Item2, ke_20.Item2, F.Item2);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

    }
}
