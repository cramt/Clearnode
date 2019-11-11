using System;
using System.Collections.Generic;
using System.Linq;

namespace Clearnode.Helper {
    [EngineInterup("console", EngineInterupAttribute.EngineSetType.global)]
    public class Console {
        public static void log(params object[] args) {
            System.Console.WriteLine(string.Join(" ", args.Select(x => x.ToString()).ToArray()));
        }
        public static void info(params object[] args) {
            log(args);
        }
        public static void assert(bool check, string str) {
            if (!check) {
                log("Assertion failed: " + str);
            }
        }
        private static Dictionary<string, uint> countDictionary = new Dictionary<string, uint>();
        public static void count(string str = null) {
            string toLog = str + ": ";
            if (countDictionary.ContainsKey(str)) {
                countDictionary[str]++;
                toLog += countDictionary[str];
            }
            else {
                countDictionary.Add(str, 1);
                toLog += 1;
            }
            log(toLog);
        }
        public static void countReset(string str = null) {
            if(str == null) {
                return;
            }
            countDictionary.Remove(str);
        }
        public static void debug(params object[] args) {
            log(args);
        }
        public static void dir(object obj, dynamic options) {
            throw new NotImplementedException("console.dir not implemented yet");
        }

    }
}
