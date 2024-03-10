using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace OfficeHealthTracker.Infrastructure
{
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        private readonly object _lock = new object();

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(_logFilePath, $"{DateTime.Now} [{logLevel}] {formatter(state, exception)}{Environment.NewLine}");
            }
        }
    }
}
