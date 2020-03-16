using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Zoo.Animals;

namespace Zoo.Services
{
    public class AnimalService : IAnimalService
    {
        public List<Animal> animals;
        public List<string> animalNames;

        public AnimalService()
        {
            animals = new List<Animal>();
            animals.AddRange(new List<Animal> { new Monkey("ad"), new Elephant("michiel"), new Lion("Maurice") } );
            animalNames = GetAnimalTypeNames();
        }

        public List<Animal> GetAnimals()
        {
            return animals;
        }

        public void UseEnergy()
        {
            foreach(Animal animal in animals)
            {
                animal.UseEnergy();
            }          
        }

        public void AddAnimal<T>(T animal) where T : Animal
        {
            animals.Add(animal);
        }

        public List<string> GetAnimalTypeNames()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Animal)));
            List<string> names = new List<string>();
            foreach (Type type in types)
            {
                names.Add(type.Name);
            }

            return names;
        }

        public void FeedAnimals()
        {
            throw new NotImplementedException();
        }
    }
}
