using Carmarket.Infrastructure.InfrastructureExceptions;
using Carmarket.Infrastructure.Log;
using Carmarket.Infrastructure.Resource;

namespace Carmarket.Infrastructure.Task
{
    public class InfrastructureTaskExecutor : IInfrastructureTaskExecutor
    {
        private readonly IInfrastructureLog log;

        public InfrastructureTaskExecutor(IInfrastructureLog log)
        {
            this.log = log;
        }

        public async Task<bool> ExecuteAsync(InfrastructureResource resource, InfrastructureTask task)
        {
            using (resource)
            {
                try
                {
                    // execute infrastructure task
                    await task;
                }
                catch (InfrastructureException e)
                {
                    // log infrastructure exception
                    log.WriteInfrastructureException(e);
                    return false;
                }
                catch (Exception e)
                {
                    // log exception
                    log.WriteException(e);
                    return false;
                }
            }
        
            return true;
        }
    }
}