using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CaseLib.Core.Logging {
    public class ConsoleLogger : ILogger {

        public void Debug(string message, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int line = 0) {
            var msg = $"[DEBUG]{Environment.NewLine} [{callerFilePath}]{Environment.NewLine}  [{memberName}]{Environment.NewLine}   [Line: {line}]:{Environment.NewLine}    {message}";
            DebugLog(msg);
        }

        [Conditional("DEBUG")]
        private void DebugLog(string msg) {
            Console.WriteLine(msg);
        }

    }
}