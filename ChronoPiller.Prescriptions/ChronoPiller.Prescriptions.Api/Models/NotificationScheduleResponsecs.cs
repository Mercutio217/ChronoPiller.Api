using ChronoPiller.Api.Models.CreateRequest;

namespace ChronoPiller.Api.Models;

public record NotificationScheduleResponse
{
    public NotificationScheduleResponse(Guid PrescriptionItemId, DateTime DateStart, DateTime? DateEnd, int DailyPillCount, int OccurenceCount)
    {
        this.PrescriptionItemId = PrescriptionItemId;
        this.DateStart = DateStart;
        this.DateEnd = DateEnd;
        this.DailyPillCount = DailyPillCount;
        this.OccurenceCount = OccurenceCount;
    }

    public Guid PrescriptionItemId { get; init; }
    public DateTime DateStart { get; init; }
    public DateTime? DateEnd { get; init; }
    public int DailyPillCount { get; init; }
    public int OccurenceCount { get; init; }
    public PrescriptionCreateItemDto PrescriptionCreateItem { get; set; }

}