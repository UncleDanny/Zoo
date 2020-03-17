using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Services
{
    public interface IAnimalHub
    {
        Task Refresh(string refresh = "Refresh");

        Task Death(string animal,string death = "Death");

        Task AllDied(string animalsDied = "All Animals Died");

        //Task Breed(string animal1, string animal2, string child, string bred = "Bred");
    }
}
