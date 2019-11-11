using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clearnode {
    public class Engine {
        #region  all the reflection stuff
        private class HelperClass {
            public Type Type { get; set; }
            public EngineInterupAttribute Attribute { get; set; }
        }
        private static List<HelperClass> interopClasses = GetInteropClasses();
        private static List<HelperClass> globalSetInteropClasses = interopClasses.Where(x => x.Attribute.SetType == EngineInterupAttribute.EngineSetType.global).ToList();
        private static List<HelperClass> requireInteropClasses = interopClasses.Where(x => x.Attribute.SetType == EngineInterupAttribute.EngineSetType.require).ToList();
        private static List<HelperClass> GetInteropClasses() {
            return (from a in AppDomain.CurrentDomain.GetAssemblies()
                    from t in a.GetTypes()
                    let attributes = t.GetCustomAttributes(typeof(EngineInterupAttribute), true)
                    where attributes != null && attributes.Length > 0
                    select new HelperClass { Type = t, Attribute = attributes.Cast<EngineInterupAttribute>().ElementAt(0) }).ToList();
        }
        #endregion

        private string filename = "";
        private string dirname = "";
        public V8ScriptEngine V8Engine { get; private set; }
        public Engine(V8ScriptEngine V8Engine = null) {
            if (V8Engine == null) {
                V8Engine = new V8ScriptEngine();
            }
            this.V8Engine = V8Engine;
            globalSetInteropClasses.ForEach(x => {
                V8Engine.AddHostType(x.Attribute.Name, x.Type);
            });
            V8Engine.AddHostObject("require", new Func<string, object>((string requirePath) => {
                HelperClass helper = requireInteropClasses.FirstOrDefault(x => x.Attribute.Name == requirePath);
                if (helper != null) {
                    return helper.Type.GetConstructor(new Type[] { }).Invoke(new object[] { });
                }
                if (requirePath.Length > 2 && requirePath.Substring(0, 2) == "./") {
                    //TODO: check if file doesnt exist
                    return Evaluate(File.ReadAllText(Path.Combine(dirname, requirePath)));
                }
                return null;
                //TODO: require package.json stuff
            }));
            V8Engine.Evaluate("const __dirname = '" + dirname + "';");
            V8Engine.Evaluate("const __filename = '" + filename + "';");
        }
        public void LoadFile(string path) {
            filename = path;
            dirname = Directory.GetParent(path).FullName;
            if (!File.Exists(path)) {
                throw new ArgumentException("no file on path given");
            }
            Evaluate(File.ReadAllText(path));

        }
        public object Evaluate(string code) {
            string pre = @"(()=>{
const module = {}
module.exports = {}
";
            string post = @"
return module
})()";
            return V8Engine.Evaluate(pre + code + post);
        }
    }
}
