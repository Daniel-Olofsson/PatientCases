using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;

namespace PatientCases.Services
{
    internal class CaseService
    {
        private readonly DataContext _context;
        private readonly PatientService _patientService;
        private readonly DoctorService _doctorService;

        public CaseService(DataContext context, PatientService patientService, DoctorService doctorService)
        {
            _context = context;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        public async Task<CaseEntity> CreateCaseAsync(string caseTitle, int patientId, int doctorId, CommentEntity comment, StatusEntity status)
        {
            var patient = await _patientService.GetOrCreateAsync(patientId);
            var doctor = await _doctorService.GetOrCreateAsync(doctorId);

            var newCase = new CaseEntity
            {
                Title = caseTitle,
                PatientId = patient.Id,
                DoctorId =  doctor.Id,
                Comments = new List <CommentEntity> { comment},
                StatusId = status.Id,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            await _context.Cases.AddAsync(newCase);
            await _context.SaveChangesAsync();

            return newCase;
        }

        public async Task<PatientEntity> GetOrCreatePatientAsync(PatientEntity patient)
        {
            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p => p.Email == patient.Email);

            if (existingPatient != null)
            {
                return existingPatient;
            }

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<PatientEntity> GetOrCreatePatientAsync(int patientId)
        {
            var existingPatient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);

            if (existingPatient != null)
            {
                return existingPatient;
            }

            throw new ArgumentException($"Patient with id {patientId} does not exist");
        }

        public async Task<DoctorEntity> GetOrCreateDoctorAsync(DoctorEntity doctor)
        {
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.FName == doctor.FName && d.LName == doctor.LName);

            if (existingDoctor != null)
            {
                return existingDoctor;
            }

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<DoctorEntity> GetOrCreateDoctorAsync(int doctorId)
        {
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            if (existingDoctor != null)
            {
                return existingDoctor;
            }

            throw new ArgumentException($"Doctor with id {doctorId} does not exist");
        }

        public async Task<CaseEntity> GetAsync(Expression<Func<CaseEntity, bool>> predicate)
        {
            return await _context.Cases
                .Include(c => c.Patient)
                .Include(c => c.Status)
                .Include(c => c.Doctor)
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
