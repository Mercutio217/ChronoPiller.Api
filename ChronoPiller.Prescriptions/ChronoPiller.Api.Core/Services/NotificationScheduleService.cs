using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;

namespace ChronoPiller.Api.Core.Services
{
    public class NotificationScheduleService : INotificationScheduleService
    {
        private readonly INotificationScheduleRepository _repository;

        public NotificationScheduleService(INotificationScheduleRepository repository)
        {
            _repository = repository;
        }

        public Task<List<NotificationSchedule?>> GetAllNotificationSchedulesByUserId(Guid userId)
        {
            return _repository.GetAllNotificationSchedulesByUserId(userId);
        }

        public Task AddNotification(NotificationSchedule notification)
        {
            return _repository.AddNotification(notification);
        }

        public Task UpdateNotification(NotificationSchedule notification)
        {
            return _repository.UpdateNotification(notification);
        }

        public Task DeleteNotification(Guid id)
        {
            return _repository.DeleteNotification(id);
        }
    }
}