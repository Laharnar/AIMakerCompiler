
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/** TODO
 * Add snippets -- sout --> print
 * 
 * */

namespace AiBuildCompiler {
    partial class LexAn {
        /* Verify if character are valid.
        * Create id's out of it and dump them
        * */
        bool isComment = false;

        Token lastToken;
        StringBuilder word;

        public List<Symbol> parsed = new List<Symbol>();
        private bool waitOne; // for double operator words
        int state = 0;
        int whitespaceCount = 0;
        internal void Dump(LexAn lexAn) {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < parsed.Count; i++) {
                s.Append(parsed[i].ToString());
                s.Append(Environment.NewLine);
            }
            File.WriteAllText(Program.outpath, s.ToString());
        }

        internal void Parse(string text) {
            parsed.Clear();
            int len = text.Length;
            word = new StringBuilder();
            for (int i = 0; i < len; i++) {
                // recognize id's
                /* For now just support characters separated by whitespaces
                 * */
                char cur = text[i];
                // if next is separator, add id
                bool isWhitespace = false;
                
                // if the caracter is beginning of new word, save the old token
                Token t = peekNext(cur, text, ref i);
                if (t != lastToken) {
                    string saveSymbol = word.ToString();
                    isWhitespace = true;
                    if (!string.IsNullOrEmpty(saveSymbol)) {
                        isWhitespace = false;
                        if (state == 1) {
                            state = 0;
                        }else 
                        if (parsed.Count > 0 && parsed[parsed.Count - 1].token == Token.EOL &&
                            lastToken == Token.EOL) { // no need for multiple eol's

                        } else if (lastToken != Token.comment // don't compile comments
                             ) {
                            if ((lastToken == Token.whitespace|| lastToken == Token.tab)) {
                                // skip whitespaces that arent at start
                                if(parsed[parsed.Count - 1].token == Token.EOL)
                                    parsed.Add(new Symbol(whitespaceCount.ToString(), lastToken));
                            } else
                            if (waitOne) {
                                parsed.Add( new Symbol(saveSymbol+cur, lastToken));
                                state = 1;
                            } else parsed.Add(new Symbol(saveSymbol, lastToken));
                        }
                        word.Clear();
                    }
                    lastToken = t;
                }
                // use whitespaces to make offset
                if (!isWhitespace) {
                    word.Append(cur);
                }
                if (!(lastToken == Token.whitespace || lastToken == Token.tab)) {
                    whitespaceCount = 0;
                }
            }

            // at the end, add what's left
            if (!string.IsNullOrEmpty(word.ToString())) {
                parsed.Add(new Symbol(word.ToString(), lastToken));
            }
            // make sure separator is at the end too
            if (lastToken != Token.EOL) {
                parsed.Add(new Symbol(Environment.NewLine, Token.EOL));
            }
        }

        /// <summary>
        /// True --> separator
        /// False --> normal character
        /// 
        /// ' ' \t should be ignored
        /// # is multiline comment #
        /// \n new line
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        Token peekNext(char c, string text, ref int i) {
            waitOne = false;
            switch (c) {
                case ' ': 
                    whitespaceCount++;
                    return Token.whitespace;
                    
                case '\t':
                    whitespaceCount+=4;
                    return Token.tab;
                case '#': // token.comment + not comment --> end of comment, token.comment + is comment --> start of comment
                    isComment = !isComment;
                    return Token.comment;
                    
                case ':':
                    return Token.colon;
                case ',':
                    return Token.comma;
                case '(':
                    return Token.lparent;
                case ')':
                    return Token.rparent;
                case '>':
                    return Token.gt;
                case '<':
                    return Token.lt;
                case '=':
                    waitOne = true;
                    if (lastToken == Token.gt)
                        return Token.geq;
                    if (lastToken == Token.lt)
                        return Token.leq;
                    if (lastToken == Token.assign)
                        return Token.eq;
                    else {
                        waitOne = false;
                        return Token.assign;
                    }
                default:
                    if (isComment)
                        return Token.comment;
                    if (c.Equals('\r')) {
                        if (text[i + 1].Equals('\n')) {
                            i += 1;
                            return Token.EOL;
                        }
                    }
                    return Token.identifier;
            }
        }
    }
}
