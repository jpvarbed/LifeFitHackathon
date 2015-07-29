using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace LifeFitApp.Model
{
    public class ParseHelper
    {
        static public List<string> ParseList(string list)
        {
            char[] delimiterChars = { ';' };
            string[] arr = list.Split(delimiterChars);
            List<string> listObj = new List<string>(arr);
            return listObj;
        }

        static public List<baseObj> GetChildren(string list)
        {
            List<string> children = ParseList(list);
            List<baseObj> output = new List<baseObj>();
            foreach(var child in children)
            {
                baseObj obj;
                if (DataModel.objectMap.TryGetValue(child, out obj))
                {
                    output.Add(obj);
                }
            }
            return output;
        }

        static public List<LifeList> GetLifeList(string list)
        {
            List<string> children = ParseList(list);
            List<LifeList> output = new List<LifeList>();
            foreach (var child in children)
            {
                baseObj obj;
                if (DataModel.objectMap.TryGetValue(child, out obj))
                {
                    output.Add((LifeList)obj);
                }
            }
            return output;
        }

        static public List<Meal> GetMealList(string list)
        {
            List<string> children = ParseList(list);
            List<Meal> output = new List<Meal>();
            foreach (var child in children)
            {
                baseObj obj;
                if (DataModel.objectMap.TryGetValue(child, out obj))
                {
                    output.Add((Meal)obj);
                }
            }
            return output;
        }
        static public List<Exercise> GetExerciseList(string list)
        {
            List<string> children = ParseList(list);
            List<Exercise> output = new List<Exercise>();
            foreach (var child in children)
            {
                baseObj obj;
                if (DataModel.objectMap.TryGetValue(child, out obj))
                {
                    output.Add((Exercise)obj);
                }
            }
            return output;
        }
    }

    public class LifeStyle : baseObj
    {
        public List<LifeList> lifeLists;
        public string lists;
        public LifeStyle(string name, XmlReader reader)
        {
            this.typeName = name;
            ParseData(name, reader);
        }

        override public void Initialize()
        {
            lifeLists = (List<LifeList>) ParseHelper.GetLifeList(lists);
        }

        override public void ParseOtherData(string readerName, XmlReader reader)
        {
            bool end = false;
            switch (readerName)
            {
                case "lifelistDB":
                    this.lists = reader.Value;
                    break;
            }
        }
    }

    public class LifeList : baseObj
    {
        public MealPlan mealPlan;
        public ExercisePlan exercisePlan;
        public string mealList;
        public string exerciseList;
        public string exerciseDuration;
        public string mealDuration;
        public int followers { get; set; } = 0;
        public LifeList(string name, XmlReader reader)
        {
            this.typeName = name;
            ParseData(name, reader);
        }

        override public void Initialize()
        {
            mealPlan = new MealPlan(mealList);
            exercisePlan = new ExercisePlan(exerciseList);
        }

        override public void ParseOtherData(string readerName, XmlReader reader)
        {
            switch (readerName)
            {
                case "meals":
                    this.mealList = reader.Value;
                    break;
                case "exercise":
                    this.exerciseList = reader.Value;
                    break;
                case "mealsTime":
                    this.mealDuration = reader.Value;
                    break;
                case "workoutsTime":
                    this.exerciseDuration = reader.Value;
                    break;
                case "followers":
                    this.followers = reader.ReadContentAsInt();
                    break;
            }
        }
    }

    public class MealPlan : baseObj
    {
        public List<Meal> meals;
        public MealPlan(string mealList)
        {
            meals = ParseHelper.GetMealList(mealList);
        }
    }

    public class ExercisePlan : baseObj
    {
        public List<Exercise> exercises;

        public ExercisePlan(string mealList)
        {
            exercises = ParseHelper.GetExerciseList(mealList);
        }
    }

    public class Meal : ActivityItem
    {
        //public List<ActivityQualifier> dietaryRestrictions;

        public Meal(string name, XmlReader reader)
        {
            this.typeName = name;
            ParseData(name, reader);
        }
    }

    public class Exercise : ActivityItem
    {
        //public ActivityQualifier exerciseType;

        public Exercise(string name, XmlReader reader)
        {
            this.typeName = name;
            ParseData(name, reader);
        }
    }

    public class ActivityQualifier : baseObj
    {
        public string qualifier;
    }

    public class ActivityItem : baseObj
    {
        public double percentLike { get; set; } = 20;
        public string duration { get; set; } = "20 minutes";
        public string steps { get; set; } = "Do yoga";

        // hack
        public string fat { get; set; } = "50mg";
        public string carbs { get; set; } = "2000g";
        public string protein { get; set; } = "30g";
        public string ingredients { get; set; } = "300 ants";

        // exercise hack
        public string exerciseType { get; set; } = "cardio";

        override public void ParseOtherData(string readerName, XmlReader reader)
        {
            bool end = false;
            switch (readerName)
            {
                case "fat":
                    this.fat = reader.Value;
                    break;
                case "carbs":
                    this.carbs = reader.Value;
                    break;
                case "protein":
                    this.protein = reader.Value;
                    break;
                case "ingredients":
                    this.ingredients = reader.Value;
                    break;
                case "mealTime":
                case "exerciseTime":
                    this.duration = reader.Value;
                    break;
                case "type":
                    this.exerciseType = reader.Value;
                    break;
                case "percentLike":
                    this.percentLike = reader.ReadContentAsDouble();
                    break;
            }
        }
    }

    public class ActivitySteps : baseObj
    {
        public List<string> steps;
    }

    public class baseObj
    {
        public string guid { get; set; } = "1";
        public string name { get; set; } = "Dunking";
        public LifeImage image;
        public string typeName { get; set; } = "type";
        // not all objects have this
        public string description { get; set; } = "description";
        public string imageMain = "images/lifelists/pinocchio.jpg";
        public string imageFixedPath { get; set; }
        public string imageThumb;
        public virtual void ParseOtherData(string readerName, XmlReader reader) { }

        public virtual void Initialize() { }

        public string GetImageAssetPath(string badPath)
        {
            string start = "ms-appx:///Assets/";
            string goodPath = badPath.Replace(@"\", "/");
            return (start + goodPath);
        }

        public void ParseData(string type, XmlReader reader)
        {
            bool end = false;
            string readerName = "nothing";
            do
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.Name == this.typeName)
                        {
                            end = true;
                        }
                        break;
                    case XmlNodeType.Element:
                        readerName = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        switch (readerName)
                        {
                            case "ID":
                                this.guid = reader.Value;
                                break;
                            case "name":
                                this.name = reader.Value;
                                break;
                            case "imageMain":
                                if (reader.Value != "PATH")
                                {
                                    this.imageMain = reader.Value;
                                }
                                this.imageFixedPath = GetImageAssetPath(this.imageMain);
                                break;
                            case "imageThumbnail":
                                this.imageThumb = reader.Value;
                                break;
                            case "description":
                                this.description = reader.Value;
                                break;
                            default:
                                ParseOtherData(readerName, reader);
                                break;
                        }
                        break;
                }
            } while (!end && reader.Read());
        }
    }

    public class LifeImage
    {
        public LifeImage(string mainPath, string thumbnailPath)
        {
            this.filePath = mainPath;
            this.thumbnailPath = thumbnailPath;
            //this.image = Image(path, true);
        }
        //public Image imageMain;
        //public Image thumbnail;
        public string filePath;
        public string thumbnailPath;
    }

    public class DataModel
    {
        public List<LifeStyle> import()
        {
            List<LifeStyle> lifestyles = new List<LifeStyle>();
            objectMap = new Dictionary<string, baseObj>();
            using (XmlReader reader = XmlReader.Create("Assets\\data.xml"))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.Name)
                            {
                                case "Lifestylelist":
                                    LifeStyle style = new LifeStyle("Lifestylelist", reader);
                                    objectMap.Add(style.guid, style);
                                    lifestyles.Add(style);
                                    break;
                                case "Lifelist":
                                    LifeList list = new LifeList("Lifelist", reader);
                                    objectMap.Add(list.guid, list);
                                    break;
                                case "meal":
                                    Meal meal = new Meal("meal", reader);
                                    objectMap.Add(meal.guid, meal);
                                    break;
                                case "workout":
                                    Exercise exercise = new Exercise("workout", reader);
                                    objectMap.Add(exercise.guid, exercise);
                                    break;
                            }
                        break;
                    }
                }
            }

            foreach(var item in objectMap)
            {
                item.Value.Initialize();
            }

            return lifestyles;
        }
        static public Dictionary<string, baseObj> objectMap;
    }

    public class Model
    {
        public Model()
        {
            DataModel model = new DataModel();
            lifeStyles = model.import();
        }
        
        public List<LifeStyle> GetLifeStyles()
        {
            return this.lifeStyles;
        }

        private List<LifeStyle> lifeStyles;
    }
}
