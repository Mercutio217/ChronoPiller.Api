using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Models;

namespace ChronoPiller.Api.Interfaces;

public interface IPrescriptionApiService
{
    Task<Prescription> CreatePrescription(PrescriptionCreateDto prescription);
    Task<PrescriptionDto> GetPrescriptionById(Guid id);
    Task<List<PrescriptionDto>> GetPrescriptionsByUserId(Guid userId);     
    Task<PrescriptionDto> UpdatePrescription(PrescriptionDto prescription);
    Task DeletePrescription(Guid id);
}