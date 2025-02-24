namespace LoanManagementSystem.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }
      

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}

public class Result<TData> : Result
        where TData : class
{
    internal Result(TData data,bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
        Data = data;
    }

    public TData Data { get; set; }

    public static Result Success(TData data)
    {
        return new Result<TData>(data,true, Array.Empty<string>());
    }

}

