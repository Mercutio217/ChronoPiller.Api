using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Infrastructure.Database;
using ChronoPiller.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ChronoPiller.Infrastructure.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PrescriptionRepository(ApplicationDbContext applicationDbContext) => 
        _applicationDbContext = applicationDbContext;

    public async Task<Prescription> CreatePrescription(Prescription prescription)
    {
        _applicationDbContext.Add(prescription);
        await _applicationDbContext.SaveChangesAsync();
        return prescription;
    }

    public async Task<Prescription?> GetPrescriptionById(Guid id) =>
        await _applicationDbContext.Prescriptions
            .Include(pres => pres.Items)!
            .ThenInclude(it => it.Doses)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Prescription> UpdatePrescription(Prescription prescription)
    {
        _applicationDbContext.Entry(prescription).State = EntityState.Modified;
        await _applicationDbContext.SaveChangesAsync();
        return prescription;
    }

    public async Task DeletePrescription(Guid id)
    {
        Prescription? prescription = 
            await _applicationDbContext.Prescriptions.FirstOrDefaultAsync(p => p.Id == id).ConfigureAwait(false);
        if (prescription is null)
            throw new NotFoundException();
        _applicationDbContext.Prescriptions.Remove(prescription);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<List<Prescription>> GetPrescriptionsByUserId(Guid userId) =>
        await _applicationDbContext.Prescriptions
            .Include(pres => pres.Items)!
            .ThenInclude(it => it.Doses).Where(p => p.UserId == userId)
            .ToListAsync();

    public Task SubstractPrescriptionItemCount(Guid prescriptionItemId, int pillsCount)
    {
        var prescriptionItem = _applicationDbContext.Medications
            .FirstOrDefault(presItem => presItem.Id == prescriptionItemId);

        if (prescriptionItem == null)
        {
            throw new NotFoundException();
        }
        prescriptionItem.InitialBoxAmount -= pillsCount;

        return _applicationDbContext.SaveChangesAsync();
    }

    public async Task StartPrescription(Guid id, DateTime requestAcquireDate)
    {
        var prescription = await _applicationDbContext.Prescriptions
            .FirstOrDefaultAsync(pres => pres.Id == id);
        if (prescription is null)
        {
            throw new NotFoundException();
        }
        
        prescription.AcquireDate = requestAcquireDate;

        await _applicationDbContext.SaveChangesAsync();
    }
}
