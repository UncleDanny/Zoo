using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zoo.Utilities;

namespace Zoo.Animals
{
    public abstract class Animal
    {
        private int energy;
        private bool pregnant;

        private Random random;
        public Animal()
        {
            random = new Random();
            Gender = (Gender)random.Next(2);
            Family = new List<Animal>();
            Infertile = random.NextBool(10);
        }

        public string Name { get; set; }

        public List<Animal> Family { get; }

        public Gender Gender { get; }

        public bool Infertile { get; }

        public abstract int MaxEnergy { get; }

        [Display(Name = "Current Energy")]
        public int CurrentEnergy
        {
            get => energy;
            set => energy = Math.Clamp(value, 0, MaxEnergy);
        }

        public bool Pregnant
        {
            get => pregnant;
            set => pregnant = Gender == Gender.Male ? false : value;
        }

        public abstract void Eat();

        public abstract void UseEnergy();

        public bool FamilyOf(Animal other)
        {
            return other.Family.Contains(this) && Family.Contains(other);
        }

        public bool CanBreed(Animal other)
        {
            return other.Gender != Gender && CurrentEnergy >= (int)(MaxEnergy / 2f) && !FamilyOf(other);
        }

        public Animal Breed(Animal other)
        {
            if (!CanBreed(other))
            {
                return null;
            }

            Animal animal = (Animal)Activator.CreateInstance(GetType(), other.Name + other.Name);
            Family.AddRange(other.Family);
            other.Family.AddRange(Family);

            Pregnant = true;
            other.Pregnant = true;

            return animal;
        }
    }

    public class AnimalObject
    {
        public string Name { get; set; }

        public string Type { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
    }
}
