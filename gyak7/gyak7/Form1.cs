using gyak7.Entities;
using System;
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
            return persons;
        }
        public Form1()
        {
            InitializeComponent();
        }
    }
}
