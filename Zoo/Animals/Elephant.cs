namespace Zoo.Animals
{
    public class Elephant : Animal
    {
        public override int MaxEnergy => 10000;

        public Elephant(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
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
