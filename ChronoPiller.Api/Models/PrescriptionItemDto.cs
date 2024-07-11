namespace ChronoPiller.Api.Models;

public record PrescriptionItemDto(
    string MedicationName,
    
    List<DosageDto> Doses);