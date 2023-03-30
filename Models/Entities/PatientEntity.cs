using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models.Entities;

internal class PatientEntity
{
    public PatientEntity() 
    {
        PatientName = null!;
        Email = null!;
        Cases = new HashSet<CaseEntity>();
        Doctor = null!;
    }
    public int Id { get; set; }
    public string PatientName { get; set; } 
    public string Email { get; set; }

    
    public int DoctorId { get; set; }
    public DoctorEntity Doctor { get; set; }
    
    public ICollection<CaseEntity> Cases { get; set; }
}
