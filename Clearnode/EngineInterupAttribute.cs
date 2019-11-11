using System;

namespace Clearnode {
    [AttributeUsage(AttributeTargets.All)]
    public class EngineInterupAttribute : Attribute {
        public enum EngineSetType {
            global,
            require
        }
        public string Name { get; set; }
        public EngineSetType SetType { get; set; }
        public EngineInterupAttribute(string name, EngineSetType setType = EngineSetType.global) {
            Name = name;
            SetType = setType;
        }
    }
}
