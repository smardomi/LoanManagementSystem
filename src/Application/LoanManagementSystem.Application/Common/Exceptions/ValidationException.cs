using FluentValidation.Results;

namespace LoanManagementSystem.Application.Common.Exceptions;
public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(IEnumerable<string> failures)
    : this()
    {
        Errors = ConvertFailuresToErrors(failures);
    }

    public IDictionary<string, string[]> Errors { get; }

    private IDictionary<string, string[]> ConvertFailuresToErrors(IEnumerable<string> failures)
    {
        var errorDictionary = new Dictionary<string, string[]>();

        errorDictionary[$"ValidationError"] = failures.ToArray();

        return errorDictionary;
    }

}
