namespace TwitterCloneBackend.Shared.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string id)
            : base(String.Format("No entity found with id {0}", id))
        {
        }
    }
}
