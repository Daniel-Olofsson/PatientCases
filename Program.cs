
using PatientCases.Services;

var statusService = new StatusService();

// Initialize
await statusService.InitializeAsync();

var statusResult = await statusService.GetAllAsync();
foreach (var status in statusResult)
{
    Console.WriteLine($"{status.StatusName}");
}

Console.WriteLine(statusResult);