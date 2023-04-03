using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models.Entities;

public class CaseEntity
{
    public CaseEntity() 
    {
        Title = null!;
        DateCreated = DateTime.Now;
        DateModified = DateTime.Now;
        Patient = null!;
        Status = null!;
        Doctor = null!;

    }
    public Guid Id { get; set; }
   
    public string Title { get; set; }

    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public int PatientId { get; set; }
    public PatientEntity Patient { get; set; }
    

    public StatusEntity Status { get; set; }
    public int StatusId { get; set; }

    public int DoctorId { get; set; }
    public DoctorEntity Doctor { get; set; }

    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();
}
