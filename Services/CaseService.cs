using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;
using System.Linq.Expressions;

namespace PatientCases.Services;

internal class CaseService
{
    private readonly DataContext _context= new DataContext();
    public async Task CreateAsync(CaseEntity caseEntity)
    {
        await _context.AddAsync(caseEntity);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<CaseEntity>> GetAllActiveAsync()
    {
        return await _context.Cases
            .Include(x=> x.Comments)
            .Include(x=> x.Patient)
            .Include(x=> x.Doctor)
            .Include(x=> x.Status)
            .Where(x=> x.StatusId!= 3)
            .OrderByDescending(x=>x.DateCreated)
            .ToListAsync();
    }
    public async Task<IEnumerable<CaseEntity>> GetAllAsync()
    {
        return await _context.Cases
            .Include(x => x.Comments)
            .Include(x => x.Patient)
            .Include(x => x.Doctor)
            .Include(x => x.Status)
            .OrderByDescending(x => x.DateCreated)
            .ToListAsync();
    }
    public async Task<CaseEntity> GetAsync(Expression<Func<CaseEntity, bool>> predicate)
    {
        var _entity = await _context.Cases
            .Include(x => x.Comments)
            .Include(x => x.Patient)
            .Include(x => x.Doctor)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(predicate);
        return _entity!;
    }
    public async Task UpdateCaseDoctorAsync(int caseId, DoctorEntity doctorEntity)
    {
        var _entity = await _context.Cases.FindAsync(caseId);
        if (_entity != null)
        {
            _entity.DateModified = DateTime.Now;
            _entity.Doctor = doctorEntity;
            _context.Update(_entity);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<CaseEntity> UpdateCaseStatusAsync(Expression<Func<CaseEntity, bool>> predicate)
    {
        var _caseEntity = await _context.Cases.FirstOrDefaultAsync(predicate);
        if (_caseEntity != null)
        {
            switch (_caseEntity.StatusId)
            {
                case 1:
                    _caseEntity.StatusId = 2;
                    _caseEntity.DateModified = DateTime.Now;
                    break;

                case 2:
                    _caseEntity.StatusId = 3;
                    _caseEntity.DateModified = DateTime.Now;
                    break;

                case 3:
                    _caseEntity.StatusId = 2;
                    _caseEntity.DateModified = DateTime.Now;
                    break;
            }

            _context.Update(_caseEntity);
            await _context.SaveChangesAsync();
        }

        return _caseEntity!;
    }

}
