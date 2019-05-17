
using System;

/** TODO
 * Add snippets -- sout --> print
 * 
 * */

namespace AiBuildCompiler {
    class Report {
        public static void Warning(string msg) {
            Console.WriteLine(string.Format("Warning: {0}", msg));
        }

        public static void Dump(string msg) {
            Console.WriteLine(string.Format(msg));
        }

        public static void Error(string msg) {
            Console.WriteLine(string.Format("Error: {0}", msg));
        }
    }
}
