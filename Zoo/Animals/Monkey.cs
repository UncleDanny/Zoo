namespace Zoo.Animals
{
    public class Monkey : Animal
    {
        public override int MaxEnergy => 2000;

        public Monkey(string name)
        {
            Name = name;
            CurrentEnergy = 20;
        }

        public override void Eat()
        {
            CurrentEnergy += 250;
        }

        public override void UseEnergy()
        {
            CurrentEnergy -= 2;
        }
    }
}
