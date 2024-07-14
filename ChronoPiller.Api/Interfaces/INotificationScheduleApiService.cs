using ChronoPiller.Api.Models;

namespace ChronoPiller.Api.Interfaces;

public interface INotificationScheduleApiService
{
    Task<List<NotificationScheduleDto?>> GetAllNotificationSchedulesByUserId(Guid userId);
    Task AddNotification(NotificationScheduleDto notification);
    Task UpdateNotification(NotificationScheduleDto notification);
    Task DeleteNotification(Guid id);
}
