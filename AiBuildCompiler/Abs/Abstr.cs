


using System;
using System.Text;
/** TODO
* Add snippets -- sout --> print
* 
* */
namespace AiBuildCompiler.Abs {
    class Abstr:Visitor {
        int indent = 0;
        private bool dump;

        public Abstr(bool v) {
            this.dump = v;
        }

        public void Visit(AbsArgs args) {
            indent++; Dump("AbsArgs ");
            for (int i = 0; i < args.list.Count; i++) {
                indent++; args.list[i].Accept(this); indent--;
            }
            indent--;
        }

        public void Visit(AbsExprs exprs) {
            Dump("AbsExprs");
            for (int i = 0; i < exprs.exprs.Count; i++) {
                indent++; exprs.exprs[i].Accept(this); indent--;
            }
        }

        public void Visit(AbsFunCall funCall) {
            Dump("AbsFunCall " + funCall.name);
            indent++; funCall.args.Accept(this); indent--;
        }

        public void Visit(AbsArg arg) {
            Dump("AbsArg " + arg.name);
        }

        internal void Dump(AbsTree source) {
            if (!dump) return;

            if (source == null) return;

            source.Accept(this);
        }

        void Dump(string production) {
            if (dump) {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < indent; i++) {
                    sb.Append(' ');
                }
                System.IO.File.AppendAllText(Program.outpath, sb.ToString()+production + Environment.NewLine);
            }
        }
    }

    
}
