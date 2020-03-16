using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Zoo.Animals;
using Zoo.Utilities;

namespace Zoo.Services
{
    public class ChatHub : Hub<IChatHub>
    {
        private IAnimalService _animalService;

        public ChatHub(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public async Task AddAnimal(AnimalObject animal)
        {
            var name = animal.Name;
            if (name is null)
            {
                return;
            }

            Type type = Type.GetType($"Zoo.Animals.{animal.Type}");
            _animalService.AddAnimal((Animal)Activator.CreateInstance(type, name));

            await Clients.All.Refresh();
        }

        public async Task FeedAnimal(string animalType)
        {
            Type type = Type.GetType($"Zoo.Animals.{animalType}");
            IEnumerable animals = _animalService.GetAnimals().Where(x => x.GetType() == type);
            foreach (Animal animal in animals)
            {
                animal.Eat();
            }
            await Breed();

            await Clients.All.Refresh();
        }

        public async Task Refresh(string refresh = "Refresh")
        {
            await Clients.All.Refresh(refresh);
        }

        public async Task Breed()
        {
            void CheckBreed(ref IEnumerable<Animal> animals)
            {
                for (int i = 0; i < animals.Count() - 1; i++)
                {
                    for (int j = i + 1; j < animals.Count(); j++)
                    {
                        Animal parentOne = animals.ElementAt(i);
                        Animal parentTwo = animals.ElementAt(j);

                        Animal child = parentOne.Breed(parentTwo);
                        if (!(child is null))
                        {
                            child.Family.AddMultiple(parentOne, parentTwo);
                            child.Family.AddRanges(parentOne.Family, parentTwo.Family);
                            _animalService.AddAnimal(child);
                        }

                        animals = animals.Where(x => !x.HadKid);
                        if (animals.Count() < 2)
                        {
                            return;
                        }
                    }
                }
            }

            foreach (string stype in _animalService.GetAnimalTypeNames())
            {
                Type type = Type.GetType($"Zoo.Animals.{stype}");
                IEnumerable<Animal> animals = _animalService.GetAnimals().Where(x => x.GetType() == type && !x.HadKid);
                if (animals.Count() > 2)
                {
                    CheckBreed(ref animals);
                }
            }

            await Clients.All.Refresh();
        }
    }
}
