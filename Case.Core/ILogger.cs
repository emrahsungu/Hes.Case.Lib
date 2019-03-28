using System.Runtime.CompilerServices;

namespace Case.Core
{
    public interface ILogger
    {
        void Debug(string message, [CallerFilePath] string callerFilePath = null,[CallerMemberName] string memberName = null, [CallerLineNumber] int line = 0);

    }
}