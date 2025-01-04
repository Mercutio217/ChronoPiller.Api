using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;
using ChronoPiller.Api.Models.CreateRequest;
using Mapster;

namespace ChronoPiller.Api.Services;

public class PrescriptionApiService : IPrescriptionApiService
{
    private readonly IPrescriptionService _prescriptionService;
    private readonly ILogger<PrescriptionApiService> _logger;

    public PrescriptionApiService(IPrescriptionService prescriptionService, ILogger<PrescriptionApiService> logger)
    {
        _prescriptionService = prescriptionService;
        _logger = logger;
    }

    public Task<Prescription> CreatePrescription(PrescriptionCreateDto prescription)
    {
        return Execute(async () =>
        {
            Prescription model =
                prescription.Adapt<Prescription>();
            return await _prescriptionService.CreatePrescription(model);
        });
    }

    public async Task<PrescriptionDto> GetPrescriptionById(Guid id) => 
        await Execute(async () => (await _prescriptionService.GetPrescriptionById(id)).Adapt<PrescriptionDto>());

    public async Task<List<PrescriptionDto>> GetPrescriptionsByUserId(Guid userId)
    {
        return await Execute(async () =>
        {
            List<Prescription> result = await _prescriptionService.GetPrescriptionByUserId(userId);
            return result.Adapt<List<PrescriptionDto>>();
        });
    }

    public async Task<List<PrescriptionDto>> GetPrescriptionByUserId(Guid userId) => 
        await Execute(async () => (await _prescriptionService.GetPrescriptionByUserId(userId)).Adapt<List<PrescriptionDto>>());

    public async Task<PrescriptionDto> UpdatePrescription(PrescriptionDto prescription)
    {
        Prescription model = prescription.Adapt<Prescription>();
        return await Execute(async () => (await _prescriptionService.UpdatePrescription(model)).Adapt<PrescriptionDto>());
    }

    public async Task DeletePrescription(Guid id) => 
        await Execute(async () => await _prescriptionService.DeletePrescription(id));

    public async Task SubtractPills(Guid prescriptionItemId, int pillsCount)
    {
        await _prescriptionService.SubstractPrescriptionItemCount(prescriptionItemId, pillsCount);
    }

    public async Task StartPrescription(Guid id, DateTime requestAcquireDate)
    {
        await _prescriptionService.StartPrescription(id, requestAcquireDate);
    }

    private async Task Execute(Func<Task> function)
    {
        try
        {
            await function();
        }
        catch (Exception exception)
        {
            _logger.LogError("Critical error during execution! {exception}", exception.Message);
            throw;
        }
    }

    private async Task<T> Execute<T>(Func<Task<T>> function)
    {
        try
        {
            return await function();
        }
        catch (Exception exception)
        {
            _logger.LogError("Critical error during execution! {exception}", exception.Message);
            throw;
        }
    }
}