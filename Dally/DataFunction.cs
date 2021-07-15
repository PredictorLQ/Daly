using Daly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daly
{

    public class DataFunction
    {
        //показатель смертности
        public double GetSurvival_mx(int died, int all_died) => all_died == 0 ? 0 : (double)died / (double)all_died;
        //вероятность умереть в данном возрасте
        public (double, double, double) GetSurvival_qx(DataSetDaly DataSetDaly, int diaes)
        {
            int year = DataSetDaly.Year,
                DataPopulation_Id = DataSetDaly.DataPopulation_Id;
            if (DataPopulation_Id == 22)
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
            if (DataSetDaly.DataPopulation_Id == 23)
                return (DataSetDalyDiases.DataSurvivalMale.l, DataSetDalyDiases.DataSurvivalFemale.l, DataSetDalyDiases.DataSurvivalSumm.l);
            double data_male = DataSetDalyDiases.DataSurvivalMale.px * DataSetDalyDiases.DataSurvivalMale.l,
                data_female = DataSetDalyDiases.DataSurvivalFemale.px * DataSetDalyDiases.DataSurvivalFemale.l,
                data_summ = DataSetDalyDiases.DataSurvivalSumm.px * DataSetDalyDiases.DataSurvivalSumm.l;
            return (data_male, data_female, data_summ);
        }
        //вероятность умереть в данном возрасте
        public double GetSurvival_px(double GetSurvival_qx_val) => 1.0 - GetSurvival_qx_val;
        //вероятность умереть в данном возрасте возраст больше 85 лет
        public (double, double, double) GetSurvival_px_two(int diases_id, int population_id)
        {
            DataSurvivalPeriod_70_year data = DataDaly.DataSurvivalPeriod_70_year.First(u => u.DataSetDalyDiases_Id == diases_id);
            int[] arr_population = { 23, 24, 25 };
            int coeff = Array.IndexOf(arr_population, population_id);
            if (coeff == -1)
                return (0, 0, 0);
            coeff += 3;
            return (Math.Pow(10, (data.male.a + data.male.b * Math.Pow(data.male.c, coeff))),
                Math.Pow(10, (data.female.a + data.female.b * Math.Pow(data.female.c, coeff))),
                Math.Pow(10, (data.summ.a + data.summ.b * Math.Pow(data.summ.c, coeff))));
        }
        //число умерших в данном возрасте
        public double GetSurvival_d(double GetSurvival_qx, double GetSurvival_l) => GetSurvival_qx * GetSurvival_l;
        //число умерших в данном возрасте старше 85 лет
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
        public double GetSurvival_e0(double GetSurvival_T_val, double GetSurvival_I_l) => GetSurvival_I_l == 0 || GetSurvival_T_val ==0? 0 : GetSurvival_T_val / GetSurvival_I_l;
        //показывает число человеко-лет, которое прожито совокупностью родившихся в течении любого интервала при данных уровнях смертности  
        public (double, double, double) GetSurvival_L(DataSetDaly DataSetDaly, int diases)
        {
            int period = DataSetDaly.DataPopulation_Id, period_next = period + 1;
            double data_male = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalMale.l,
                data_female = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalFemale.l,
                data_summ = DataSetDaly.DataSetDalyDiases.First(u => u.DataDiases_Id == diases).DataSurvivalSumm.l;
            if (period == 22)
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
            if (period == 22)
                return (data_male_two, data_female_two, data_summ_two);
            return ((data_l_male + data_male_two), (data_l_female + data_female_two), data_l_summ + data_summ_two);
        }
        //табличный коэффициент смертности
        public double GetSurvival_mx1(double GetSurvival_e0_val) => GetSurvival_e0_val == 0 ? 0 : 1000.0 / GetSurvival_e0_val;
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
        public double GetSurvival_ke_20(double T, double data_l) => data_l == 0 ? 0 : T / data_l;
        //количество лет трудовой жизни, которое в среднем теряет одно лицо из-за смертей в детском возрасте
        public double GetSurvival_F(double GetSurvival_ke0_20_val, double GetSurvival_ke_20_val) => GetSurvival_ke0_20_val - GetSurvival_ke_20_val;
        public double GetSurvival_log10(double GetSurvival_px_val) => Math.Log10(GetSurvival_px_val);
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
        public double GetSurvival_a(DataSurvivalPeriod_70_year_sex data) => data.log_60_year - data.b;
        //Ожидаемая продолжительность жизни
        public (double, double, double) GetSurvival_e0_daly(DataSetDaly DataSetDaly, int diases)
        {
            int period = DataSetDaly.DataPopulation_Id, period_next = period + 1;
            if (period == 1)
                period_next = 6;
            if (period == 22)
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
                beta = DataDalyConstant.Beta, e = Math.E, rpb = r + beta, a = popul.PeriodDied,
                N = DataDalyConstant.Constant_N,
                L_m = d_diases.DataSurvivalMale.e0_2,
                L_f = d_diases.DataSurvivalFemale.e0_2,
                L_s = d_diases.DataSurvivalSumm.e0_2,
                coef_1 = K * C * Math.Pow(e, r * a) / Math.Pow(rpb, 2),
                coef_2 = Math.Pow(e, -1.0 * rpb * a) * (-1.0 * rpb * a - 1.0),
                coef_3 = (1.0 - K) / r;

            double male = d_diases.MaleDied * N * (coef_1 * (Math.Pow(e, -1.0 * rpb * (L_m + a))
                * (-1.0 * rpb * (L_m + a) - 1.0) - coef_2) + coef_3 * (1.0 - Math.Pow(e, -1.0 * r * L_m))),

                female = d_diases.FemaleDied * N * (coef_1 * (Math.Pow(e, -1.0 * rpb * (L_f + a))
                * (-1.0 * rpb * (L_f + a) - 1.0) - coef_2) + coef_3 * (1.0 - Math.Pow(e, -1.0 * r * L_f))),

                summ = (d_diases.MaleDied + d_diases.FemaleDied) * N * (coef_1 * (Math.Pow(e, -1.0 * rpb * (L_s + a))
                * (-1.0 * rpb * (L_s + a) - 1.0) - coef_2) + coef_3 * (1.0 - Math.Pow(e, -1.0 * r * L_s)));

            return (male, female, summ);
        }
        public double GetSurvival_YLL100000(double LifeAll, double YLL) => LifeAll == 0 ? 0 : 100000.0 * YLL / (double)LifeAll;
        //расчет экономического ущерба
        public double GetSurvival_VRP(double GetSurvival_YLL_val, int year, int region) => GetSurvival_YLL_val * DataDaly.DataVRP.First(u => u.DataRegion_Id == region && u.Year == year).VRP;
        //расчет WHO
        public double GetSurvival_WHO(double GetSurvival_YLL_val, int DataPopulation_Id) => DataDaly.DataPopulation.First(u => u.Id == DataPopulation_Id).WHO * GetSurvival_YLL_val / 100.0;
    }
    public class DataSubFunction
    {
        public (int, int) GetCountBirth(int year, int region_id)
        {
            DataSetDaly DataSetDaly = DataDaly.DataSetDaly.First(u => u.Year == year && u.DataRegion_Id == region_id);
            return (DataSetDaly.MaleBirth, DataSetDaly.FemaleBirth);
        }
    }
}
