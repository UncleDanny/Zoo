namespace Zoo.Animals
{
    public class Monkey : Animal
    {
        public override int MaxEnergy => 2000;

        public override int EnergyConsumptionRate => 2;

        public Monkey(string name)
        {
            Name = name;
            CurrentEnergy = 200;
        }

        public override void Eat()
        {
            CurrentEnergy += 250;
        }

     
    }
}
