using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeFitApp.Model;

namespace LifeFitApp.Controller
{
    class WeeklyController
    {
        public string name;
        public string activityName;
        public string activityImage;
        public WeeklyController(object obj)
        {
            list = obj as LifeList;
            name = list.name;
        }

        public void UpdateActivity()
        {
            ActivityItem item;
            if (mealTime)
            {
                item = list.mealPlan.meals.First();
            }
            else
            {
                item = list.exercisePlan.exercises.First();
            }
            mealTime = !mealTime;
            activityImage = item.imageFixedPath;
            activityName = item.name;
        }

        // true is meal
        public bool IsObjectMeal(object obj, out object sendObj)
        {
            bool isMeal = true;
            // toggled it last time
            if (mealTime)
            {
                isMeal = false;
                sendObj = list.exercisePlan.exercises.First();
            }
            else
            {
                sendObj = list.mealPlan.meals.First();
            }
            return isMeal;
        }
        private bool mealTime = false;
        private LifeList list;
    }
}
