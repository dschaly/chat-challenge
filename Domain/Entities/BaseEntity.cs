namespace Domain.Entities
{
    public abstract class BaseEntity<TPrimarykey>
    {
        public TPrimarykey Id { get; set; }
    }
}
