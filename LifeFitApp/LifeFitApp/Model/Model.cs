using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace LifeFitApp.Model
{
    public class LifeStyle : baseObj
    {
        public List<LifeList> lifeLists;
        public LifeStyle(string name, XmlReader reader)
        {
            this.typeName = name;
        }
    }

    public class LifeList : baseObj
    {
        public MealPlan mealPlan;
        public ExercisePlan exercisePlan;
    }

    public class MealPlan : baseObj
    {
        public List<Meal> meals;
    }


    public class ExercisePlan : baseObj
    {
        public List<Exercise> exercises;
    }

    public class Meal : ActivityItem
    {
        public int fat;
        public int carbs;
        public int protein;
        public string ingredients;
        public List<ActivityQualifier> dietaryRestrictions;
    }

    public class Exercise : ActivityItem
    {
        public ActivityQualifier exerciseType;
    }

    public class ActivityQualifier : baseObj
    {
        public string qualifier;
    }

    public class ActivityItem : baseObj
    {
        public string description;
        public int percentLike;
        public TimeSpan duration;
        public ActivitySteps steps;
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
        public void ParseData(XmlReader reader)
        {
            bool end = false;
            do
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Text:
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name == "Lifestylelist")
                        {
                            end = true;
                        }
                        break;
                }
            } while (reader.Read() && !end);
        }
    }

    public class LifeImage
    {
        public LifeImage(string path)
        {
            this.filePath = path;
            //this.image = Image(path, true);
        }
        //public Image imageMain;
        //public Image thumbnail;
        private string filePath;
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
		<workouts>
			<ID>W4</ID>
			<nameWorkout/>
			<description/>
			<type/>
			<percentLike/>
			<exerciseTime/>
			<instructions/>
			<imageMain/>
			<imageThumbnail/>
		</workouts>
		<workouts>
			<ID>W5</ID>
			<nameWorkout/>
			<description/>
			<type/>
			<percentLike/>
			<exerciseTime/>
			<instructions/>
			<imageMain/>
			<imageThumbnail/>
		</workouts>
		<workouts>
			<ID>W6</ID>
			<nameWorkout/>
			<description/>
			<type/>
			<percentLike/>
			<exerciseTime/>
			<instructions/>
			<imageMain/>
			<imageThumbnail/>
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
                                    break;
                                case "meals":
                                    break;
                                case "workouts":
                                    break;
                            }
                        break;
                    }
                }
            }
        }
        private Dictionary<string, baseObj> objectMap;
    }

    public class Model
    {
        public List<LifeStyle> lifeStyles;
    }
}
