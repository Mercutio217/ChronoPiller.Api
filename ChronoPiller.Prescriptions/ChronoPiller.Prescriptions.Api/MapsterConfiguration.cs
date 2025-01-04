using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Models;
using ChronoPiller.Api.Models.CreateRequest;
using Mapster;

namespace ChronoPiller.Api;

public static class MapsterConfiguration
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<PrescriptionCreateDto, Prescription>
            .NewConfig()
            .Map(dest => dest.Items, src => src.Items.Select(item => new Medication()
            {
                Doses = item.Doses.Adapt<List<Dosage>>(),
                MedicationName = item.MedicationName,
                BoxPillCount = item.BoxSize,
                InitialBoxAmount = item.CurrentBoxCount,
                Id = Guid.NewGuid()
            }).ToList());

        TypeAdapterConfig<DosageDto, Dosage>
            .NewConfig()
            .Map(dest => dest.DosageTime, src => MapDosageTimeDto(src.DosageTime));
        
        TypeAdapterConfig<Dosage, DosageDto>
            .NewConfig()
            .Map(dest => dest.DosageTime, src => MapTimeSpan(src.DosageTime));

    }
    
    private static TimeSpan MapDosageTimeDto(DosageTimeDto dosageTime) => 
        TimeSpan.FromMinutes(dosageTime.Hour * 60 + dosageTime.Minute);

    private static DosageTimeDto MapTimeSpan(TimeSpan timeSpan) => 
        new (timeSpan.Hours, timeSpan.Minutes);
}