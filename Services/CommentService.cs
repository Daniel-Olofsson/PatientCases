using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;

namespace PatientCases.Services;

internal class CommentService
{
    private readonly DataContext _context = new DataContext();
    // private readonly CaseService _caseService = new CaseService();

    //public async Task InitializeCommentAsync() // lägg till demo data i "comment"
    //{
    //    if (!await _context.Comments.AnyAsync())
    //    {
    //        new CommentService() { }

    //        //currentUser = await menuService.CreateUserAsync();

    //        var list = new List<CommentEntity>()
    //        {
    //            new StatusEntity() { StatusName = "Stable" },
    //            new StatusEntity() { StatusName = "Critical" },
    //            new StatusEntity() { StatusName = "Discharged" },
    //        };

    //        _context.AddRange(list);
    //        await _context.SaveChangesAsync();
    //    }
    //}



    public async Task CreateAsync(CommentEntity commentEntity)
    {
        // Lägg till: om CaseID från _caseService inte är null?
        _context.Add(commentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }
        
}
