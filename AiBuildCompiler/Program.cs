
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AiBuildCompiler.Abs;
using AiBuildCompiler.SemAn;

/** TODO
 * Add snippets -- sout --> print
 * 
 * */

namespace AiBuildCompiler {
    class Program {
        public static string path = "";
        public static string outpath = "";

        static void Main(string[] args) {
            Run(args);
            Console.WriteLine("\nDone <o-_I_-o>.\n");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void Run(string[] args) {
            Console.WriteLine("This is AIM compiler.");

            string phase = "";
            string dump = "";
            // parse args
            for (int i = 0; i < args.Length; i++) {
                if (args[i].StartsWith("--phase=")) {
                    phase = (string)args[i].Substring(8);
                }
                else // dump args should be separated by or smth else
                if (args[i].StartsWith("--dump=")) {
                    dump = (string)args[i].Substring(7);
                } else if (args[i].StartsWith("--path=")) {
                   path = (string) args[i].Substring(7);
                   outpath = path.Replace(".aim", ".log");
                }
            }
             

            string text = File.ReadAllText(path); ;

            Dump("** Begin LEXICAL ANALYZER");
            LexAn lexAn = new LexAn();
            lexAn.Parse(text);

            if (dump.Contains("lexan")) {
                lexAn.Dump(lexAn);
            }

            if (phase == "lexan") return;
            Dump("** Begin SYNTAX ANALYZER");

            // check if order of commands is correct--> productions
            Synan synan = new Synan(lexAn, dump.Contains("synan"));
            AbsTree source = synan.Parse();
            if (phase == "synan") return;
            Dump("** Begin AST");
            Abstr ast = new Abstr(dump.Contains("ast"));
            ast.Dump(source);
            if (phase.Equals("ast")) return;
            Dump("** Begin SEMAN");

            SemAn.SemAn semAn = new SemAn.SemAn(dump.Contains("seman"));
            source.Accept(new NameChecker());
            semAn.Dump(source);
            if (phase.Equals("seman")) return;

            Interpreter.Interpreter interpreter = new Interpreter.Interpreter();

            if (phase.Equals("interpreter")) return;

            Report.Error("Unknown phase "+phase+".");
        }
        static void Dump(string production) {
            
            System.IO.File.AppendAllText(Program.outpath,  production + Environment.NewLine);
        }
    }
}
