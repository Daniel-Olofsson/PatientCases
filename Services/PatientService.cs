using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;

namespace PatientCases.Services
{
    internal class PatientService
    {
        private readonly DataContext _context;

        public PatientService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientEntity>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<PatientEntity> GetAsync(int id)
        {
            return await _context.Patients
                .Include(p => p.Doctor)
                .Include(p => p.Cases)
                    .ThenInclude(c => c.Doctor)
                .Include(p => p.Cases)
                    .ThenInclude(c => c.Status)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PatientEntity> GetOrCreateAsync(int patientId)
        {
            var existingPatient = await _context.Patients.FindAsync(patientId);

            if (existingPatient != null)
            {
                return existingPatient;
            }

            var newPatient = new PatientEntity
            {
                PatientId = patientId
            };

            await _context.Patients.AddAsync(newPatient);
            await _context.SaveChangesAsync();

            return newPatient;
        }
        public async Task<IEnumerable<PatientEntity>> FindAsync(Expression<Func<PatientEntity, bool>> predicate)
        {
            return await _context.Patients.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(PatientEntity patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PatientEntity patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await GetAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
