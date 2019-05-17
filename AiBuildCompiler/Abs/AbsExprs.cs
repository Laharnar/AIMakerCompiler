using System.Collections.Generic;

namespace AiBuildCompiler.Abs {
    public class AbsExprs : AbsTree {
        public List<AbsExpr> exprs;

        public AbsExprs(List<AbsExpr> exprs) {
            this.exprs = exprs;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}