using ChronoPiller.Api.Core.Entities;

namespace ChronoPiller.Api.Models;

public record NotificationScheduleDto(Guid PrescriptionItemId, DateTime DateStart, DateTime? DateEnd, int DailyPillCount, int OccurenceCount, PrescriptionItemDto PrescriptionItem);