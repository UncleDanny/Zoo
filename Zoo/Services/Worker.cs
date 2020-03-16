﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Zoo.Services
{
    public class Worker : BackgroundService
    {
        private IHubContext<ChatHub,IChatHub> _hub;
        private IAnimalService _animalService;
        
        public Worker(IHubContext<ChatHub,IChatHub> hub,IAnimalService animalService)
        {
            _hub = hub;
            _animalService = animalService;
        }  

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _animalService.UseEnergy();
                await this._hub.Clients.All.Refresh();
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
