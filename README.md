# mini-fitness
mini fitness
🏃‍♂️ Mini Fitness Tracker
📌 Overview

Mini Fitness Tracker is a C# console-based application designed to help users manage and track their fitness activities.
It allows users to create personal profiles, log various types of exercises, calculate calories burned, track daily activity, and monitor their overall progress over time.

This project demonstrates strong object-oriented programming principles, file handling, and data persistence in C#.

✨ Features
👤 User Management

Create new user profiles with username, password, name, age, height, and weight.

Secure login with validation.

View and update profile details (password, height, weight).

All user data is stored persistently in a local text file (users.txt).

🏋️ Workout Plan Management

Choose exercises from three categories: Cardio, Strength, and Yoga.

Add exercises with duration and date.

Automatically calculate total calories burned based on duration and exercise intensity.

View, remove, or modify exercises in the workout plan.

All workout data is stored in workout.txt.

📅 Daily Activity Tracking

Track total workout time and total calories burned per day.

Automatically update daily stats when exercises are added or removed.

Daily activity data is saved in days.txt.

📈 Progress Tracking

View your total calories burned and total workout time.

Monitor your progress and improvement over time.

📁 Project Structure
Mini_Fitness_tracker/
│
├── Program.cs           # Entry point - handles menus and main logic
├── User.cs              # User creation, login, update, and data loading/saving
├── Exercise.cs          # Defines exercise categories and calorie calculations
├── WorkOutPlan.cs       # Add, remove, view, and manage workout plans
├── Day.cs               # Tracks daily workout statistics
└── ProgressTracker.cs   # Displays user progress and overall stats

▶️ How to Run

Open the project in Visual Studio or any C# IDE.

Build and run the solution.

Choose from the main menu:

[1] Create new profile
[2] Log in
[3] Exit


Once logged in, you can:

View or update your profile

Add, remove, or modify exercises

View progress and total calories burned

🧪 Example Usage
[1] Create new profile
[2] Log in
[3] Exit
Enter your choice: 1

Enter your username: hagar
Enter your name: Hagar Ahmed
Enter your password: 1234
Please enter your age: 21
Please enter your height: 165
Please enter your weight: 60
Profile created successfully!

The menu:
[1] View profile
[2] Update profile
[3] Log workout plan
[4] View progress
[5] Exit

🏋️ Example – Adding an Exercise
Choose type (Cardio - Strength - Yoga): cardio

Cardio Exercises:
1. Running (10 cal/min)
2. Cycling (8 cal/min)
3. Swimming (9 cal/min)
4. Jump Rope (12 cal/min)

Enter the number of the exercise you want to add: 1
Enter the time of the exercise (minutes): 30
Total calories burned: 300 cal
Enter today's date (month/day/year): 09/26/2025
Workout added successfully!

🛠️ Technologies Used

Language: C#

Framework: .NET Console Application

Data Storage: Local .txt files
