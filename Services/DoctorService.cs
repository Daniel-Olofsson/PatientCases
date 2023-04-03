using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientCases.Services
{
    internal class DoctorService
    {
        private readonly DataContext _context;

        public DoctorService(DataContext context)
        {
            _context = context;
        }

        public async Task<DoctorEntity> CreateAsync(DoctorEntity doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<DoctorEntity> GetByIdAsync(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DoctorEntity>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<IEnumerable<DoctorEntity>> GetBySpecializationAsync(string specialization)
        {
            return await _context.Doctors.Where(d => d.Specialization == specialization).ToListAsync();
        }
        public async Task<DoctorEntity> GetOrCreateAsync(int doctorId)
        {
            var existingDoctor = await _context.Doctors.FindAsync(doctorId);

            if (existingDoctor != null)
            {
                return existingDoctor;
            }

            throw new ArgumentException($"Doctor with id {doctorId} does not exist");
        }

        public async Task<DoctorEntity> GetOrCreateAsync(string firstName, string lastName)
        {
            var existingDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.FName == firstName && d.LName == lastName);

            if (existingDoctor != null)
            {
                return existingDoctor;
            }

            var newDoctor = new DoctorEntity
            {
                FName = firstName,
                LName = lastName
            };

            await _context.Doctors.AddAsync(newDoctor);
            await _context.SaveChangesAsync();

            return newDoctor;
        }


        public async Task UpdateAsync(DoctorEntity doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor));

            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctor == null)
                throw new InvalidOperationException($"Doctor with Id {id} not found.");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
