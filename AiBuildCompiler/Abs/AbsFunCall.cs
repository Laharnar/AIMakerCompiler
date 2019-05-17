namespace AiBuildCompiler.Abs {
    public class AbsFunCall : AbsExpr {
        public Symbol name;
        public AbsArgs args;

        public AbsFunCall(Symbol name, AbsArgs args) {
            this.name = name;
            this.args = args;
        }

        public override void Accept(Visitor visitor) {
            visitor.Visit(this);
        }
    }
}