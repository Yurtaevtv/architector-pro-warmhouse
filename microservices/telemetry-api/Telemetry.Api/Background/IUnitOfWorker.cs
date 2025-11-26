namespace Telemetry.Api.Background
{
    public interface IUnitOfWork
    {
        string Schedule { get; }

        Task InvokeAsync();
    }
}
