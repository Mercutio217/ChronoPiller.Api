using ChronoPiller.Api.Core.Entities;

namespace ChronoPiller.Api.Core.Interface;

public interface IPrescriptionService
{
    Task<Prescription> CreatePrescription(Prescription prescription);
    Task<Prescription> GetPrescriptionById(Guid id);
    Task<Prescription> UpdatePrescription(Prescription prescription);
    Task DeletePrescription(Guid id);
    Task<List<Prescription>> GetPrescriptionByUserId(Guid userId);
    Task SubstractPrescriptionItemCount(Guid prescriptionItemId, int pillsCount);
    Task StartPrescription(Guid id, DateTime requestAcquireDate);
}