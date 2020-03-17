namespace Zoo.Animals
{
    public class Monkey : Animal
    {
        public override int MaxEnergy => 2000;

        public override int EnergyConsumptionRate => 2;

        public Monkey(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
            CurrentEnergy = 50;
        }

        public override void Eat()
        {
            CurrentEnergy += 250;
        }
    }
}
