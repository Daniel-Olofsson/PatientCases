
using PatientCases.Models;
using PatientCases.Models.Entities;
using PatientCases.Services;

var statusService = new StatusService();
var commentService = new CommentService();
var caseService = new CaseService();
var patientService = new PatientService();

var doctorService = new DoctorService();

// Initialize
await statusService.InitializeAsync();

//Comment = null!;
//Created = DateTime.Now;
//Case = null!;
//CaseId = Guid.NewGuid(); // initiera CaseId med ett GUID

var comment = new CommentEntity() {  };

var _doctorModel = new DoctorModels() { FName = "Leon", LName = "Lagergren", Specialization = "Nothing" };
var _patientModel = new PatientModels() { PatientName = "Hans", Email = "hans@hospital.se" };

await doctorService.GetOrCreateAsync(_doctorModel, 1);
await patientService.GetOrCreateAsync(_patientModel, 1);



//await commentService.InitializeCommentAsync(); // kör initiera comment-data
var doctorResult = await doctorService.GetAllAsync();
var statusResult = await statusService.GetAllAsync();
var patientResult = await patientService.GetAllAsync();

foreach (var patient in patientResult)
{
    Console.WriteLine($"{patient.PatientName}");

}
Console.WriteLine("----------------------------------------------------------");

foreach (var status in statusResult)
{
    Console.WriteLine($"{status.StatusName}");
}

Console.WriteLine("----------------------------------------------------------");


foreach (var doctor in doctorResult)
{
    Console.WriteLine($"{doctor.FName}");
}


Console.WriteLine("----------------------------------------------------------");

//var commentResult = await commentService.GetAllAsync();
//foreach (var comment in commentResult)
//{
//    Console.WriteLine($"{comment.Comment}");
//}

Console.ReadKey();