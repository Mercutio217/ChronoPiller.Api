namespace ChronoPiller.Api.Core.Entities
{
    /// <summary>
    /// Represents the base entity in the application.
    /// All other entities should inherit from this class.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; set; }
    }
}