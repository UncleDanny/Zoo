﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Animals
{
    public abstract class Animal
    {
        private int energy;

        public string Name { get; set; }

        public string Gender { get; set; }

        public abstract int MaxEnergy { get; }

        [Display(Name = "Current Energy")]
        public int CurrentEnergy
        {
            get => energy;
            set => energy = Math.Clamp(value, 0, MaxEnergy);
        }

        public abstract void Eat();

        public abstract void UseEnergy();

        public bool CanBreed(Animal other)
        {
            return other.Gender != Gender && other.GetType() == GetType();
        }
    }

    public class AnimalObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
