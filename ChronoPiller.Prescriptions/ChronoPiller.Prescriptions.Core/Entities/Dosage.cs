using ChronoPiller.Shared.Abstractions;

namespace ChronoPiller.Api.Core.Entities;

public class Dosage : ChronoBaseEntity<Guid>
{
    public Guid PrescriptionItemId { get; set; }
    public TimeSpan DosageTime { get; set; }
    public double DosageAmount { get; set; }
}