using Carmarket.Infrastructure.InfrastructureExceptions;
using Carmarket.Infrastructure.Log;

namespace Carmarket.Infrastructure.Logger
{
    public class InfrastructureLog : IInfrastructureLog
    {
        public void Read(InfrastructureLogReadStream stream)
        {
            throw new NotImplementedException();
        }

        public void Write(InfrastructureLogWriteStream stream)
        {
            throw new NotImplementedException();
        }

        public void WriteException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void WriteInfrastructureException(InfrastructureException exception)
        {
            throw new NotImplementedException();
        }
    }
}
