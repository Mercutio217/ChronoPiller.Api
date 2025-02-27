﻿using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Shared.Exceptions;

namespace ChronoPiller.Api.Core.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public Task<Prescription> CreatePrescription(Prescription prescription) => 
        _prescriptionRepository.CreatePrescription(prescription);

    public async Task<Prescription> GetPrescriptionById(Guid id)
    {
        Prescription? result = 
            await _prescriptionRepository.GetPrescriptionById(id);
        if (result is null)
            throw new NotFoundException();

        return result;
    }

    public Task<Prescription> UpdatePrescription(Prescription prescription)
    {
        throw new NotImplementedException();
    }

    public Task DeletePrescription(Guid id) => 
        _prescriptionRepository.DeletePrescription(id);

    public Task<List<Prescription>> GetPrescriptionByUserId(Guid userId) => 
        _prescriptionRepository.GetPrescriptionsByUserId(userId);

    public Task SubstractPrescriptionItemCount(Guid prescriptionItemId, int pillsCount)
    {
        return _prescriptionRepository.SubstractPrescriptionItemCount(prescriptionItemId, pillsCount);
    }

    public Task StartPrescription(Guid id, DateTime requestAcquireDate)
    {
        return _prescriptionRepository.StartPrescription(id, requestAcquireDate);
    }
}