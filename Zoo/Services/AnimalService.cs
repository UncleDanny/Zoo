using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            animals.AddRange(new List<Animal> { new Monkey("ad", Gender.Male), new Elephant("michiel", Gender.Male), new Lion("Maurice", Gender.Male), new Monkey("Meintje", Gender.Female) });
            animalNames = GetAnimalTypeNames();
        }

        public List<Animal> GetAnimals()
        {
            return animals;
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

        public void AddAnimal<T>(T animal) where T : Animal
        {
            if (string.IsNullOrEmpty(animal.Name) || animals.Where(a => a.Name == animal.Name).Any())
            {
                return;
            }

            animals.Add(animal);
        }

        public void AnimalDeath(Animal animal)
        {
            animals.Remove(animal);
        }

        public void Reset()
        {
            animals = new List<Animal>();
            animals.AddRange(new List<Animal> { new Monkey("ad", Gender.Male), new Elephant("michiel", Gender.Male), new Lion("Maurice", Gender.Male), new Monkey("Meintje", Gender.Female) });
            animalNames = GetAnimalTypeNames();
        }

        public void FeedAnimals(Type type)
        {
            IEnumerable<Animal> animalsOfType = animals.Where(x => x.GetType() == type);
            foreach (Animal animal in animalsOfType)
            {
                animal.Eat();
            }
        }

        public List<Tuple<string, string, string, string>> BreedAnimals()
        {
            var tuples = new List<Tuple<string, string, string, string>>();

            if (!animals.Any())
            {
                return null;
            }

            foreach (string stype in GetAnimalTypeNames())
            {
                Type type = Type.GetType($"Zoo.Animals.{stype}");
                var grouptuples = BreedAnimalGroup(type);
                if (tuples != null)
                {
                    foreach(var tuple in grouptuples)
                    {
                        tuples.Add(tuple);
                    }
                }
            }

            return tuples;

        }

        public List<Tuple<string, string, string, string>> BreedAnimalGroup(Type type)
        {
            IEnumerable<Animal> breedableAnimals = GetAnimals().Where(x => x.GetType() == type && !x.HadKid);
            var tuples = new List<Tuple<string,string, string, string>>();
            if (breedableAnimals.Count() >= 2)
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
                            var tuple = new Tuple<string, string, string, string>(type.Name, parentOne.Name, parentTwo.Name, child.Name);
                            tuples.Add(tuple);
                        }

                        breedableAnimals = breedableAnimals.Where(x => !x.HadKid);
                        if (breedableAnimals.Count() < 2)
                        {
                            return null;
                        }
                    }
                }
            }

            return tuples;
        }



        public Tuple<bool, string> UseEnergy()
        {
            if (!animals.Any()) return new Tuple<bool, string>(true, "");

            foreach (Animal animal in animals)
            {
                bool energy = animal.UseEnergy();
                if (!energy)
                {
                    Debug.WriteLine("Animal " + animal.Name + " has died!");
                    AnimalDeath(animal);
                    return new Tuple<bool, string>(true, animal.Name);
                }
            }

            return new Tuple<bool, string>(false, "");
        }
    }
}
