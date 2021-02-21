﻿using DDDInPractice.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDInPractice.Logic
{
    public class Slot : Entity
    {
      
        public virtual SnackPile SnackPile { get; set; }
        public virtual SnackMachine SnackMachine { get; protected set; }
        public virtual int Position { get; protected set; }

        protected Slot()
        {
        }

        public Slot(SnackMachine snackMachine, int position)
            : this()
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = SnackPile.Empty;
        }
    }
}
