using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models;
using PatientCases.Models.Entities;

namespace PatientCases.Services;

internal class DoctorService
{
    private readonly DataContext _context = new DataContext();

        public async Task<DoctorEntity> GetOrCreateAsync(DoctorModels model, int id)
        {
        
        var _doctorEntity = await _context.Doctors.FirstOrDefaultAsync(x => x.LName == model.LName);
            if (_doctorEntity == null)
            {
            var _patientEntity = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            _doctorEntity = new DoctorEntity
            {
                    FName = model.FName,
                    LName = model.LName,
                    Specialization= model.Specialization,
                    Patients = new List<PatientEntity> { _patientEntity }
                };

                await _context.AddAsync(_doctorEntity);
                await _context.SaveChangesAsync();
            }

            return _doctorEntity;
        }

        public async Task<IEnumerable<DoctorEntity>> GetAllAsync()
        {
            return await _context.Doctors.Include(d => d.Patients).ToListAsync();
        }

}
