namespace ChronoPiller.Api.Models;

public record PrescriptionDto(
    Guid Id,
    Guid UserId, 
    string DoctorName, 
    DateTime StartDate,
    DateTime? AcquireDate, 
    List<PrescriptionItemDto> Items);
