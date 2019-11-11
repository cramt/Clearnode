using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clearnode {
    public class Engine {
        private class HelperClass {
            public Type Type { get; set; }
            public HelperAttribute Attribute { get; set; }
        }
        private static List<HelperClass> helperClasses = GetHelperClasses();
        private static List<HelperClass> GetHelperClasses() {
            return (from a in AppDomain.CurrentDomain.GetAssemblies()
                 from t in a.GetTypes()
                 let attributes = t.GetCustomAttributes(typeof(HelperAttribute), true)
                 where attributes != null && attributes.Length > 0
                 select new HelperClass { Type = t, Attribute = attributes.Cast<HelperAttribute>().ElementAt(0) }).ToList();
        }
        public V8ScriptEngine V8Engine { get; private set; }
        public Engine(V8ScriptEngine V8Engine = null) {
            if (V8Engine == null) {
                V8Engine = new V8ScriptEngine();
            }
            this.V8Engine = V8Engine;
            helperClasses.ForEach(x => {
                V8Engine.AddHostType(x.Attribute.Name, x.Type);
            });
        }
    }
}
