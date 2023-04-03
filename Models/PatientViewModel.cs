using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCases.Models;

internal class PatientViewModel
{
    public int Id { get; set; }
    public string PatientName { get; set; }
    public string Email { get; set; }
    public string DoctorName { get; set; }
    public string Specialization { get; set; }
}
