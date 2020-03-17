﻿using System;
using System.Collections.Generic;

namespace Zoo.Animals
{
    public class Lion : Animal
    {
        public override int MaxEnergy => 5000;

        public override int EnergyConsumptionRate => 10;

        public Lion(string name)
        {
            Name = name;
            CurrentEnergy = 100;
        }

        public override void Eat()
        {
            CurrentEnergy += 625;
        }

       
    }
}
