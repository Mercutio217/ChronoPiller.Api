using ChronoPiller.Shared.Abstractions;

namespace ChronoPiller.Api.Core.Entities;

public class Medication : ChronoBaseEntity<Guid>
{
    public Guid PrescriptionId { get; set; }
    public string? MedicationName { get; set; }
    public double BoxPillCount { get; set; }
    public double CurrentBoxPillCount { get; set; }
    public int InitialBoxAmount { get; set; }
    public int CurrentBoxAmount { get; set; }
    public List<Dosage> Doses { get; set; } = new();
    public bool IsFinished { get; set; }
    
    public void TakePills(double dosage)
    {
        var pillCount = CurrentBoxPillCount - dosage;
        if (pillCount <= 0)
        {
            --CurrentBoxAmount;
            if (CurrentBoxAmount > 0)
            {
                CurrentBoxPillCount = InitialBoxAmount + pillCount;
            }
            else
            {
                CurrentBoxPillCount = 0;
                IsFinished = true;
            }
        }
    }
}