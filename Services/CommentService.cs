using Microsoft.EntityFrameworkCore;
using PatientCases.Context;
using PatientCases.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientCases.Services;

public class CommentService
{
    private readonly DataContext _context;

    public CommentService(DataContext context)
    {
        _context = context;
    }

    public void AddComment(string comment, Guid caseId)
    {
        var caseEntity = _context.Cases.FirstOrDefault(c => c.Id == caseId);

        if (caseEntity == null)
        {
            throw new ArgumentException($"No case found with ID {caseId}");
        }

        var commentEntity = new CommentEntity
        {
            Comment = comment,
            CaseId = caseId
        };

        _context.Comments.Add(commentEntity);
        _context.SaveChanges();
    }

    public void ViewComments(Guid caseId, int statusId)
    {
        var caseEntity = _context.Cases.FirstOrDefault(c => c.Id == caseId);

        if (caseEntity == null)
        {
            throw new ArgumentException($"No case found with ID {caseId}");
        }

        Console.WriteLine($"Comments for case {caseEntity.Id} with status {statusId}:");
        Console.WriteLine();

        var comments = _context.Comments
            .Join(_context.Cases,
                  comment => comment.CaseId,
                  caseEntity => caseEntity.Id,
                  (comment, caseEntity) => new { Comment = comment, Case = caseEntity })
            .Where(c => c.Case.StatusId == statusId && c.Comment.CaseId == caseId)
            .OrderByDescending(c => c.Comment.Created)
            .ToList();

        if (comments.Any())
        {
            foreach (var comment in comments)
            {
                Console.WriteLine($"[{comment.Comment.Created}] {comment.Comment.Comment}");
            }
        }
        else
        {
            Console.WriteLine("No comments found.");
        }
    }
}