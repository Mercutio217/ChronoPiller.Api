namespace ChronoPiller.Api.Models;

public record PrescriptionCreateDto(
    Guid UserId, 
    string DoctorName, 
    DateTime StartDate, 
    DateTime? AcquireDate, 
    List<PrescriptionItemDto> Items) : PrescriptionDto(
        UserId, 
        DoctorName, 
        StartDate, 
        AcquireDate, 
        Items);