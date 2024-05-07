using ChronoPiller.Api.Core.Entities;

namespace ChronoPiller.Api.Core.Interface;

public interface IPrescriptionRepository
{
    Task<Prescription> CreatePrescription(Prescription prescription);
    Task<Prescription> GetPrescriptionById(int id);
    Task<Prescription> UpdatePrescription(Prescription prescription);
    Task DeletePrescription(int id);
    Task<List<Prescription>> GetPrescriptionsByUserId(int userId);
}