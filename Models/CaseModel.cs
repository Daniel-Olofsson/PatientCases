using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models;

internal class CaseModel
{
    public string Title { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public int PatientId { get; set; }
    public int StatusId { get; set; }
    public int DoctorId { get; set; }
}
