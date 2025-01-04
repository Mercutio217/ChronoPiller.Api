using ChronoPiller.Shared.Abstractions;
using ChronoPiller.Shared.Exceptions;

namespace ChronoPiller.Api.Core.Entities;

public class Prescription : ChronoBaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public string DoctorName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? AcquireDate { get; set; }
    public List<Medication>? Items { get; set; }

    public bool IsFinished { get; set; }

    public void TakePills(Guid medicationId, double amount)
    {
        var medication = Items?.FirstOrDefault(x => x.Id == medicationId);
        if (medication is null)
        {
            throw new MissingItemException(medicationId);
        }
        medication.TakePills(amount);
        
        if (Items?.All(item => item.IsFinished) == true)
        {
            IsFinished = true;
        }
    }
}