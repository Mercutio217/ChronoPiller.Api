namespace ChronoPiller.Api.Models;

public record PrescriptionItemDto(
    Guid Id,
    string MedicationName,
    int BoxSize,
    int CurrentBoxCount,
    List<DosageDto> Doses);