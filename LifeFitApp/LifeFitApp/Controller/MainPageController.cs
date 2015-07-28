using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeFitApp.Controller
{
    class MainPageController
    {
        public string currentPlan;
        public string currentView;
        public int dayOfWeek;
        List<string> meals;
        List<string> workouts;

        public MainPageController()
        {

        }

        public void UpdateModel()
        {
            dayOfWeek = 2;


        }
    }
}
