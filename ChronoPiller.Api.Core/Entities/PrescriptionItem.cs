namespace ChronoPiller.Api.Core.Entities;

public class PrescriptionItem : BaseEntity
{
    public string MedicationName { get; set; }
    public double Dose { get; set; }
    public List<TimeSpan> Times { get; set; } = new();
}