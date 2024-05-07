using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;

namespace ChronoPiller.Api.Core.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<Prescription> CreatePrescription(Prescription prescription)
    {
        return await _prescriptionRepository.CreatePrescription(prescription);
    }

    public async Task<Prescription> GetPrescriptionById(int id)
    {
        return await _prescriptionRepository.GetPrescriptionById(id);
    }

    public Task<Prescription> UpdatePrescription(Prescription prescription)
    {
        throw new NotImplementedException();
    }

    public async Task DeletePrescription(int id)
    {
        await _prescriptionRepository.DeletePrescription(id);
    }

    public async Task<List<Prescription>> GetPrescriptionByUserId(int userId)
    {
        return await _prescriptionRepository.GetPrescriptionsByUserId(userId);
    }
}