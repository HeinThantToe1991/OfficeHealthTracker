using Microsoft.Extensions.Logging;

namespace OfficeHealthTracker.Infrastructure
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _logFilePath;

        public FileLoggerProvider(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_logFilePath);
        }

        public void Dispose() { }
    }
}
