using ChronoPiller.Api.Core.Entities;

namespace ChronoPiller.Api.Core.Interface;

public interface IPrescriptionService
{
    Task<Prescription> CreatePrescription(Prescription prescription);
    Task<Prescription> GetPrescriptionById(int id);
    Task<Prescription> UpdatePrescription(Prescription prescription);
    Task DeletePrescription(int id);
    Task<List<Prescription>> GetPrescriptionByUserId(int userId);
}