namespace Zoo.Animals
{
    public class Monkey : Animal
    {
        public override int MaxEnergy => 2000;

        public Monkey(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
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
