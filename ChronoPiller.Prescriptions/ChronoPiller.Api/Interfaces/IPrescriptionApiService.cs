using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Models;
using ChronoPiller.Api.Models.CreateRequest;

namespace ChronoPiller.Api.Interfaces;

public interface IPrescriptionApiService
{
    Task<Prescription> CreatePrescription(PrescriptionCreateDto prescription);
    Task<PrescriptionDto> GetPrescriptionById(Guid id);
    Task<List<PrescriptionDto>> GetPrescriptionsByUserId(Guid userId);     
    Task<PrescriptionDto> UpdatePrescription(PrescriptionDto prescription);
    Task DeletePrescription(Guid id);
    Task SubtractPills(Guid prescriptionItemId, int pillsCount);
}