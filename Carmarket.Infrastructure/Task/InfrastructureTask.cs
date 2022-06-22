namespace Carmarket.Infrastructure.Task
{
    public class InfrastructureTask : Task<bool>
    {
        public InfrastructureTask(Func<object?, bool> function, object? state) : base(function, state)
        {
        }

        public InfrastructureTask(Func<object?, bool> function, object? state, CancellationToken cancellationToken) : base(function, state, cancellationToken)
        {
        }
    }
}
