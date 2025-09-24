using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Fitness_tracker
{
    public class Day           // class to store daily workout data of a user
    {
        public double calories;
        public double minutes;
        public DateTime date;
        public static void SaveDays(List<User> users)
        {
            string pathDays = @"D:\Helper\Mini Fitness Tracker\days.txt";

            using (StreamWriter sw = new StreamWriter(pathDays, false))
            {
                foreach (var user in users)
                {                    
                    var daysData = user.days
                        .Select(d => $"{d.date:yyyy-MM-dd};{d.minutes};{d.calories}")
                        .ToList();
                    
                    string line = $"{user.Username}|{string.Join(",", daysData)}";
                    sw.WriteLine(line);
                }
            }
        }
        public static void LoadDays(List<User> users)
        {
            string pathDays = @"D:\Helper\Mini Fitness Tracker\days.txt";
            if (!File.Exists(pathDays))
                return;

            foreach (var line in File.ReadAllLines(pathDays))
            {
                
                var parts = line.Split('|');
                if (parts.Length < 2) continue;

                string username = parts[0];
                User user = users.FirstOrDefault(u => u.Username == username);
                if (user == null) continue;

                user.days.Clear();
                string[] daysParts = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var d in daysParts)
                {
                    var data = d.Split(';');
                    if (data.Length < 3) continue;

                    if (DateTime.TryParse(data[0], out DateTime date) &&
                        double.TryParse(data[1], out double minutes) &&
                        double.TryParse(data[2], out double calories))
                    {
                        user.days.Add(new Day
                        {
                            date = date,
                            minutes = minutes,
                            calories = calories
                        });
                    }
                }
            }
        }


    }
}
