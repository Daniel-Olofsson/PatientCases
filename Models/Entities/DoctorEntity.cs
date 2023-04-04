using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models.Entities;

public class DoctorEntity
{
    public DoctorEntity() 
    {
        FName = null!;
        LName = null!;
        Specialization = null!;
        Patients = new HashSet<PatientEntity>();
        Cases = new HashSet<CaseEntity>();

    }
    [Key]
    public int Id { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string Specialization { get; set; }
    public ICollection<PatientEntity> Patients { get; set; } 
    public ICollection<CaseEntity> Cases { get; set; } 
}
