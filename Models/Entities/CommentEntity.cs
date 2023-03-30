using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models.Entities;

internal class CommentEntity
{
    public CommentEntity() 
    {
        Comment = null!;
        Created = DateTime.Now;
        Case = null!;
    }
    public int Id { get; set; }
    public string Comment { get; set; } 
    public DateTime Created { get; set; }

    
    public Guid CaseId { get; set; }
    public CaseEntity Case { get; set; } 
}
