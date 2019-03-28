using System;
using System.Runtime.CompilerServices;

namespace Case.Core
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string message, [CallerFilePath]string callerFilePath = null,[CallerMemberName] string memberName =null, [CallerLineNumber]int line = 0)
        {
            Console.WriteLine($"[DEBUG]{Environment.NewLine} [{callerFilePath}]{Environment.NewLine}  [{memberName}]{Environment.NewLine}   [Line: {line}]:{Environment.NewLine}    {message}");
        }
    }
}