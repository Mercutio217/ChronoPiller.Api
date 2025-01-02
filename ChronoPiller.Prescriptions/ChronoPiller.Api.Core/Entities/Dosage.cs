namespace ChronoPiller.Api.Core.Entities;

public class Dosage : BaseEntity
{
    public Guid PrescriptionItemId { get; set; }
    public TimeSpan DosageTime { get; set; }
    public double DosageAmount { get; set; }
}