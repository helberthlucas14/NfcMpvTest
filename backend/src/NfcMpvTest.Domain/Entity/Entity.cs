namespace NfcMpvTest.Domain.Entity
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        protected Entity() => Id = Guid.NewGuid();
    }
}
