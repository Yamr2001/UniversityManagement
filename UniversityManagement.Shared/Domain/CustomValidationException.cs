namespace UniversityManagement.Shared.Domain
{
    public abstract class CustomValidationException : ApplicationException
    {
        protected CustomValidationException(string title, string message)
            : base(title, message)
        {
        }
        public CustomValidationException(string title, string message, IEnumerable<string> errors)
            : this(title, message)
        {
            Errors = errors;
        }
        public IEnumerable<string> Errors { get; } = Enumerable.Empty<string>();

        public sealed class ValidationExceptionValidator : ApplicationException
        {
            public ValidationExceptionValidator(IEnumerable<string> errorMessages)
                : base("Validation Failure", "One or more validation errors occurred")
            => ErrorMessages = errorMessages.ToList();

            public List<string> ErrorMessages
            {
                get;
            }
        }
    }
}
