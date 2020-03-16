using System;
using System.Collections.Generic;
using Zoo.Animals;

namespace Zoo.Services
{
    public interface IAnimalService
    {
        public List<Animal> GetAnimals();

        public List<string> GetAnimalTypeNames();

        public void AddAnimal<T>(T animal) where T : Animal;

        public void FeedAnimals(Type type);

        public void BreedAnimals();

        public void UseEnergy();
    }
}