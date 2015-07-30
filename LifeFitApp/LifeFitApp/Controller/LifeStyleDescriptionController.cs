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
        public string followers { get; set; }
        public string imageFixedPath { get; set; }
        public string description { get; set; }

        public string workoutDuration { get; set; }
        public string mealDuration { get; set; }
        public LifeStyleDescriptionController(object obj)
        {
            list = obj as LifeList;
            mealPlan = list.mealPlan;
            exercisePlan = list.exercisePlan;
            title = list.name;
            imageFixedPath = list.imageFixedPath;
            description = list.description;
            mealDuration = "Cooking Time   " + list.mealDuration;
            workoutDuration = "Workout Time   " + list.exerciseDuration;
        }
        // TODO this shouldnt be public
        public LifeList list;
    }
}
