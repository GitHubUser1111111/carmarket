namespace Carmarket.Infrastructure.InfrastructureExceptions
{
    internal class InfrastructureExceptionAnalyzeResult
    {
        protected bool IsWarning { get; }

        protected bool IsError { get; }

        protected InfrastructureExceptionAnalyzeResult(bool isWarning, bool isError)
        {
            IsWarning = isWarning;
            IsError = isError;
        }
    }
}
