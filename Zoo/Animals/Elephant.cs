using System;

namespace Zoo.Animals
{
    public class Elephant : Animal
    {
        public override int MaxEnergy => 10000;

        public Elephant(string name)
        {
            Name = name;
            CurrentEnergy = 10;
            Gender = new Random().Next(2) == 0 ? "Male" : "Female";
        }

        public override void Eat()
        {
            CurrentEnergy += 1250;
        }

        public override void UseEnergy()
        {
            CurrentEnergy -= 5;
        }
    }
}
