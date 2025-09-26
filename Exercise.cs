using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Fitness_tracker
{
    
    
       public class Excerciseitem
        {
            public string Name;
            public int CaloriesBurnedPerMin;
            public Excerciseitem(string name, int caloriesBurnedPerMin)
            {
                Name = name;
                CaloriesBurnedPerMin = caloriesBurnedPerMin;
            }
        }
       public class Exercise
        {
           
            public static string Type;

            public static List<Excerciseitem> Cardio = new List<Excerciseitem>
            {
              new Excerciseitem( "Running",10),
              new Excerciseitem("Cycling",8),
              new Excerciseitem("Swimming",9),
              new Excerciseitem("Jump Rose",12)

            };

            public static List<Excerciseitem> Strength = new List<Excerciseitem>
            {
               new Excerciseitem( "Push-up",7),
                new Excerciseitem("Squats",8),
                    new Excerciseitem( "Plank",4),
                    new Excerciseitem("Deadlift",10)

            };
            public static List<Excerciseitem> Yoga = new List<Excerciseitem>
            {
                 new Excerciseitem("Tree Pose",3),
                 new Excerciseitem("Warrior Pose",4),
                 new Excerciseitem("Child's Pose",2),
                 new Excerciseitem("sun salutation",5)

            };

            public static double CalculateCalories(double duration, int calore)
            {
                double totalcalore = duration * calore;
                return totalcalore;
            }
            public static List<Excerciseitem> GetExercisesByType(string type)
            {
                type = type.ToLower().Trim();
                if (type == "cardio") return Cardio;
                else if (type == "strength") return Strength;
                else  return Yoga;
            }

       }


    
}

