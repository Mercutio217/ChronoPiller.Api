using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Models;

namespace ChronoPiller.Api.Interfaces;

public interface IPrescriptionApiService
{
    Task<Prescription> CreatePrescription(PrescriptionDto prescription);
    Task<Prescription> GetPrescriptionById(int id);
    Task<List<Prescription>> GetPrescriptionByUserId(int userId);     
    Task<Prescription> UpdatePrescription(PrescriptionDto prescription);
    Task DeletePrescription(int id);
}