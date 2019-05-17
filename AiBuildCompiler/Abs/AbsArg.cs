using System;

namespace AiBuildCompiler.Abs {
    public class AbsArg : AbsTree {
        public string name;
        public Symbol sym;

        public AbsArg(Symbol name) {
            this.name = name.txt;
            this.sym = name;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}