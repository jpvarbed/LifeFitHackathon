using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeFitApp.Model;

namespace LifeFitApp.Controller
{
    class MealController
    {
        public string title { get; set; }
        public List<string> ingredients { get; set; }
        public List<string> instructions { get; set; }
        public string imageFixedPath { get; set; }
        public string description { get; set; }
        public MealController(object obj)
        {
            meal = obj as Meal;
            title = meal.name;
            ingredients = meal.ingredientsList;
            instructions = meal.instructionsList;
            imageFixedPath = meal.imageFixedPath;
            description = meal.description;
        }
        private Meal meal;
    }
}
