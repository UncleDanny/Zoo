using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Animals;


namespace Zoo.Services
{
    public class ChatHub : Hub
    {
        private IAnimalService _animalService;
        public ChatHub(IAnimalService animalService)
        {
            _animalService = animalService;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddAnimal(AnimalDto animal)
        {
            var name = animal.Name;
            if (name is null)
            {
                return;
            }

            Type type = Type.GetType($"Zoo.Animals.{animal.Type}");
            _animalService.AddAnimal((Animal)Activator.CreateInstance(type, name));

            await Clients.All.SendAsync("Refresh");
        }

        public async Task FeedAnimal(string a)
        {
            Type type = Type.GetType($"Zoo.Animals.{a}");
            IEnumerable animals = _animalService.GetAnimals().Where(x => x.GetType() == type);
            foreach (Animal animal in animals)
            {
                animal.Eat();
            }
            await Clients.All.SendAsync("Refresh");
        }


    }
}
