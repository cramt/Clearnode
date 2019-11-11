using System;
using System.IO;
using System.Text;
using Clearnode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearnodeTest {
    [TestClass]
    public class ConsoleTest {
        private class ConsoleCapture : TextWriter {
            public string Captured { get; private set; } = "";
            public override Encoding Encoding => Encoding.UTF8;
            public override void Write(char value) {
                Captured += value;
            }
            public override void Write(string value) {
                Captured += value;
            }
        }
        Engine engine;
        ConsoleCapture Capture;
        public ConsoleTest() {
            Capture = new ConsoleCapture();
            Console.SetOut(Capture);
            engine = new Engine();
        }
        [TestMethod]
        public void Log() {
            string str = "hello theredd";
            engine.V8Engine.Evaluate("console.log('d" + str + "')");
            Assert.AreEqual(Capture.Captured, str);
        }
    }
}
