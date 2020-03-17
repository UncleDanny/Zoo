using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Linq;
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
                if (_animalService.GetAnimals().Any())
                {
                    var tuple = _animalService.UseEnergy();
                    if (tuple.Item1) await _hub.Clients.All.Death(tuple.Item2);
                    await _hub.Clients.All.Refresh();
                } else
                {
                    await _hub.Clients.All.AllDied();
                    while (!_animalService.GetAnimals().Any())
                    {
                        await Task.Delay(500, stoppingToken);
                    }
                }
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
