namespace ChronoPiller.Shared.Abstractions
{
    /// <summary>
    /// Represents the base entity in the application.
    /// All other entities should inherit from this class.
    /// </summary>
    public abstract class ChronoBaseEntity<TKey> where TKey : struct
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public TKey Id { get; set; }
    }
}