using System;

namespace Zoo.Animals
{
    public class Lion : Animal
    {
        public override int MaxEnergy => 5000;

        public Lion(string name)
        {
            Name = name;
            CurrentEnergy = 10;
            Gender = new Random().Next(2) == 0 ? "Male" : "Female";
        }

        public override void Eat()
        {
            CurrentEnergy += 625;
        }

        public override void UseEnergy()
        {
            CurrentEnergy -= 10;
        }
    }
}
