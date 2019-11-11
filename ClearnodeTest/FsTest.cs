using Clearnode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ClearnodeTest {
    [TestClass]
    public class FsTest {
        Engine engine;
        public FsTest() {
            engine = new Engine();
        }
        [TestMethod]
        public void ReadFile() {

            Console.WriteLine(engine.V8Engine.Evaluate("require('fs')").GetType());
        } 
    }
}
