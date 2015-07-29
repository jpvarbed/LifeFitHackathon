using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeFitApp.Model;

namespace LifeFitApp.Controller
{
    class LifeStyleDouble
    {
        public LifeStyleDouble(LifeStyle style, LifeStyle style2)
        {
            name = style.name;
            name2 = style2.name;
            imageFixedPath = style.imageFixedPath;
            imageFixedPath2 = style2.imageFixedPath;
        }
        public string name { get; set; }
        public string name2 { get; set; }
        public string imageFixedPath { get; set; }
        public string imageFixedPath2 { get; set; }
    }

    class LifeStyleGenreController
    {
        private Model.Model model;
        public List<LifeStyle> lifeStyles;
        public List<LifeStyleDouble> doubleStyles;
        public string imagePath;

        public LifeStyleGenreController()
        {
            this.InitLifeStyles();
        }

        public void InitLifeStyles()
        {
            model = new Model.Model();

            lifeStyles = model.GetLifeStyles();

            doubleStyles = new List<LifeStyleDouble>();
            var it = lifeStyles.GetEnumerator();
            while(it.MoveNext())
            {
                LifeStyle one;
                one = it.Current;
                it.MoveNext();
                LifeStyleDouble doubleStyle = new LifeStyleDouble(one, it.Current);
                doubleStyles.Add(doubleStyle);
            }
        }
    }
}
