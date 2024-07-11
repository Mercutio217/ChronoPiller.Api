namespace ChronoPiller.Api.Core.Entities;

public class Dosage
{
    public TimeSpan DosageTime { get; set; }
    public int DosageCount { get; set; }
    public double DosageAmount { get; set; }
}