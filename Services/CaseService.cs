using PatientCases.Context;
using PatientCases.Models.Entities;
using PatientCases.Services;
using System;
using System.Linq;

namespace YourProjectName.Services
{
    public class CaseService
    {
        private readonly DataContext _context;
        private readonly StatusService _statusService;
        public CaseService(DataContext context, StatusService statusService)
        {
            _context = context;
            _statusService = statusService;
        }

        public async void AddCase(CommentEntity comment,string title, int doctorId, int patientId, int statusid)
        {
            var _status = await _statusService.GetAsync(x=>x.Id == statusid);
            var newCase = new CaseEntity
            {
                Comments = new List<CommentEntity> { comment },
                Title = title,
                DoctorId = doctorId,
                PatientId = patientId,
                Status = _status,
               
                
            };

            _context.Cases.Add(newCase);
            _context.SaveChanges();
        }

        public void ViewCases()
        {
            var cases = _context.Cases.ToList();

            foreach (var caseItem in cases)
            {
                Console.WriteLine($"Case ID: {caseItem.Id}");
                Console.WriteLine($"Comment: {caseItem.Comments}");
                Console.WriteLine($"Doctor Name: {caseItem.Doctor.FName}");
                Console.WriteLine($"Patient Name: {caseItem.Patient.PatientName}");
                Console.WriteLine($"Status: {caseItem.Status}");
                Console.WriteLine();
            }
        }
    }
}
