using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Zoo.Animals;

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
            Gender gender = (Gender)int.Parse(animal.Gender);
            _animalService.AddAnimal((Animal)Activator.CreateInstance(type, name, gender));

            await Clients.All.Refresh();
        }

        public async Task FeedAnimal(string animalType)
        {
            Type type = Type.GetType($"Zoo.Animals.{animalType}");
            _animalService.FeedAnimals(type);

            await Clients.All.Refresh();
        }

        public async Task BreedAnimal()
        {
            _animalService.BreedAnimals();
            await Clients.All.Refresh();
        }

        public async Task Refresh(string refresh = "Refresh")
        {
            await Clients.All.Refresh(refresh);
        }
    }
}
