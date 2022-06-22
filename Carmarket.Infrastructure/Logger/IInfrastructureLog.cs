using Carmarket.Infrastructure.InfrastructureExceptions;

namespace Carmarket.Infrastructure.Log
{
    public interface IInfrastructureLog
    {
        /// <summary>
        /// Write to inftrastructure log
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        void Write(InfrastructureLogWriteStream stream);

        /// <summary>
        /// Write to inftrastructure log
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        void WriteInfrastructureException(InfrastructureException exception);

        /// <summary>
        /// Write to inftrastructure log
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        void WriteException(Exception exception);

        /// <summary>
        /// Read from inftrastructure log
        /// </summary>
        /// <param name="stream"></param>
        void Read(InfrastructureLogReadStream stream);
    }
}
