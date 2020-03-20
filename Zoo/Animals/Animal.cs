using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zoo.Utilities;

namespace Zoo.Animals
{
    public abstract class Animal
    {
        private int energy;
        private readonly Random random;

        public Animal()
        {
            random = new Random();
            Gender = (Gender)random.Next(2);
            Family = new List<Animal>();
        }

      

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public bool HadKid { get; set; }

        public abstract int EnergyConsumptionRate { get;  }

        public List<Animal> Family { get; }

        public abstract int MaxEnergy { get; }

        [Display(Name = "Current Energy")]
        public int CurrentEnergy
        {
            get => energy;
            set => energy = Math.Clamp(value, 0, MaxEnergy);
        }

        public abstract void Eat();

        public bool UseEnergy()
        {
            if (CurrentEnergy == 0)
            {
                return false;
            }

            CurrentEnergy -= EnergyConsumptionRate;
            return true;
        }

        public bool FamilyOf(Animal other)
        {
            return other.Family.Contains(this) && Family.Contains(other);
        }

        public bool CanBreed(Animal other)
        {
            return CurrentEnergy >= (int)(MaxEnergy / 2f) && !FamilyOf(other);
        }

        public Animal Breed(Animal other)
        {
            if (!CanBreed(other))
            {
                return null;
            }

            Animal animal = (Animal)Activator.CreateInstance(GetType(), Name + other.Name);
            Family.AddRange(other.Family);
            other.Family.AddRange(Family);
            animal.Family.AddMultiple(this, other);
            animal.Family.AddRanges(Family, other.Family);

            HadKid = true;
            other.HadKid = true;

            return animal;
        }
    }

    public class AnimalObject
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Gender { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
    }
}
