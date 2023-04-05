using Microsoft.EntityFrameworkCore;
using PatientCases.Context;

using PatientCases.Models.Entities;
using System;
using System.Linq;

namespace YourProjectName.Services
{
    public class PatientService
    {
        private readonly DataContext _context;

        public PatientService(DataContext context)
        {
            _context = context;
        }

        public void AddPatient(string name, string email, int doctorId)
        {
            var newPatient = new PatientEntity
            {
                PatientName = name,
                Email = email,
                DoctorId = doctorId,
                             
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();
        }

        public void ViewPatients()
        {
            var caseContext = _context.Cases
                .Include(s => s.Status)
                .ToList();
            var patients = _context.Patients
                .Include(p=>p.Cases)
                .Include(p=>p.Doctor)
                .ToList();

            foreach (var item in patients)
            {
                Console.WriteLine($"Name: {item.PatientName}");
                foreach (var _cases in item.Cases)
                {
                    Console.WriteLine($"Case:{_cases.Title}");

                }
                Console.Write("All current case statuses");
                foreach (var cases in caseContext)
                {
                    
                    Console.WriteLine($"{cases.Status.StatusName}");
                }
                //Console.WriteLine("------------------------");
                Console.WriteLine($"Patient ID: {item.Id}");
                
                Console.WriteLine($"Doctor Name: {item.Doctor.FName}");
                Console.WriteLine();
                

            }

        }
    }
}
