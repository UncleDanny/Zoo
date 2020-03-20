namespace Zoo.Animals
{
    public class Elephant : Animal
    {
        public override int MaxEnergy => 10000;

        public override int EnergyConsumptionRate => 5;

        public Elephant(string name) : base()
        {
            Name = name;
        }
        public Elephant(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
            CurrentEnergy = 100;
        }

        public override void Eat()
        {
            CurrentEnergy += 1250;
        }   
    }
}
