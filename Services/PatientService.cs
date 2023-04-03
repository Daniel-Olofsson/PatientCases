using PatientCases.Context;
using PatientCases.Models.Entities;
using PatientCases.Models;
using Microsoft.EntityFrameworkCore;

namespace PatientCases.Services;

internal class PatientService
{
    private readonly DataContext _context = new DataContext();

    public async Task<PatientEntity> GetOrCreateAsync(PatientModels model)
    {

        var _patientEntity = await _context.Patients.FirstOrDefaultAsync(x => x.PatientName == model.PatientName);
        if (_patientEntity == null)
        {
            _patientEntity = new PatientEntity()
            {
                PatientName = model.PatientName,
                Email = model.Email //,
                // Doctor = 
            };

            await _context.AddAsync(_patientEntity);
            await _context.SaveChangesAsync();
        }

        return _patientEntity;
    }

    public async Task<IEnumerable<PatientEntity>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }


}
