using System;
using System.Collections.Generic;

namespace AiBuildCompiler.Abs {
    public class AbsArgs : AbsTree {
        public List<AbsArg> list;

        public AbsArgs(List<AbsArg> list) {
            this.list = list;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}