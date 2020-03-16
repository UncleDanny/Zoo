using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Zoo.Animals;
using Zoo.Utilities;

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

        public void FeedAnimals(Type type)
        {
            IEnumerable<Animal> animalsOfType = animals.Where(x => x.GetType() == type);
            foreach (Animal animal in animalsOfType)
            {
                animal.Eat();
            }
        }

        public void BreedAnimals()
        {
            void TryBreed(ref IEnumerable<Animal> breedableAnimals)
            {
                for (int i = 0; i < breedableAnimals.Count() - 1; i++)
                {
                    for (int j = i + 1; j < breedableAnimals.Count(); j++)
                    {
                        Animal parentOne = breedableAnimals.ElementAt(i);
                        Animal parentTwo = breedableAnimals.ElementAt(j);

                        Animal child = parentOne.Breed(parentTwo);
                        if (!(child is null))
                        {
                            AddAnimal(child);
                        }

                        breedableAnimals = breedableAnimals.Where(x => !x.HadKid);
                        if (breedableAnimals.Count() < 2)
                        {
                            return;
                        }
                    }
                }
            }

            foreach (string stype in GetAnimalTypeNames())
            {
                Type type = Type.GetType($"Zoo.Animals.{stype}");
                IEnumerable<Animal> breedableAnimals = GetAnimals().Where(x => x.GetType() == type && !x.HadKid);
                if (breedableAnimals.Count() > 2)
                {
                    TryBreed(ref breedableAnimals);
                }
            }
        }
    }
}
