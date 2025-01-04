using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;
using ChronoPiller.Api.Models.CreateRequest;
using ChronoPiller.Shared.Abstractions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPiller.Api.Controllers
{
    [ApiController]
    [Route("prescriptions")]
    public class PrescriptionsController : ChronoBaseController
    {
        private readonly IPrescriptionApiService _prescriptionApiService;

        public PrescriptionsController(IPrescriptionApiService prescriptionApiService)
        {
            _prescriptionApiService = prescriptionApiService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPrescriptionById(Guid id)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                var prescription = await _prescriptionApiService.GetPrescriptionById(id);
                return Ok(prescription);
            });
        }

        [HttpGet("prescriptions")]
        [Authorize]
        public async Task<IActionResult> GetPrescriptionsByUserId(Guid id)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                List<PrescriptionDto> prescription = await _prescriptionApiService.GetPrescriptionsByUserId(id);
                return Ok(new Result<List<PrescriptionDto>> { Items = prescription });
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription(PrescriptionCreateDto prescription)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                var createdPrescription = await _prescriptionApiService.CreatePrescription(prescription);
                return Created($"/prescriptions/{createdPrescription.Id}", createdPrescription);
            });
        }
        
        [HttpPut("prescriptions/{id}/acquire-date")]
        public async Task<IActionResult> StartPrescription(Guid id, StartPrescriptionRequest request)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await _prescriptionApiService.StartPrescription(id, request.AcquireDate);
                return Ok();
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(Guid id)
        {
            return await ExecuteWithErrorHandling(async () =>
            {
                await _prescriptionApiService.DeletePrescription(id);
                return NoContent();
            });
        }
    }
}