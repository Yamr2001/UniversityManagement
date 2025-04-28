namespace UniversityManagement.Shared.Domain
{
    public abstract class ValidationException : ApplicationException
    {
        protected ValidationException(string title, string message)
            : base(title, message)
        {
        }
        public ValidationException(string title, string message, IEnumerable<string> errors)
            : this(title, message)
        {
            Errors = errors;
        }
        public IEnumerable<string> Errors { get; } = Enumerable.Empty<string>();
    }
}
