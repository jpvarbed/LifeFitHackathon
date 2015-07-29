using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeFitApp.Model;

namespace LifeFitApp.Controller
{
    class LifeStyleDescriptionController
    {
        public MealPlan mealPlan;
        public ExercisePlan exercisePlan;
        public string title { get; set; }
        public LifeStyleDescriptionController(object obj)
        {
            list = obj as LifeList;
            mealPlan = list.mealPlan;
            exercisePlan = list.exercisePlan;
            title = list.name;
        }
        private LifeList list;
    }
}
