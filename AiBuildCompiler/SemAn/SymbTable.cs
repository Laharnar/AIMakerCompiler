using System;
using AiBuildCompiler.Abs;
using System.Collections.Generic;

namespace AiBuildCompiler.SemAn {
    class SymbTable {
        static Dictionary<string, AbsTree> table = new Dictionary<string, AbsTree>();

        internal static bool ins(string name, AbsTree acceptor) {
            if (table.ContainsKey(name)) {// prevent duplicated definitions
                Report.Error("Duplicate name --> "+name);
                return false;
            }
            table.Add(name, acceptor);
            return true;
        }
    }
}