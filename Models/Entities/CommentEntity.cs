﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientCases.Models.Entities;

public class CommentEntity
{
    public CommentEntity() 
    {
        Comment = null!;
        Created = DateTime.Now;
        Case = null!;
        CaseId = Guid.NewGuid(); // initiera CaseId med ett GUID
    }
    [Key]
    public Guid Id { get; set; }
    public string Comment { get; set; } 
    public DateTime Created { get; set; }

    [ForeignKey("Case")]
    public Guid CaseId { get; set; }
    public CaseEntity Case { get; set; } 
}
