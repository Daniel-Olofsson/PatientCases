using Microsoft.EntityFrameworkCore;
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
                StatusId = _status.Id,
                //Status = new StatusEntity { Id = _status.Id, StatusName=_status.StatusName }
               
                
            };

            _context.Cases.Add(newCase);
            _context.SaveChanges();
        }

        public void ViewCases()
        {
            var cases = _context.Cases
            .Include(c => c.Patient)
            .Include(c => c.Doctor)
            .Include(c => c.Status)
            .Select(c => new CaseEntity
            {
                Id = c.Id,
                Patient = c.Patient,
                Doctor = c.Doctor,
                DateCreated = c.DateCreated,
                Title = c.Title,
                Comments = c.Comments,
                Status = new StatusEntity { StatusName = c.Status.StatusName }
            })
            .ToList();

            foreach (var caseItem in cases)
            {
                Console.WriteLine($"Case ID: {caseItem.Id}");

                if (caseItem.Comments != null)
                {
                    Console.WriteLine($"Comment(s):");
                    foreach (var comment in caseItem.Comments)
                    {
                        Console.WriteLine($"- {comment.Comment}");
                    }
                }
                else
                {
                    Console.WriteLine("No comments.");
                }
                Console.WriteLine($"Comment: {caseItem.Comments}");
                Console.WriteLine($"Doctor Name: {caseItem.Doctor.FName}");
                Console.WriteLine($"Patient Name: {caseItem.Patient.PatientName}");
                if (caseItem.Status != null)
                {
                    Console.WriteLine($"Status: {caseItem.Status.StatusName}");
                }
                else
                {
                    Console.WriteLine("No status assigned.");
                }
                Console.WriteLine();
            }
        }
    }
}
