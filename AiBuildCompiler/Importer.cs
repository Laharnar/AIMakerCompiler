using System.IO;

/** TODO
 * Add snippets -- sout --> print
 * 
 * */

namespace AiBuildCompiler {
    class Importer {
        /* Reads file and return content in binary or string.
         * */
        string path;
        string sourceTxt;
        byte[] sourceByt;
        public Importer(string path) {
            this.path = path;
        }

        /// <summary>
        /// Converts source to string builder
        /// </summary>
        /// <returns></returns>
        public string ReadText() {
            return sourceTxt = File.ReadAllText(path);
        }

        public byte[] ReadBytes() {
            return sourceByt = File.ReadAllBytes(path);
        }
    }
}
