using TS.Result;

namespace AppointmentSystemServer.Application.Commons;

public static class ResultValidate
{
    public static Result<T> Run<T>(params Result<T>[] logics)
    {
        var resultError = ValidateAsync(logics);
        return resultError.Item1 ? Result<T>.Failure(resultError.Item2) : Result<T>.Succeed(default(T));
    }

    private static (bool, string) ValidateAsync(params Result<object>[] logics)
    {
        List<Result<object>> results = logics.Where(x => !x.IsSuccessful).ToList(); // False olanları getirecek
        var hasError = results.Any(); // results var mı ?
        var messages = string.Join("\n", results.Select(x => x.Data));
        return (hasError, messages);

    }
}