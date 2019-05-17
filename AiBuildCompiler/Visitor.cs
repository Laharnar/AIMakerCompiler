using AiBuildCompiler.Abs;

namespace AiBuildCompiler {
    public interface Visitor {
        void Visit(AbsArg acceptor);
        void Visit(AbsArgs acceptor);
        void Visit(AbsExprs acceptor);
        void Visit(AbsFunCall acceptor);
    }
}
