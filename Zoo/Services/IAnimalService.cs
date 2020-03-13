using System.Collections.Generic;
using Zoo.Animals;

namespace Zoo.Services
{
    public interface IAnimalService
    {
        public List<Animal> GetAnimals();

        public void AddAnimal<T>(T animal) where T : Animal;

        public List<string> GetAnimalTypeNames();


    }
}