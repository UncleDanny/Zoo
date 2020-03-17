using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zoo.Services
{
    public class Worker : BackgroundService
    {
        private IHubContext<AnimalHub,IAnimalHub> _hub;
        private IAnimalService _animalService;
        
        public Worker(IHubContext<AnimalHub,IAnimalHub> hub,IAnimalService animalService)
        {
            _hub = hub;
            _animalService = animalService;
        }  

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                if(_animalService.UseEnergy()) await _hub.Clients.All.Death("Animal");
                _animalService.BreedAnimals();
                await _hub.Clients.All.Refresh();
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
