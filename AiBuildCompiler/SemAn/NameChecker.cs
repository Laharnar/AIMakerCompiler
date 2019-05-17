using AiBuildCompiler.Abs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiBuildCompiler.SemAn {
    class NameChecker : Visitor {
        public void Visit(AbsArg acceptor) {
            SymbTable.ins(acceptor.name, acceptor);
        }

        public void Visit(AbsArgs args) {
            for (int i = 0; i < args.list.Count; i++) {
                args.list[i].Accept(this);
            }
        }

        public void Visit(AbsExprs exprs) {
            for (int i = 0; i < exprs.exprs.Count; i++) {
                exprs.exprs[i].Accept(this);
            }
        }

        public void Visit(AbsFunCall funCall) {
            funCall.args    .Accept(this);
        }
    }
}
