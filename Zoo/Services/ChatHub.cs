using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Animals;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace Zoo.Services
{
    public class ChatHub : Hub<IChatHub>
    {
        private IAnimalService _animalService;
        public ChatHub(IAnimalService animalService)
        {
            _animalService = animalService;
        }
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        public async Task AddAnimal(AnimalDto animal)
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
            await Clients.All.Refresh();
        }

        public async Task Refresh(string refresh = "Refresh")
        {
            await Clients.All.Refresh(refresh);
        }
    }
}
