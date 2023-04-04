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
            var patients = _context.Patients.ToList();

            foreach (var patient in patients)
            {
                Console.WriteLine($"Patient ID: {patient.Id}");
                Console.WriteLine($"Name: {patient.PatientName}");
                
                Console.WriteLine($"Doctor Name: {patient.Doctor.FName}");
                Console.WriteLine();
            }
        }
    }
}
