﻿using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;
using System.Linq.Expressions;

namespace PatientCases.Services;

public class StatusService
{
    private readonly DataContext _context = new();
    public StatusService()
    {
        
    }
    public async Task InitializeAsync()
    {
        if (!await _context.Statuses.AnyAsync())
        {
            var list = new List<StatusEntity>()
            {
                new StatusEntity() {  StatusName = "Stable" },
                new StatusEntity() {  StatusName = "Critical" },
                new StatusEntity() {  StatusName = "Discharged" },
            };

            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateCaseStatusByTitle(string title, int statusId)
    {
        var theCase = await _context.Cases.SingleOrDefaultAsync(c => c.Title == title);

        if (theCase == null)
        {
            Console.WriteLine($"No case found with the title {title}");
            return;
        }

        theCase.StatusId = statusId;
        await _context.SaveChangesAsync();
        Console.WriteLine($"Status of case with title '{title}' has been updated to '{statusId}'");
    }

    public async Task<IEnumerable<StatusEntity>> GetAllAsync()
    {
        return await _context.Statuses.ToListAsync();
    }

    public async Task<StatusEntity> GetAsync(Expression<Func<StatusEntity, bool>> predicate)
    {
        var _statusEntity = await _context.Statuses.FirstOrDefaultAsync(predicate);
        return _statusEntity!;
    }
}
