namespace ChronoPiller.Api.Models;

public class PrescriptionItemDto
{
    public string MedicationName { get; set; }
    public double Dose { get; set; }
    public List<string> Times { get; set; } = new();
}