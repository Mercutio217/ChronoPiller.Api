using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Core.Interface;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;

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

    public async Task<Prescription> CreatePrescription(PrescriptionDto prescription)
    {
        var model = new Prescription()
        {
            Items = prescription.Items?.Select(pres => new PrescriptionItem()
            {
                Times = new(),
                Dose = 1,
                MedicationName = "test"
            }).ToList(),
            AcquireDate = prescription.AcquireDate,
            DoctorName = prescription.DoctorName,
            StartDate = prescription.StartDate,
            UserId = prescription.UserId
        };
        return await _prescriptionService.CreatePrescription(model);
    }

    public async Task<Prescription> GetPrescriptionById(int id) => 
        await Execute(async () => await _prescriptionService.GetPrescriptionById(id));

    public async Task<List<Prescription>> GetPrescriptionByUserId(int userId) => 
        await Execute(async () => await _prescriptionService.GetPrescriptionByUserId(userId));

    public async Task<Prescription> UpdatePrescription(PrescriptionDto prescription)
    {
        var model = new Prescription()
        {
            Items = prescription.Items?.Select(pres => new PrescriptionItem()
            {
                Times = new(),
                Dose = 1,
                MedicationName = "test"
            }).ToList(),
            AcquireDate = prescription.AcquireDate,
            DoctorName = prescription.DoctorName,
            StartDate = prescription.StartDate,
            UserId = prescription.UserId
        };
        return await Execute(async () => await _prescriptionService.UpdatePrescription(model));
    }

    public async Task DeletePrescription(int id) => 
        await Execute(async () => await _prescriptionService.DeletePrescription(id));

    private async Task Execute(Func<Task> function)
    {
        try
        {
            await function();
        }
        // catch (ApplicationValidationException validationException)
        // {
        //     logger.LogValidationException(validationException);
        //     throw;
        // }
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
        // catch (ApplicationValidationException validationException)
        // {
        //     logger.LogValidationException(validationException);
        //     throw;
        // }
        catch (Exception exception)
        {
            _logger.LogError("Critical error during execution! {exception}", exception.Message);
            throw;
        }
    }
}