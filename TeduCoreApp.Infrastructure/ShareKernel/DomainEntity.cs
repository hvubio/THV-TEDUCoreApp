namespace TeduCoreApp.Infrastructure.ShareKernel
{
    public abstract class DomainEntity<T>
    {
        public T Id { get; set; }

        /// <summary>
        ///     True if domain entity has an identity
        /// </summary>
        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }
    }
}