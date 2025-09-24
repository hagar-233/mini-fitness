using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Fitness_tracker
{
    public class User
    {
        public string Name;
        public List<WorkOutPlan> workOutPlans = new List<WorkOutPlan>();
        public List<Day> days = new List<Day>();
        public string Username;
        public string Password;
        public int Age;
        public double Height;
        public double Weight;

        public static void SaveUsers(List<User> users)               // saving users data
        {
            string pathUsers = @"D:\Helper\Mini Fitness Tracker\userData.txt";

            using (StreamWriter sw = new StreamWriter(pathUsers, false))
            {
                foreach (var user in users)
                {
                    sw.WriteLine($"{user.Username},{user.Password},{user.Name},{user.Age},{user.Height},{user.Weight}");
                }
            }
        }

        //************************************************************************************************
        public int CreateNewProfile(List<User> users)           // creating new profile
        {
            User user = new User();
            while (string.IsNullOrWhiteSpace(user.Username))
            {
                Console.Write("Enter your username: ");
                user.Username = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    Console.WriteLine("Username cannot be empty. Please try again.");
                }
            }
            while (string.IsNullOrWhiteSpace(user.Name))
            {
                Console.Write("Enter your name: ");
                user.Name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    Console.WriteLine("Name cannot be empty. Please try again.");
                }
            }
            while (string.IsNullOrWhiteSpace(user.Password))
            {
                Console.Write("Enter your password: ");
                user.Password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    Console.WriteLine("Password cannot be empty. Please try again.");
                }
            }
            Console.Write("Please Enter Your Age : ");
            string test = Console.ReadLine();
            while (!int.TryParse(test, out user.Age) || user.Age <= 0)
            {
                Console.WriteLine("This value is invalid.");
                Console.Write("Please enter another value: ");
                test = Console.ReadLine();
            }
            Console.Write("Please Enter Your Height : ");
            test = Console.ReadLine();
            while (!double.TryParse(test, out user.Height) || user.Height <= 0)
            {
                Console.WriteLine("This value is invalid.");
                Console.Write("Please enter another value: ");
                test = Console.ReadLine();
            }
            Console.Write("Please Enter Your Weight : ");
            test = Console.ReadLine();
            while (!double.TryParse(test, out user.Weight) || user.Weight <= 0)
            {
                Console.WriteLine("This value is invalid.");
                Console.Write("Please enter another value: ");
                test = Console.ReadLine();
            }

            users.Add(user);

            User.SaveUsers(users);
            Console.WriteLine("done");
            return users.Count - 1;
        }

        //************************************************************************************************
        public int Login(List<User> users)                   // logging in
        {
            string username = "";
            string password = "";
            while (string.IsNullOrWhiteSpace(username))
            {
                Console.Write("Enter your username: ");
                username = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username cannot be empty. Please try again.");
                }
            }
            while (string.IsNullOrWhiteSpace(password))
            {
                Console.Write("Enter your password: ");
                password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("password cannot be empty. Please try again.");
                }
            }

            int index = -1;

            bool found = true;
            for (int i = 0; i < users.Count; i++)
            {

                if (users[i].Username == username && users[i].Password == password)
                {
                    Console.WriteLine("Login successful");
                    found = false;
                    index = i;
                    break;
                }
            }
            if (found)
            {
                Console.WriteLine("Exist wrong in your username or password");
            }

            return index;
        }

        //************************************************************************************************
        public void ViewProfile(int index, List<User> users)             // viewing profile
        {
            Console.WriteLine("User data:");
            Console.WriteLine();
            Console.WriteLine($"Username :{users[index].Username}");
            Console.WriteLine($"Password :{users[index].Password}");
            Console.WriteLine($"Name :{users[index].Name}");
            Console.WriteLine($"Age :{users[index].Age}");
            Console.WriteLine($"Height :{users[index].Height}");
            Console.WriteLine($"Weight :{users[index].Weight}");
        }


        //************************************************************************************************

        public void UpdateProfile(int index, List<User> users)           // updating profile
        {
            Console.WriteLine("[1]Update password \n[2]Update height \n[3]Update weight");
            Console.Write("Enter your choice: ");
            string test = Console.ReadLine();
            int choice;
            while (!int.TryParse(test, out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("This value is invalid.");
                Console.Write("Please enter another value from 1 to 3: ");
                test = Console.ReadLine();
            }

            if (choice == 1)
            {
                string newpassword = "";
                while (string.IsNullOrWhiteSpace(newpassword))
                {
                    Console.Write("Enter new password: ");
                    newpassword = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newpassword))
                    {
                        Console.WriteLine("New password cannot be empty. Please try again.");
                    }
                }
                users[index].Password = newpassword;
                User.SaveUsers(users);

            }
            else if (choice == 2)
            {
                Console.Write("Enter new height :");
                string input = Console.ReadLine();
                double newheight;

                while (!double.TryParse(input, out newheight) || newheight <= 0)
                {
                    Console.WriteLine("This value is invalid.");
                    Console.Write("Please enter another value: ");
                    input = Console.ReadLine();
                }

                users[index].Height = newheight;
                User.SaveUsers(users);
            }
            else if (choice == 3)
            {
                Console.Write("Enter new weight :");
                string input = Console.ReadLine();
                double newweight;

                while (!double.TryParse(input, out newweight) || newweight <= 0)
                {
                    Console.WriteLine("This value is invalid.");
                    Console.Write("Please enter another value: ");
                    input = Console.ReadLine();
                }

                users[index].Weight = newweight;
                User.SaveUsers(users);
            }
        }

        //**************************************************************************************************

        public static void LoadUsers(List<User> users)         // loading users
        {
            string pathUsers = @"D:\Helper\Mini Fitness Tracker\userData.txt";

            if (!File.Exists(pathUsers)) return;

            var lines = File.ReadAllLines(pathUsers);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 6)
                {
                    User u = new User
                    {
                        Username = parts[0],
                        Password = parts[1],
                        Name = parts[2],
                        Age = int.Parse(parts[3]),
                        Height = double.Parse(parts[4]),
                        Weight = double.Parse(parts[5]),
                        workOutPlans = new List<WorkOutPlan>(),
                        days = new List<Day>()
                    };

                    users.Add(u);
                }
            }
        }


    }
}
