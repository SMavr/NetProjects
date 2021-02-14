﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDInPractice.Logic
{
    public class Snack : AggragateRoot
    {
        public virtual string Name { get; protected set; }

        protected Snack()
        {
        }

        public Snack(string name)
        {
            Name = name;
        }
    }
}