namespace ChronoPiller.Api.Core.Entities;

public class PrescriptionItem : BaseEntity
{
    public Guid PrescriptionId { get; set; }
    public string MedicationName { get; set; }
    public List<Dosage> Doses { get; set; } = new();
}