namespace ChronoPiller.Api.Core.Entities;

public class PrescriptionItem : BaseEntity
{
    public Guid PrescriptionId { get; set; }
    public string MedicationName { get; set; }
    public int BoxSize { get; set; }
    public int CurrentBoxCount { get; set; }
    public List<Dosage> Doses { get; set; } = new();
    
    public NotificationSchedule? NotificationSchedule { get; set; }
}