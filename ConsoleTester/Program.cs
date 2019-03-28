using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Case.Core;

namespace ConsoleTester
{
    class Program
    {
        private static readonly ICaseStorage caseStorage = new FileSystemCaseStorage("dbDirectory");
        static void Main(string[] args)
        {
            var @case = new Case.Core.Case("123456","スタッフ");
            var result = caseStorage.AddCase(@case);
        }
    }
}
