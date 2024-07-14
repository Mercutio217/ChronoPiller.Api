using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;
using Mapster;

namespace ChronoPiller.Api.Services;

public class NotificationScheduleApiService : INotificationScheduleApiService
{
    private readonly INotificationScheduleService _service;

    public NotificationScheduleApiService(INotificationScheduleService service)
    {
        _service = service;
    }

    public async Task<List<NotificationScheduleDto?>> GetAllNotificationSchedulesByUserId(Guid userId)
    {
        var notificationSchedules = await _service.GetAllNotificationSchedulesByUserId(userId);
        var mapped = notificationSchedules.Adapt<List<NotificationScheduleDto?>>();
        return mapped;
    }

    public Task AddNotification(NotificationScheduleDto notification)
    {
        var domain = notification.Adapt<NotificationSchedule>();
        return _service.AddNotification(domain);
    }

    public Task UpdateNotification(NotificationScheduleDto notification)
    {
        var domain = notification.Adapt<NotificationSchedule>();
        return _service.UpdateNotification(domain);
    }

    public Task DeleteNotification(Guid id)
    {
        return _service.DeleteNotification(id);
    }
}