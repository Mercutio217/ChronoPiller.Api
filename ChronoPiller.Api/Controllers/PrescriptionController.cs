using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Interfaces;
using ChronoPiller.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChronoPiller.Api.Controllers
{
    [Route("prescriptions")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionApiService _prescriptionApiService;

        public PrescriptionController(IPrescriptionApiService prescriptionApiService)
        {
            _prescriptionApiService = prescriptionApiService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPrescriptions(PrescriptionFilter filter)
        {
            return Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var prescription = await _prescriptionApiService.GetPrescriptionById(id);
            return Ok(prescription);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription(PrescriptionDto prescription)
        {
            var createdPrescription = await _prescriptionApiService.CreatePrescription(prescription);
            return CreatedAtAction(nameof(GetPrescriptionById), new { id = createdPrescription.Id }, createdPrescription);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            await _prescriptionApiService.DeletePrescription(id);
            return NoContent();
        }
    }
}