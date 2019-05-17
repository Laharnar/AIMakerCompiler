/** TODO
 * Add snippets -- sout --> print
 * 
 * */

namespace AiBuildCompiler {
    public enum Token {
        identifier,
        comment,
        whitespace,

        undefined, // any tipe - int, bool, tr, flots with up to 5 decimals
        colon, // :
        comma,
        EOL, // end of line \n or \n\r i guess
        rparent,
        lparent,

        // operators
        lt, // < > =, also detects doubles
        gt,
        assign,
        eq,
        leq,
        geq,
        tab,
    }
}
