using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ChronoPiller.Infrastructure.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PrescriptionRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Prescription> CreatePrescription(Prescription prescription)
    {
        _applicationDbContext.Add(prescription);
        await _applicationDbContext.SaveChangesAsync();
        return prescription;
    }

    public async Task<Prescription> GetPrescriptionById(int id)
    {
        return await _applicationDbContext.Prescriptions.FirstAsync(p => p.Id == id);
    }

    public async Task<Prescription> UpdatePrescription(Prescription prescription)
    {
        _applicationDbContext.Entry(prescription).State = EntityState.Modified;
        await _applicationDbContext.SaveChangesAsync();
        return prescription;
    }

    public async Task DeletePrescription(int id)
    {
        var prescription = await _applicationDbContext.Prescriptions.FindAsync(id);
        _applicationDbContext.Prescriptions.Remove(prescription);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<List<Prescription>> GetPrescriptionsByUserId(int userId)
    {
        return await _applicationDbContext.Prescriptions.Where(p => p.UserId == userId).ToListAsync();
    }
}