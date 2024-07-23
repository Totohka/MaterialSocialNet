namespace Goods.System.Social.Network.API
{
    public class FileLoggerProvider : ILoggerProvider
    {
        string path;
        public FileLoggerProvider(string path)
        {
            this.path = path;
        }
        public ILogger CreateLogger(string categotyName)
        {
            return new FileLogger(path);
        }
        public void Dispose() { }
    }
}
