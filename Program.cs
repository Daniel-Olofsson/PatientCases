// Initialize services
using PatientCases.Models;
using PatientCases.Services;

var statusService = new StatusService();
var commentService = new CommentService();
var caseService = new CaseService();
var patientService = new PatientService();
var doctorService = new DoctorService();

await statusService.InitializeAsync();

// Create a new case
var patient = await patientService.GetOrCreateAsync(new PatientModels() { PatientName = "John", Email = "john@example.com" }, 1);
var doctor = await doctorService.GetOrCreateAsync(new DoctorModels() { FName = "Jack", LName = "Smith", Specialization = "Cardiology" }, 1);
var status = await statusService.GetAsync(s => s.StatusName == "Stable");

var newCase = await caseService.CreateCaseAsync("New case", patient.PatientId, doctor.DoctorId, status);
Console.WriteLine($"Created new case with id {newCase.CaseId}");