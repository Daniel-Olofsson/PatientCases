using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientCases.Services;

internal class CommentService
{
    private readonly DataContext _context;

    public CommentService(DataContext context)
    {
        _context = context;
    }

    public async Task<CommentEntity> CreateAsync(CommentEntity comment)
    {
        // add the new comment to the context and save changes
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<CommentEntity> GetAsync(Guid id)
    {
        // find the comment with the given id
        var comment = await _context.Comments.FindAsync(id);

        // return null if not found
        return comment;
    }

    public async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        // get all comments from the context
        var comments = await _context.Comments.ToListAsync();

        return comments;
    }

    public async Task<IEnumerable<CommentEntity>> GetAllByCaseIdAsync(Guid caseId)
    {
        // get all comments for the given case id
        var comments = await _context.Comments
            .Where(c => c.CaseId == caseId)
            .ToListAsync();

        return comments;
    }

    public async Task<bool> UpdateAsync(CommentEntity comment)
    {
        // check if the comment exists in the context
        var existingComment = await _context.Comments.FindAsync(comment.Id);
        if (existingComment == null)
        {
            return false;
        }

        // update the existing comment properties
        existingComment.Comment = comment.Comment;
        existingComment.Created = comment.Created;

        // save changes to the context
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        // find the comment with the given id
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return false;
        }

        // remove the comment from the context
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return true;
    }
}
