namespace Zoo.Animals
{
    public class Elephant : Animal
    {
        public override int MaxEnergy => 10000;

        public Elephant(string name)
        {
            Name = name;
            CurrentEnergy = 10;
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
