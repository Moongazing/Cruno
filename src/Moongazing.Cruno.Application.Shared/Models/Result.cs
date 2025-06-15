namespace Moongazing.Cruno.Application.Shared.Models;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }

    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string? error)
    {
        if (isSuccess && error != null) throw new InvalidOperationException();
        if (!isSuccess && error is null) throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base(true, null) => Value = value;

    private Result(string error) : base(false, error) => Value = default;

    public static Result<T> Success(T value) => new(value);
    public static new Result<T> Failure(string error) => new(error);
}
