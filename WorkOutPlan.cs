using Mini_Fitness_tracker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Mini_Fitness_tracker
{
    public class WorkOutPlan
    {
        public string Name_exercise;
        public double time;
        public double calories;
        public DateTime date;

        public static bool View(int index, List<User> users)          // viewing the workout plans of a user
        {
            int count = 1;
            if (users[index].workOutPlans.Count == 0)
                return false;
            Console.WriteLine("{0,-3} {1,-20} {2,-10} {3,-12}",
                      " ", "Exercise Name", "Duration", "Calories");
            foreach (var plan in users[index].workOutPlans)
            {
                Console.WriteLine("{0,-3} {1,-20} {2,-10} {3,-12}",
                                  count, plan.Name_exercise, plan.time, plan.calories);
                count++;
            }
            return true;
        }

        //************************************************************************************************
        public static void Add_exercises(int index, List<User> users)  // adding exercises to the workout plan of a user
        {
            WorkOutPlan workOutPlan = new WorkOutPlan();

            Console.Write("choose type(Cardio-Strength-Yoga):");

           string Type = Console.ReadLine();
            while(Type.ToLower().Trim()!="cardio"&& Type.ToLower().Trim() != "strength"&& Type.ToLower().Trim() != "yoga")
            {
                Console.Write("invalid choice! Please enter one of the following :(Cardio,Strength,or Yoga):");
                Type=Console.ReadLine();    
            }
            var list = Exercise.GetExercisesByType(Type);
            Console.WriteLine($"{Type} Exercises:");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {list[i].Name} ({list[i].CaloriesBurnedPerMin} cal/min)");
            }

            Console.Write("Enter the number of the exercise you want to add: ");
            string test = Console.ReadLine();
            int num;
            while (!int.TryParse(test, out num) || num <= 0 || num > list.Count)
            {
                Console.WriteLine("This value is invalid.");
                Console.Write("Please enter another value from 1 to " + list.Count + " : ");
                test = Console.ReadLine();
            }
            workOutPlan.Name_exercise = list[num - 1].Name;
            Console.Write("Enter the time of the exercise: ");
            test = Console.ReadLine();
            double duration;
            while (!double.TryParse(test, out duration ) || duration <= 0)
            {
                Console.WriteLine("This value is invalid.");
                Console.Write("Please enter a valid value " + " : ");
                test = Console.ReadLine();
            }
            workOutPlan.time = duration;
           double totalcaloreofexercise= Exercise.CalculateCalories(duration, list[num - 1].CaloriesBurnedPerMin);
            Console.WriteLine($"total calore of exercise {totalcaloreofexercise} cal");
            Console.Write("Enter today's date (month/day/year): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Invalid date, please enter again (month/day/year): ");
            }
            workOutPlan.date = date;
            workOutPlan.calories = totalcaloreofexercise;
            users[index].workOutPlans.Add(workOutPlan);
            bool existing = false;
            for (int i = 0; i < users[index].days.Count; i++)
            {
                if (users[index].days[i].date == workOutPlan.date)
                {
                    users[index].days[i].minutes += workOutPlan.time;
                    users[index].days[i].calories += workOutPlan.calories;
                    existing = true;
                    break;
                }
            }
            if (!existing)
            {
                Day day = new Day();
                day.date = workOutPlan.date;
                day.minutes = workOutPlan.time;
                day.calories =workOutPlan.calories;
                users[index].days.Add(day);
            }
            SaveWorkOutPlans(users);
            Day.SaveDays(users);
        }

        //******************************************************************************************************
        public static void Remove_exercises(int index, List<User> users)      // removing exercises from the workout plan of a user
        {
            WorkOutPlan workOutPlan = new WorkOutPlan();
            Console.Write("Enter the number of the exercise you want to remove: ");
            string test = Console.ReadLine();
            int num;
            while (!int.TryParse(test, out num) || num <= 0 ||  num > users[index].workOutPlans.Count)
            {
                Console.WriteLine("This value is invalid.");
                Console.Write("Please enter another value from 1 to " + users[index].workOutPlans.Count + " : ");
                test = Console.ReadLine();
            }

            for (int i = 0; i < users[index].days.Count; i++)
            {
                if (users[index].days[i].date == users[index].workOutPlans[num - 1].date)
                {
                    users[index].days[i].minutes -= users[index].workOutPlans[num - 1].time;
                    users[index].days[i].calories -= users[index].workOutPlans[num - 1].calories;
                    if (users[index].days[i].calories == 0)
                    {
                        users[index].days.Remove(users[index].days[i]);
                    }
                }
            }
            users[index].workOutPlans.Remove(users[index].workOutPlans[num - 1]);   
            SaveWorkOutPlans(users);
            Day.SaveDays(users);    
        }
        //******************************************************************************************************
        public static void SaveWorkOutPlans(List<User> users)
        {
            string pathPlans = @"E:workout.txt";

            using (StreamWriter sw = new StreamWriter(pathPlans, false))
            {
                foreach (var user in users)
                {
                    var plans = user.workOutPlans
                        .Select(p => $"{p.Name_exercise};{p.time};{p.date:yyyy-MM-dd};{p.calories}")
                        .ToList();

                    string line = $"{user.Username}|{string.Join(",", plans)}";
                    sw.WriteLine(line);
                }
            }
        }


        //******************************************************************************************************
        public static void LoadWorkOutPlans(List<User> users)
        {
            string pathPlans = @"E:workout.txt";
            if (!File.Exists(pathPlans)) return;

            var lines = File.ReadAllLines(pathPlans);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split('|');
                if (parts.Length != 2) continue;

                string username = parts[0];

                string[] plans = parts[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var user = users.FirstOrDefault(u => u.Username == username);
                if (user == null) continue;

                user.workOutPlans.Clear();

                foreach (var p in plans)
                {
                    var planParts = p.Split(';');
                    if (planParts.Length != 4) continue;

                    if (!double.TryParse(planParts[1], out double time)) continue;
                    if (!DateTime.TryParseExact(planParts[2], "yyyy-MM-dd", null,
                                                System.Globalization.DateTimeStyles.None, out DateTime date)) continue;
                    if (!double.TryParse(planParts[3], out double calories)) continue;

                    user.workOutPlans.Add(new WorkOutPlan
                    {
                        Name_exercise = planParts[0],
                        time = time,
                        date = date,
                        calories = calories
                    });
                }
            }
        }



    }
}

