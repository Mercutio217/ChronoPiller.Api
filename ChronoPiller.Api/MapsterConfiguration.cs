using ChronoPiller.Api.Core.Entities;
using ChronoPiller.Api.Models;
using Mapster;

namespace ChronoPiller.Api;

public static class MapsterConfiguration
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<PrescriptionCreateDto, Prescription>
            .NewConfig()
            .Map(dest => dest.Items, src => src.Items.Select(item => new PrescriptionItem()
            {
                Doses = item.Doses.Adapt<List<Dosage>>(),
                MedicationName = item.MedicationName,
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