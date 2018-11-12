using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlApi {

    /// <summary>
    /// this will return the next word in a file
    /// </summary>
    public class findNextWord {
        private string _filepath; // this is the full path

        public findNextWord(string filepath) {
            if(string.IsNullOrWhiteSpace(filepath)) throw new ArgumentNullException("filepath");
            if(!File.Exists(filepath)) throw new FileNotFoundException();
            _filepath = filepath;
        }

        public string find(string findstring) {

            if(string.IsNullOrWhiteSpace(findstring)) throw new ArgumentNullException("findstring");

            string line = "";
            StreamReader file = new StreamReader(_filepath);
            while((line = file.ReadLine()) != null) {
                int index = 0;
                string[] splitline = line.Split(' ');
                foreach(var word in splitline) {
                    if(word.ToLowerInvariant() == findstring.ToLowerInvariant())
                        return splitline[index + 1];
                    index++;
                }
            }

            return "**Not_Found**"; 
        }
    }
}
