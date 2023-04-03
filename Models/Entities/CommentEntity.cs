namespace PatientCases.Models.Entities;

public class CommentEntity
{
    public CommentEntity() 
    {
        Comment = null!;
        Created = DateTime.Now;
        Case = null!;
        CaseId = Guid.NewGuid(); // initiera CaseId med ett GUID
    }
    public Guid Id { get; set; }
    public string Comment { get; set; } 
    public DateTime Created { get; set; }

    
    public Guid CaseId { get; set; }
    public CaseEntity Case { get; set; } 
}
