using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

using System.Threading.Tasks;
using Zoo.Animals;
using Zoo.Services;

namespace Zoo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAnimalService _animalService;
     

        public List<Animal> Animals { get; set; }

        public SelectList ListOfAnimals {get; set;}

        [BindProperty(SupportsGet = true)]
        public string SelectedAddAnimal { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedFeedAnimal { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IAnimalService animalService)
        {
            _logger = logger;
            _animalService = animalService;
            Animals = _animalService.GetAnimals();
            ListOfAnimals = new SelectList(_animalService.GetAnimalTypeNames());
        }

        public void OnGet()
        {
            
        }

        public void OnPostAdd([FromBody] AnimalDto animal)
        {
            var name = animal.Name;
            if (name is null)
            {
                return;
            }

            Type type = Type.GetType($"Zoo.Animals.{animal.Type}");         
            _animalService.AddAnimal((Animal)Activator.CreateInstance(type, name));
        }

        public void OnPostFeed([FromBody]string a)
        {
            Type type = Type.GetType($"Zoo.Animals.{a}");
            IEnumerable animals = _animalService.GetAnimals().Where(x => x.GetType() == type);
            foreach (Animal animal in animals)
            {
                animal.Eat();
            }
        }


        

        
        //public  void OnPostFeeding([FromBody] string animal)
        //{

        //    Type type = Type.GetType($"Zoo.Animals.{animal}");
        //    IEnumerable animals = _animalService.GetAnimals().Where(x => x.GetType() == type);
        //    foreach (Animal a in animals)
        //    {
        //        a.Eat();
        //    }
        //}


        public PartialViewResult OnGetAnimalPartial()
        {
            return Partial("_AnimalPartial", Animals);
        }
    }
}
