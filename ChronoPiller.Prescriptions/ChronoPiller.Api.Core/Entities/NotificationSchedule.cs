namespace ChronoPiller.Api.Core.Entities;

public class NotificationSchedule : BaseEntity
{
    public Guid PrescriptionItemId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public int DailyPillCount { get; set; }
    public int OccurenceCount { get; set; }
    public PrescriptionItem PrescriptionItem { get; set; }
}