using System.Collections.ObjectModel;

namespace GameHostsManager.Application.Exceptions
{
    public class BadOperationException : Exception
    {
        public sealed class ValidationError
        {
            public string PropertyName { get; set; } = null!;
            public string ErrorMessage { get; set; } = null!;
        }

        public readonly IReadOnlyCollection<ValidationError> ValidationErrors;

        public BadOperationException(string message, IEnumerable<ValidationError>? validationErrors = null)
            : base(message)
        {
            ValidationErrors = validationErrors != null
                ? new ReadOnlyCollection<ValidationError>(validationErrors.ToList())
                : new ReadOnlyCollection<ValidationError>(new List<ValidationError>());
        }

        public static BadOperationException ValidationProblems(IEnumerable<ValidationError> validationErrors)
        {
            return new BadOperationException("Errors occurred during validation", validationErrors);
        }

        public static BadOperationException RoomAlreadyExists()
        {
            return new BadOperationException("The room with the specified parameters already exists");
        }

        public static BadOperationException RoomNotFound()
        {
            return new BadOperationException("Room not found");
        }
    }
}
