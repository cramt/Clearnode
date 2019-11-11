using System;

namespace Clearnode {
    [AttributeUsage(AttributeTargets.All)]
    public class HelperAttribute : Attribute {
        public string Name { get; set; }
        public HelperAttribute(string name) {
            Name = name;
        }
    }
}
