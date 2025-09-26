using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mini_Fitness_tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>();

           
            User.LoadUsers(users);
            WorkOutPlan.LoadWorkOutPlans(users);
            Day.LoadDays(users);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t\t\t\t\t************************");
            Console.WriteLine("\t\t\t\t\t\t* Mini Fitness Tracker *");
            Console.WriteLine("\t\t\t\t\t\t************************");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                int index = -1;
                User user = new User();
                Console.WriteLine("[1]Create new profile\n[2]Log in \n[3]Exit ");
                Console.Write("Enter your choice: ");
                string test = Console.ReadLine();
                Console.WriteLine();
                int num;
                while (!int.TryParse(test, out num) || num < 1 || num > 3)
                {
                    Console.WriteLine("This value is invalid.");
                    Console.Write("Please enter another value from 1 to 3: ");
                    test = Console.ReadLine();
                }
                int choice = num;
                if (choice == 1)
                {
                    index = user.CreateNewProfile(users);
                    Console.WriteLine();
                }
                else if (choice == 2)
                {
                    index = user.Login(users);
                    Console.WriteLine();
                    if (index == -1)
                        continue;

                }
                else if (choice == 3)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("exit program ...");
                    break;
                }

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("The menu: \n[1]Viwe profile\n[2]Update profile\n[3]Log work out plan\n[4]Viwe progress\n[5]Exit");
                    Console.Write("Enter your choice: ");
                    test = Console.ReadLine();
                    Console.WriteLine();
                    while (!int.TryParse(test, out num) || num < 1 || num > 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("This value is invalid.");
                        Console.Write("Please enter another value from 1 to 5: ");
                        test = Console.ReadLine();
                    }
                    if (num == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        user.ViewProfile(index, users);
                        WorkOutPlan.View(index, users);
                        Console.WriteLine();
                    }
                    else if (num == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        user.UpdateProfile(index, users);
                        Console.WriteLine();
                    }
                    else if (num == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("The menu: \n [1]Add exercise \n [2]Remove exercise \n [3]Modify");
                        Console.Write("Enter your choice: ");
                        test = Console.ReadLine();
                        Console.WriteLine();
                        while (!int.TryParse(test, out num) || num < 1 || num > 3)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("This value is invalid.");
                            Console.Write("Please enter another value from 1 to 3: ");
                            test = Console.ReadLine();
                        }
                        if (num == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            
                            WorkOutPlan.Add_exercises(index, users);
                            Console.WriteLine();
                        }
                        else if (num == 2)
                        {
                            if (!WorkOutPlan.View(index, users))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("No exercises done yet!");
                                Console.WriteLine();
                                continue;
                            }
                            WorkOutPlan.Remove_exercises(index, users);
                        }
                        else
                        {
                            if (!WorkOutPlan.View(index, users))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("No exercises done yet!");
                                Console.WriteLine();
                                continue;
                            }
                            WorkOutPlan.Remove_exercises(index, users);
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine();
                            WorkOutPlan.Add_exercises(index, users);

                        }
                    }
                    else if (num == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        ProgressTracker progress = new ProgressTracker();
                        progress.DisplayProgress(index, users);
                        Console.WriteLine();
                    }
                    else
                        break;
                }
            }

        }
    }
}
