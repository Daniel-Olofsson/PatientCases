using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientCases.Context;

using PatientCases.Context;
using PatientCases.Models.Entities;
using PatientCases.Models;
using Microsoft.EntityFrameworkCore;

namespace PatientCases.Services;

internal class PatientService
{
    private readonly DataContext _context;
    public PatientService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientViewModel>> GetAllAsync()
    {
        return await _context.Patients
            .Include(p => p.Doctor)
            .Select(p => new PatientViewModel
            {
                PatientName = p.PatientName,
                Email = p.Email,
                DoctorName = $"{p.Doctor.FName} {p.Doctor.LName}",
                Specialization = p.Doctor.Specialization
            })
            .ToListAsync();
    }

}
