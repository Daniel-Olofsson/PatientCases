using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models;
using PatientCases.Models.Entities;

namespace PatientCases.Services;

internal class DoctorService
{
    private readonly DataContext _context = new DataContext();

        public async Task<DoctorEntity> GetOrCreateAsync(DoctorModels model)
        {
        
        var _doctorEntity = await _context.Doctors.FirstOrDefaultAsync(x => x.LName == model.LName);
            if (_doctorEntity == null)
            {
            _doctorEntity = new DoctorEntity
            {
                    FName = model.FName,
                    LName = model.LName,
                    Specialization= model.Specialization
                };

                await _context.AddAsync(_doctorEntity);
                await _context.SaveChangesAsync();
            }

            return _doctorEntity;
        }

        public async Task<IEnumerable<DoctorEntity>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

}
