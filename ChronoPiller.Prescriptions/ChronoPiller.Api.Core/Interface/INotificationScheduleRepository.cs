using ChronoPiller.Api.Core.Entities;

namespace ChronoPiller.Api.Core.Interface;

public interface INotificationScheduleRepository
{
    Task<List<NotificationSchedule?>> GetAllNotificationSchedulesByUserId(Guid userId);
    Task AddNotification(NotificationSchedule notification);
    Task UpdateNotification(NotificationSchedule notification);
    Task DeleteNotification(Guid id);
}