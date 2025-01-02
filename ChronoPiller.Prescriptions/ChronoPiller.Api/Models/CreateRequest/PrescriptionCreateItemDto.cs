namespace ChronoPiller.Api.Models.CreateRequest;

public record PrescriptionCreateItemDto(
    string MedicationName,
    int BoxSize,
    int CurrentBoxCount,
    List<DosageDto> Doses);