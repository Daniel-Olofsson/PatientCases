using PatientCases.Context;
using PatientCases.Models.Entities;
using System;
using System.Linq;
using System.Numerics;

namespace YourProjectName.Services
{
    public class DoctorService
    {
        private readonly DataContext _context;

        public DoctorService(DataContext context)
        {
            _context = context;
        }

        public void AddDoctor(string fname,string lname, string specialization)
        {
            var newDoctor = new DoctorEntity
            {
                FName = fname,
                LName = lname,
                Specialization = specialization
            };

            _context.Doctors.Add(newDoctor);
            _context.SaveChanges();
        }

        public void ViewDoctors()
        {
            var doctors = _context.Doctors.ToList();

            foreach (var doctor in doctors)
            {
                Console.WriteLine($"Doctor ID: {doctor.Id}");
                Console.WriteLine($"Name: {doctor.FName}");
                Console.WriteLine($"Name: {doctor.LName}");
                Console.WriteLine($"Specialty: {doctor.Specialization}");
                
                Console.WriteLine();
            }
        }
    }
}
