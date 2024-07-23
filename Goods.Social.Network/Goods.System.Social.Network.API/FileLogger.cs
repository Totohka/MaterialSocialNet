namespace Goods.System.Social.Network.API
{
    public class FileLogger : ILogger, IDisposable
    {
        string filepath;
        static object _lock = new object();
        public FileLogger(string path)
        {
            filepath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }
        public void Dispose() { }
        public bool IsEnabled(LogLevel logLevel) 
        {  
            return true;  
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId,
                                TState state, Exception? exeption, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(filepath, formatter(state, exeption) + Environment.NewLine);
            }
        }
    }
}
