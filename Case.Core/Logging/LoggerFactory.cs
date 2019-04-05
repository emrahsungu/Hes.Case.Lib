using System;
using System.Collections.Concurrent;

namespace CaseLib.Core.Logging {
    public static class LoggerFactory {

        private static readonly ConcurrentDictionary<Type, ILogger> Loggers;

        static LoggerFactory() {
            Loggers = new ConcurrentDictionary<Type, ILogger>();
        }

        public static ILogger GetLogger(Type type) {
            return Loggers.GetOrAdd(type, t => new ConsoleLogger());
        }

    }
}