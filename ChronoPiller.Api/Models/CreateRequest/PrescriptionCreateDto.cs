namespace ChronoPiller.Api.Models.CreateRequest;

public record PrescriptionCreateDto(Guid UserId,
    string DoctorName,
    DateTime StartDate,
    DateTime? AcquireDate,
    List<PrescriptionCreateItemDto> Items);