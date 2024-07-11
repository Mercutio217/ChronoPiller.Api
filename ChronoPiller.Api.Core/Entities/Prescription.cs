namespace ChronoPiller.Api.Core.Entities;

public class Prescription : BaseEntity
{
    public Guid UserId { get; set; }
    public string DoctorName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? AcquireDate { get; set; }
    public List<PrescriptionItem>? Items { get; set; }
}