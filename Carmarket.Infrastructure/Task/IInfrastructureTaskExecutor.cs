using Carmarket.Infrastructure.Resource;

namespace Carmarket.Infrastructure.Task
{
    public interface IInfrastructureTaskExecutor
    {
        /// <summary>
        /// Execution asyncronical infrastructure task
        /// </summary>
        /// <param name="resource">cloud/onpremise</param>
        /// <param name="task">db task, cache task, business task</param>
        /// <returns>infrastructure task result</returns>
        public Task<bool> ExecuteAsync(InfrastructureResource resource, InfrastructureTask task);
    }
}
