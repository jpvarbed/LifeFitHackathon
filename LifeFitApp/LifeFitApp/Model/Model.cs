﻿using System;
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

        override public void ParseOtherData(XmlReader reader)
        {
            bool end = false;
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
                    case XmlNodeType.Text:
                        switch (reader.Name)
                        {
                            case "lifelistDB":
                                this.lists = reader.Value;
                                break;
                        }
                        break;
                }
            } while (reader.Read() && !end);
        }
    }

    public class LifeList : baseObj
    {
        public MealPlan mealPlan;
        public ExercisePlan exercisePlan;
        public string mealList;
        public string exerciseList;
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

        public void ParseOtherData(XmlReader reader)
        {
            bool end = false;
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
                    case XmlNodeType.Text:
                        switch (reader.Name)
                        {
                            case "meals":
                                this.mealList = reader.Value;
                                break;
                            case "exercise":
                                this.exerciseList = reader.Value;
                                break;
                        }
                        break;
                }
            } while (reader.Read() && !end);
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
        public string description;
        public int percentLike;
        public string duration;
        public string steps;

        // hack
        public string fat;
        public string carbs;
        public string protein;
        public string ingredients;

        // exercise hack
        public string exerciseType;

        public void ParseOtherData(XmlReader reader)
        {
            bool end = false;
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
                    case XmlNodeType.Text:
                        switch (reader.Name)
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
                        }
                        break;
                }
            } while (reader.Read() && !end);
        }
    }

    public class ActivitySteps : baseObj
    {
        public List<string> steps;
    }

    public class baseObj
    {
        public string guid;
        public string name;
        public LifeImage image;
        public string typeName;
        // not all objects have this
        public string description;
        public string imageMain;
        public string imageThumb;
        public virtual void ParseOtherData(XmlReader reader) { }

        public virtual void Initialize() { }

        public void ParseData(string type, XmlReader reader)
        {
            bool end = false;
            do
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.Name == "type")
                        {
                            end = true;
                        }
                        break;
                    case XmlNodeType.Text:
                        switch (reader.Name)
                        {
                            case "ID":
                                this.guid = reader.Value;
                                break;
                            case "nameLifestyle":
                                this.name = reader.Value;
                                break;
                            case "imageMain":
                                this.imageMain = reader.Value;
                                break;
                            case "imageThumbnail":
                                this.imageThumb = reader.Value;
                                break;
                            case "description":
                                this.description = reader.Value;
                                break;
                            default:
                                ParseOtherData(reader);
                                break;
                        }
                        break;
                }
            } while (reader.Read() && !end);
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
        private string filePath;
        private string thumbnailPath;
    }

    public class DataModel
    {
        public void import()
        {
            String xmlString = @"<lifestylesDB>
	<section1>
		<Lifestylelist>
			<ID>1</ID>
			<nameLifestyle>Athlete</nameLifestyle>
			<lifelistDB>L1;L2</lifelistDB>
			<imageMain>PATH</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</Lifestylelist>
		<Lifestylelist>
			<ID>2</ID>
			<nameLifestyle>Getting Married</nameLifestyle>
			<lifelistDB>L3;L4</lifelistDB>
			<imageMain>PATH</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</Lifestylelist>
		<Lifestylelist>
			<ID>3</ID>
			<nameLifestyle>Beginner</nameLifestyle>
			<lifelistDB>L1;L2</lifelistDB>
			<imageMain>PATH</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</Lifestylelist>
		<Lifestylelist>
			<ID>4</ID>
			<nameLifestyle>Celebrity</nameLifestyle>
			<lifelistDB>L1;L2</lifelistDB>
			<imageMain>PATH</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</Lifestylelist>
		<Lifestylelist>
			<ID>5</ID>
			<nameLifestyle>Beach &amp; Summer</nameLifestyle>
			<lifelistDB>L1;L2</lifelistDB>
			<imageMain>PATH</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</Lifestylelist>
		<Lifestylelist>
			<ID>6</ID>
			<nameLifestyle>Hoops and Dunks</nameLifestyle>
			<lifelistDB>L1;L2</lifelistDB>
			<imageMain>PATH</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</Lifestylelist>
		<Lifestylelist>
			<ID>7</ID>
			<nameLifestyle>Crossfit</nameLifestyle>
			<lifelistDB>L1;L2</lifelistDB>
			<imageMain>PATH</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</Lifestylelist>
	</section1>
	<section2>
		<Lifelist>
			<ID>L1</ID>
			<nameLifelist>Dunk like Dennis</nameLifelist>
			<mealTime>0</mealTime>
			<workoutTime>0</workoutTime>
			<imageMain>images\lifelists\dunklikedennis.jpg</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
			<followers>0</followers>
			<meals>M1;M2;M3</meals>
			<exercise>W1;W2;W3</exercise>
			<description>Dunk bastketballs and jump like Dennis</description>
		</Lifelist>
		<Lifelist>
			<ID>L2</ID>
			<nameLifelist>Run like Forest</nameLifelist>
			<mealTime>0</mealTime>
			<workoutTime>0</workoutTime>
			<imageMain>images\lifelists\runforestrun.jpg</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
			<followers>0</followers>
			<meals>M3;M4;M5</meals>
			<exercise>W4;W5;W6</exercise>
			<description>Life is like a box of chocolates</description>
		</Lifelist>
		<Lifelist>
			<ID>L3</ID>
			<nameLifelist>Fuck like Pinoccio</nameLifelist>
			<mealTime>0</mealTime>
			<workoutTime>0</workoutTime>
			<imageMain>images\lifelists\pinocchio.jpg</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
			<followers>0</followers>
			<meals>M1;M4;M6</meals>
			<exercise>W1;W3;W6</exercise>
			<description>Don't let you nose be your tell</description>
		</Lifelist>
		<Lifelist>
			<ID>L4</ID>
			<nameLifelist>Triatholon Prep</nameLifelist>
			<mealTime>0</mealTime>
			<workoutTime>0</workoutTime>
			<imageMain>PATH</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
			<followers>0</followers>
			<meals>M1;M4;M6</meals>
			<exercise>W1;W3;W6</exercise>
			<description>Don't let you nose be your tell</description>
		</Lifelist>
	</section2>
	<section3>
		<meals>
			<ID>M1</ID>
			<nameMeal>Tuna Cakes</nameMeal>
			<description>Tuna cakes will help you gain muscle and shed fat</description>
			<ingredients>3 oz Tuna; 1 Egg; 1 Tbls Coconut Oil; 1 Green Onion</ingredients>
			<percentLike>0</percentLike>
			<imageMain>images\meals\tunacakes.jpg</imageMain>
			<mealTime>20</mealTime>
			<instructions>Step 1:  Drain the tuna and mix the tuna and egg together in a bowl; Step 2: Chop up the green onion and mix in the tuna; Step 3:  Add coconut oil to a pan on medium-high heat.    Smush the tuna into a flat cake like size and once the oil is hot cook; Step 4: After 3 minutes or once the bottom is fairly brown, flip the tuna cake and cook for 3 more minutes</instructions>
			<imageThumbnail>PATH</imageThumbnail>
			<dietaryRestrictions>GF; P;</dietaryRestrictions>
			<fat>100</fat>
			<carbs>0</carbs>
			<protien>200</protien>
		</meals>
	</section3>
	<section4>
		<workouts>
			<ID>W1</ID>
			<nameWorkout>Running</nameWorkout>
			<description>Run your ass off</description>
			<type>Aerobic</type>
			<percentLike>0.1</percentLike>
			<exerciseTime>30</exerciseTime>
			<instructions>Run!</instructions>
			<imageMain>images\workouts\Aerobic_exercise.jpg</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</workouts>
		<workouts>
			<ID>W2</ID>
			<nameWorkout>Yoga like a pro</nameWorkout>
			<description>Sweat out that water weight</description>
			<type>Weight control</type>
			<percentLike>0.9</percentLike>
			<exerciseTime>60</exerciseTime>
			<instructions>Power Yoga</instructions>
			<imageMain>images\workouts\yoga.jpg</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</workouts>
		<workouts>
			<ID>W3</ID>
			<nameWorkout>Cum on man</nameWorkout>
			<description>What do you think</description>
			<type>Anaerobic</type>
			<percentLike>0.8</percentLike>
			<exerciseTime>90</exerciseTime>
			<instructions>Jerking off</instructions>
			<imageMain>images\workouts\wink.jpg</imageMain>
			<imageThumbnail>PATH</imageThumbnail>
		</workouts>
	</section4>
</lifestylesDB>";
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
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
                                    break;
                                case "LifeList":
                                    LifeList list = new LifeList("Lifelist", reader);
                                    objectMap.Add(list.guid, list);
                                    break;
                                case "meals":
                                    Meal meal = new Meal("meals", reader);
                                    objectMap.Add(meal.guid, meal);
                                    break;
                                case "workouts":
                                    Exercise exercise = new Exercise("workouts", reader);
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
        }
        static public Dictionary<string, baseObj> objectMap;
    }

    public class Model
    {
        public Model()
        {
            DataModel model = new DataModel();
            model.import();
        }
        
        public List<LifeStyle> GetLifeStyles()
        {
            return this.lifeStyles;
        }

        private List<LifeStyle> lifeStyles;
    }
}