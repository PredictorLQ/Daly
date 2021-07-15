using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daly
{
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
        public double WHO { get; set; }
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
        public double YLL100000 { get; set; }
        public double YLLWHO { get; set; }
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
