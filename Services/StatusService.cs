﻿using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Services;

internal class StatusService
{
    private readonly DataContext _context = new();
    public async Task InitializeAsync()
    {
        if (!await _context.Statuses.AnyAsync())
        {
            var list = new List<StatusEntity>()
            {
                new StatusEntity() { StatusName = "Stable" },
                new StatusEntity() { StatusName = "Critical" },
                new StatusEntity() { StatusName = "Discharged" },
            };

            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
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