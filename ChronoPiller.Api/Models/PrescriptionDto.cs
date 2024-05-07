namespace ChronoPiller.Api.Models;

public class PrescriptionDto
{
    public int UserId { get; set; }
    public string DoctorName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? AcquireDate { get; set; }
    public List<PrescriptionItemDto> Items { get; set; } = new();
}