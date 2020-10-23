﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyak7.Entities
{
    class Person
    {
        public int BirthYear { get; set; }
        public Gender Gender { get; set; }
        public int Children { get; set; }
        public bool IsAlive { get; set; }
        public Person()
        {
            IsAlive = true;
        }
    }
    
}
