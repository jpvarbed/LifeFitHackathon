using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeFitApp.Model;

namespace LifeFitApp.Controller
{
    class LifeStyleListController
    {
        public List<LifeList> lifeLists;
        public string title;
        public LifeStyleListController (object obj)
        {
            style = obj as LifeStyle;
            lifeLists = style.lifeLists;
            title = style.name;
        }
        private LifeStyle style;
    }
}
