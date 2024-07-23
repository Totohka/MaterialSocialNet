using System.Runtime.CompilerServices;

namespace Goods.System.Social.Network.API
{
    public static class FileLoggerExtentions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filepath)
        {
            builder.AddProvider(new FileLoggerProvider(filepath));
            return builder;
        }
    }
}
