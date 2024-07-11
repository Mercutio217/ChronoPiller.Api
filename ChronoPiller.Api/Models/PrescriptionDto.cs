namespace ChronoPiller.Api.Models;

public record PrescriptionDto(
    Guid UserId, 
    string DoctorName, 
    DateTime StartDate,
    DateTime? AcquireDate, 
    List<PrescriptionItemDto> Items);
