using gyak7.Entities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyak7
{
    public partial class Form1 : Form
    {
        
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        List<Person> persons = new List<Person>();
        Random rng = new Random(1234);

        public List<Person> GetPopulation(string csvpath)
        {
            
            StreamReader sr = new StreamReader(csvpath, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(';');
                Person np = new Person();
                np.BirthYear = int.Parse(sor[0]);
                np.Gender = (Gender)Enum.Parse(typeof(Gender), sor[1]);
                np.Children = int.Parse(sor[2]);
                persons.Add(np);
            }
            sr.Close();
            return persons;
        }
        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            StreamReader sr = new StreamReader(csvpath, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(';');
                BirthProbability item = new BirthProbability();
                item.Age = int.Parse(sor[0]);
                item.Children = int.Parse(sor[1]);
                item.P = double.Parse(sor[2]);
                BirthProbabilities.Add(item);

            }
            sr.Close();

            return BirthProbabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            StreamReader sr = new StreamReader(csvpath, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(';');
                DeathProbability item = new DeathProbability();
                item.gender = (Gender)Enum.Parse(typeof(Gender), sor[0]);
                item.Age = int.Parse(sor[1]);
                item.P = double.Parse(sor[2]);
                DeathProbabilities.Add(item);

            }
            sr.Close();
            return DeathProbabilities;
        }
        public void SimStep(int aktev, Person aktszemely)
        {
            if (aktszemely.IsAlive == false) return;
            int kor = aktev - aktszemely.BirthYear;
            double szulval = (from x in BirthProbabilities
                              where x.Age == kor && x.Children == aktszemely.Children
                              select x.P).FirstOrDefault();


            if (rng.NextDouble()<=szulval)
            {
                Person newborn = new Person();
                newborn.Gender = (Gender)rng.Next(1,3);
                newborn.BirthYear = aktev;
                newborn.Children = 0;
                persons.Add(newborn);

            }
        }
        public Form1()
        {
            InitializeComponent();
            GetPopulation(@"C:\Temp\nép-teszt.csv");
            GetBirthProbabilities(@"C:\Temp\születés.csv");
            GetDeathProbabilities(@"C:\Temp\halál.csv");

            for (int i = 2005; i <= 2024; i++)
            {
                foreach (var item in persons)
                {
                    SimStep();
                }

                int ferfiakszama = (from x in persons
                                    where x.Gender == Gender.Male && x.IsAlive == true
                                    select x).Count();
                int nokszama = (from x in persons
                                    where x.Gender == Gender.Female && x.IsAlive == true
                                    select x).Count();
                Console.WriteLine(String.Format("Év: {0}, Fiúk:{1}, Lányok:{2}", i.ToString(), ferfiakszama.ToString(), nokszama.ToString()));
            }
        }
    }
}
