using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeFitApp.Model;

namespace LifeFitApp.Controller
{
    class LifeStyleGenreController
    {
        private Model.Model model;
        public List<LifeStyle> lifeStyles;

        public LifeStyleGenreController()
        {
            this.InitLifeStyles();
        }

        public void InitLifeStyles()
        {
            model= new Model.Model();

            lifeStyles = model.GetLifeStyles();
        }
    }
}
