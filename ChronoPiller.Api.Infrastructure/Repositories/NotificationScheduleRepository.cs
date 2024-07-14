using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ChronoPiller.Infrastructure.Repositories;

public class NotificationScheduleRepository : INotificationScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationSchedule?>> GetAllNotificationSchedulesByUserId(Guid userId)
    {
        var result = await _context.Prescriptions.Where(p => p.UserId == userId)
            .SelectMany(p => p.Items.Select(i => i.NotificationSchedule))
            .Include(it => it.PrescriptionItem).ThenInclude(item => item.Doses)
            .Where(it => it != null)
            .ToListAsync();
        return result;
    }

    public async Task AddNotification(NotificationSchedule notification)
    {
        var prescription = _context.Prescriptions.FirstOrDefault(item => item.Items.Any(i => i.Id == notification.PrescriptionItemId));
        var endDate = notification.DateStart.AddDays(notification.OccurenceCount);
        if (prescription == null)
        {
            return;
        }
        if (prescription.AcquireDate == null)
        {
            prescription.AcquireDate = notification.DateStart;
        }
        notification.DateEnd = endDate;
        _context.NotificationSchedules.Add(notification);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateNotification(NotificationSchedule notification)
    {
        _context.Update(notification);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteNotification(Guid id)
    {
        var notification = await _context.NotificationSchedules.FirstOrDefaultAsync(n => n.Id == id);
        _context.NotificationSchedules.Remove(notification);
        await _context.SaveChangesAsync();
    }
}