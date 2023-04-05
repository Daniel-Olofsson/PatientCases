using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models.Entities;

public class CaseEntity
{
    public CaseEntity() 
    {
        Id = Guid.NewGuid();
        Title = null!;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
        Patient = null!;
        Status = null!;
        Doctor = null!;

    }
    [Key]
    public Guid Id { get; set; }
   
    public string Title { get; set; }

    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    [ForeignKey("Patient")]
    public int PatientId { get; set; }
    public PatientEntity Patient { get; set; }

    
    
    public int StatusId { get; set; } = 1;
    [ForeignKey(nameof(StatusId))]
    public StatusEntity Status { get; set; }
    
    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }
    public DoctorEntity Doctor { get; set; }

    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();
}
