using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiBuildCompiler.Abs;
using System.IO;

namespace AiBuildCompiler.SemAn {
    class SemAn : Visitor {

        bool dump;
        int indent;

        public SemAn(bool dump) {
            this.dump = dump;
        }

        public void Visit(AbsArg acceptor) {
            Dump(string.Format("AbsArg {0}", acceptor.name));
        }

        public void Visit(AbsArgs acceptor) {
            Dump("AbsArgs");
            for (int i = 0; i < acceptor.list.Count; i++) {
                indent++; acceptor.list[i].Accept(this); indent--;
            }
        }

        public void Visit(AbsExprs acceptor) {
            Dump("AbsExprs");
            for (int i = 0; i < acceptor.exprs.Count; i++) {
                indent++; acceptor.exprs[i].Accept(this); indent--;
            }
        }

        public void Visit(AbsFunCall acceptor) {
            Dump(string.Format("AbsFunCall {0}", acceptor.name));
            indent++;
            acceptor.args.Accept(this);
            indent--;
        }

        internal void Dump(string production) {
            if (dump) {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < indent; i++) {
                    sb.Append(' ');
                }
                System.IO.File.AppendAllText(Program.outpath, sb.ToString() + production + Environment.NewLine);
            }
        }


        internal void DumpW(string production) {
            if (dump) {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < indent; i++) {
                    sb.Append(' ');
                }
                System.IO.File.AppendAllText(Program.outpath, sb.ToString() + production + Environment.NewLine);
            }
        }

        internal void Dump(AbsTree source) {
            if (!dump) return;
            indent = 0;
            source.Accept(this);
        }
    }
    
}
