using PatientCases.Context;
using PatientCases.Models.Entities;
using PatientCases.Services;
using System;
using YourProjectName.Services;

namespace YourProjectName;

class Program
{
    static async Task Main(string[] args)
    {
        var _statusService = new StatusService();
        await _statusService.InitializeAsync().ConfigureAwait(false);
        using (var context = new DataContext())
        {
            var caseService = new CaseService(context, new StatusService());
            var doctorService = new DoctorService(context);
            var patientService = new PatientService(context);

            int selectedIndex = 0;
            int previousSelectedIndex = -1;
            bool exitRequested = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Please select an option:");
                Console.WriteLine();

                PrintMenuOption("Add case", selectedIndex == 0);
                PrintMenuOption("View cases", selectedIndex == 1);
                PrintMenuOption("Add doctor", selectedIndex == 2);
                PrintMenuOption("View doctors", selectedIndex == 3);
                PrintMenuOption("Add patient", selectedIndex == 4);
                PrintMenuOption("View patients", selectedIndex == 5);
                PrintMenuOption("Exit", selectedIndex == 6);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedIndex > 0)
                        {
                            selectedIndex--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedIndex < 6)
                        {
                            selectedIndex++;
                        }
                        break;

                    case ConsoleKey.Enter:
                        switch (selectedIndex)
                        {
                            case 0:
                                Console.Clear();
                                Console.WriteLine("Enter comment:");
                                var commentText = Console.ReadLine();
                                var comment = new CommentEntity()
                                {
                                    Comment = commentText,
                                    Created = DateTime.Now
                                };
                                Console.WriteLine("Enter case title:");
                                var caseTitle = Console.ReadLine();

                                Console.WriteLine("Enter doctor ID:");
                                var doctorIdText = Console.ReadLine();
                                var doctorId = int.Parse(doctorIdText);

                                Console.WriteLine("Enter patient ID:");
                                var patientIdText = Console.ReadLine();
                                var patientId = int.Parse(patientIdText);

                                Console.WriteLine("Enter status (1 = Pending, 2 = In Progress, 3 = Resolved):");
                                var statusText = Console.ReadLine();
                                var statusId = int.Parse(statusText);

                                caseService.AddCase(comment, caseTitle, doctorId, patientId, statusId);
                                Console.WriteLine("Case added successfully!");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;

                            case 1:
                                Console.Clear();
                                caseService.ViewCases();
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("Enter doctor name:");
                                var name = Console.ReadLine();
                                Console.WriteLine("Enter doctor last name:");
                                var lname = Console.ReadLine();
                                Console.WriteLine("Enter doctor specialty:");
                                var specialty = Console.ReadLine();

                                doctorService.AddDoctor(name,lname, specialty);
                                Console.WriteLine("Doctor added successfully!");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;

                            case 3:
                                Console.Clear();
                                doctorService.ViewDoctors();
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;

                            case 4:
                                Console.Clear();
                                Console.WriteLine("Enter patient name:");
                                var patientName = Console.ReadLine();

                                Console.WriteLine("Enter patient Email:");
                                var email = (Console.ReadLine());

                                Console.WriteLine("Enter doctor ID:");
                                var patientDoctorId = int.Parse(Console.ReadLine());

                                patientService.AddPatient(patientName, email, patientDoctorId);
                                Console.WriteLine("Patient added successfully!");
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;

                            case 5:
                                Console.Clear();
                                patientService.ViewPatients();
                                Console.WriteLine("Press any key to continue.");
                                Console.ReadKey();
                                break;

                            case 6:
                                exitRequested = true;
                                break;
                        }
                        break;
                }

                if (selectedIndex != previousSelectedIndex)
                {
                    previousSelectedIndex = selectedIndex;
                }

            } while (!exitRequested);
        }
    }

    static void PrintMenuOption(string text, bool isSelected)
    {
        if (isSelected)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }
        Console.WriteLine(text);
        Console.ResetColor();
        Console.ResetColor();
    }
}