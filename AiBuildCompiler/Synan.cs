
using AiBuildCompiler.Abs;
using System;
using System.Collections.Generic;
using System.IO;

/** TODO
 * Add snippets -- sout --> print
 * 
 * */

namespace AiBuildCompiler {
    class Synan {
        private LexAn lexAn;
        bool dump;
        public Synan(LexAn lexAn, bool dump) {
            this.lexAn = lexAn;
            this.dump = dump;
        }

        /** Productions
         * NOTE: use python style indent instead of new line
         * 
         * source -> exprs
         * exprs -> expr EOF exprs_
         * exprs_ -> expr EOF exprs_
         * exprs_ -> 
	     * expr -> function
	     * expr -> 
         * 
         * function -> id ( args )        
         * args -> arg args_
         * args_ -> , arg args_
         * args_ ->
         * arg -> id
         * */
        public AbsTree Parse() {
            Dump("source -> exprs");
            AbsTree t = ParseExprs();
            if (t != null && lexAn.parsed.Count == 0) {
                return t;
            } else {
                Report.Dump("Syntax error: "+lexAn.parsed[0]);
                return null;
            }
        }
         
        AbsTree ParseExprs() {
            Dump("exprs -> expr EOL exprs_");
            List<AbsExpr> exprs = new List<AbsExpr>();
            AbsExpr t = (AbsExpr)ParseExpr();
            exprs.Add(t);
            if (peekTakeNext(Token.EOL) != null) {
                ParseExprs_(exprs);
            }
            return new AbsExprs(exprs);
        }

        List<AbsExpr> ParseExprs_(List<AbsExpr> exprs) {
            Dump("exprs_ -> expr EOL exprs_");
            AbsExpr t = (AbsExpr)ParseExpr();
            if (t != null) {
                exprs.Add(t);
                if(peekTakeNext(Token.EOL) != null)
                    return ParseExprs_(exprs);
            } else {
                Dump("[override last]exprs_ -> ");
                return exprs;
            }
            return null;
        }

        AbsTree ParseExpr() {
            DumpW("expr-> ");
            if (peekNext(Token.identifier)) {
                Dump("function");
                return ParseFunction();
            } else {
                Dump("");
                return null;
            }
        }

        AbsTree ParseFunction() {
            Dump("function -> id ( args )");
            Symbol id = peekTakeNext(Token.identifier);
            if (id != null &&
                peekTakeNext(Token.lparent) != null) {
                AbsTree t = ParseArgs();
                if (peekTakeNext(Token.rparent) != null) {
                    return new AbsFunCall(id, (AbsArgs)t);
                }
            }
            return null;
        }

        AbsTree ParseArgs() {
            Dump("args -> arg args_");
            List<AbsArg> args = new List<AbsArg>();
            return new AbsArgs(MakeList(args, ParseArg, ParseArgs_));
        }

        List<AbsArg> ParseArgs_(List<AbsArg> args) {
            DumpW("args_ -> ");
            if (peekTakeNext(Token.comma) != null) {
                Dump("arg args_");
                return MakeList(args, ParseArg, ParseArgs_);
            } else {
                Dump("");
                return args;
            }
        }

        List<T> MakeList<T>(List<T> list, Func<AbsTree> fn1, Func<List<T>, List<T>> fns_) where T: AbsTree{
            T a = (T)fn1();
            if (a != null) {
                list.Add(a);
                return fns_(list);
            }
            return list;
        }

        AbsTree ParseArg() {
            Dump("arg -> id");
            Symbol s = peekTakeNext(Token.identifier);
            if (s != null)
                return new AbsArg(s);
            return null;
                //&& peekTakeNext(Token.colon)
                //&& ParseType();
        }
        
        Symbol peekTakeNext(Token token) {
            if (peekNext(token)) {
                return take();
            }
            return null;
        }

        bool peekNext(Token token) {
            return lexAn.parsed.Count > 0 && lexAn.parsed[0].token == token;
        }

        Symbol take() {
            Symbol s = lexAn.parsed[0];
            lexAn.parsed.RemoveAt(0);
            return s;
        }

        internal void Dump(string production) {
            if (dump)
                File.AppendAllText(Program.outpath, production+Environment.NewLine);
        }
        internal void DumpW(string production) {
            if (dump)
                File.AppendAllText(Program.outpath, production);
        }
    }
}
