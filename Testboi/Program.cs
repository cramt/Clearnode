using Clearnode;
using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Testboi {
    class Program {
        static void Main(string[] args) {
            Engine engine = new Engine();
            dynamic dyn = engine.Evaluate(@"
module.exports = 'hello there'
");
            Console.WriteLine(dyn);
            Thread.Sleep(-1);
        }
    }
}
