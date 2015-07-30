using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeFitApp.Model;

namespace LifeFitApp.Controller
{
    public class ActivityItemController
    {
        public string title { get; set; }
        public List<string> ingredients { get; set; }
        public List<string> instructions { get; set; }
        public string imageFixedPath { get; set; }
        public string description { get; set; }
        public ActivityItemController(object obj)
        {
            activity = obj as ActivityItem;
            title = activity.name;
            ingredients = activity.ingredientsList;
            instructions = activity.instructionsList;
            imageFixedPath = activity.imageFixedPath;
            description = activity.description;
        }

        protected ActivityItem activity;
    }

    class MealController : ActivityItemController
    {
        public MealController(object obj) : base(obj)
        {
        }
    }
}
