/** TODO
 * Add snippets -- sout --> print
 * 
 * */

namespace AiBuildCompiler {
    public class Symbol {
        internal string txt;
        internal Token token;

        public Symbol(string txt, Token token) {
            if (token == Token.EOL) {
                txt = "";
            }
            this.txt = txt;
            this.token = token;
        }

        public override string ToString() {
            return string.Format("{0} {1}", token.ToString().ToUpper(), txt);
        }
    }
}
