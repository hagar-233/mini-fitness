using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Fitness_tracker
{
    public class ProgressTracker
    {
        public double weeklyCalories = 0;
        public double totalWorkOutTimePerWeek = 0;


        public void DisplayProgress(int index, List<User> users)    // displaying the progress of a user
        {
            if (users[index].days.Count == 0)
            {
                Console.WriteLine("No progress achieved yet!");
            }

            else
            {
                Day latestDay = users[index].days[0];
                for (int i = 0; i < users[index].days.Count; i++)
                {
                    if (users[index].days[i].date > latestDay.date)
                        latestDay = users[index].days[i];
                }

                Console.WriteLine("Calories burned and workout time for the last 7 days: ");
                Console.WriteLine("Day\t\tCalories Burned\t\tWorkout Time\n");
                Console.WriteLine("{0,-18}{1,-13}{2,18}", latestDay.date.ToShortDateString(), latestDay.calories + " cal", latestDay.minutes + " min");
                weeklyCalories += latestDay.calories;
                totalWorkOutTimePerWeek += latestDay.minutes;
                DateTime currentdate = latestDay.date;

                for (int i = 1; i < 7; i++)
                {
                    currentdate = currentdate.AddDays(-1);

                    for (int j = 0; j < users[index].days.Count; j++)
                    {
                        if (currentdate == users[index].days[j].date)
                        {
                            Console.WriteLine("{0,-18}{1,-13}{2,18}", users[index].days[j].date.ToShortDateString(), users[index].days[j].calories + " cal", users[index].days[j].minutes + " min");
                            weeklyCalories += users[index].days[j].calories;
                            totalWorkOutTimePerWeek += users[index].days[j].minutes;
                            break;
                        }
                    }
                }

                Console.WriteLine($"\nThe total calories burned for last week = {weeklyCalories} cal");
                Console.WriteLine($"The total workout time spent for last week = {totalWorkOutTimePerWeek} min");
            }
        }

    }
}
