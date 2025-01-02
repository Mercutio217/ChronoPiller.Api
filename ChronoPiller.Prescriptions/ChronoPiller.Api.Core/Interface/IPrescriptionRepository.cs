using ChronoPiller.Api.Core.Entities;

namespace ChronoPiller.Api.Core.Interface;

public interface IPrescriptionRepository
{
    Task<Prescription> CreatePrescription(Prescription prescription);
    Task<Prescription?> GetPrescriptionById(Guid id);
    Task<Prescription> UpdatePrescription(Prescription prescription);
    Task DeletePrescription(Guid id);
    Task<List<Prescription>> GetPrescriptionsByUserId(Guid userId);
    Task SubstractPrescriptionItemCount(Guid prescriptionItemId, int pillsCount);
}