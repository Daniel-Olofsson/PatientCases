using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PatientCases.Models;

internal class DoctorModels
{
    public string FName { get; set; }
    public string LName { get; set; }
    public string Specialization { get; set; }

}