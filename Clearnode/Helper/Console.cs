using System.Linq;

namespace Clearnode.Helper {
    [Helper("console")]
    public class Console {
        public static void log(params object[] args) {
            System.Console.WriteLine(string.Join(" ", args.Select(x => x.ToString()).ToArray()));
        }
    }
}
