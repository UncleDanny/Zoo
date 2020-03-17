using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Services
{
    public interface IAnimalHub
    {
        Task Refresh(string refresh = "Refresh");

        Task Death(string death = "Death");
    }
}
