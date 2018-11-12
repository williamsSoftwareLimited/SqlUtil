using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlApi
{
    // a weird name but this class should remove all the superfulous stuff from the start of a stored proc or function
    public class batchIt
    {
        private dir _dir;
        private runSql _runSql;
        private List<char[]> WORDS_TO_FIND_AS_CHAR_ARRAYs;
        private readonly string[] WORDS_TO_FIND = { "alter$", "create$" };

        public batchIt(dir dir, runSql runSql)
        {
            _dir = dir;
            _runSql = runSql;
            WORDS_TO_FIND_AS_CHAR_ARRAYs = new List<char[]>();
            foreach(var word in WORDS_TO_FIND) WORDS_TO_FIND_AS_CHAR_ARRAYs.Add(word.ToCharArray());
        }

        public List<string> process()
        {
            var l = new List<string>(); // a list of return messages

            // get the filenames and enumerate through them
            _dir.listSql().ForEach(p =>
            {
                // open each file
                var fi = new FileInfo(_dir.Dirpath + p);
                var fs = fi.OpenRead();
                int currentWordIndex = 0, positionInWord = 0;
                var canRead = fs.CanRead; // this is a two-fold var. for the can read of the file and also to exit the outer while when word found

                while (canRead && fs.Position<fs.Length)
                {
                    // retrieve the 32 bits of the char or'd with 32 to drop to lowercase
                    var c = (char)(fs.ReadByte() | 32 + fs.ReadByte()*256);

                    // if found in one word continue until either the end of that word or a space is found
                    while (true)
                    {
                        if (WORDS_TO_FIND_AS_CHAR_ARRAYs[currentWordIndex][positionInWord] == c)
                        {
                            positionInWord++; // move up the word

                            // at the end of the word to find
                            if (WORDS_TO_FIND_AS_CHAR_ARRAYs[currentWordIndex][positionInWord] == '$')
                            {
                                // we've found the word get the start                         
                                canRead = false;
                            }
                            break;
                        }
                        else
                        {
                            // try another word and start from beginning
                            currentWordIndex++;
                            positionInWord = 0;

                            // tried all the letters and 
                            if (currentWordIndex >= WORDS_TO_FIND_AS_CHAR_ARRAYs.Count())
                            {
                                currentWordIndex = 0;
                                break;
                            }
                        }
                    }

                }

                if (!canRead)
                {
                    // we've found the word get, if its the beginning then ignore if not delete everything before it 
                    Console.Write("We've found a word and it's {0} in {1},", WORDS_TO_FIND[currentWordIndex], p);
                    Console.WriteLine("The position is {0}.", fs.Position - (WORDS_TO_FIND[currentWordIndex].Length - 1) * 2);
                }

            });

            return l;
        }

        public string remove(FileInfo fi)
        {
            var text2Delete = string.Empty;
            var fs = fi.OpenRead();
            int currentWordIndex = 0, positionInWord = 0;
            var canRead = fs.CanRead; // this is a two-fold var. for the can read of the file and also to exit the outer while when word found

            while (canRead && fs.Position < fs.Length)
            {
                // retrieve the 32 bits of the char or'd with 32 to drop to lowercase
                var c = (char)(fs.ReadByte() | 32 + fs.ReadByte() * 256);

                // if found in one word continue until either the end of that word or a space is found
                while (true)
                {
                    if (WORDS_TO_FIND_AS_CHAR_ARRAYs[currentWordIndex][positionInWord] == c)
                    {
                        positionInWord++; // move up the word

                        // to the end of the word to find
                        if (WORDS_TO_FIND_AS_CHAR_ARRAYs[currentWordIndex][positionInWord] == '$') canRead = false;
                    
                        break;
                    }
                    else
                    {
                        // try another word and start from beginning
                        currentWordIndex++;
                        positionInWord = 0;

                        // tried all the letters and 
                        if (currentWordIndex >= WORDS_TO_FIND_AS_CHAR_ARRAYs.Count())
                        {
                            currentWordIndex = 0;
                            break;
                        }
                    }
                }

            }

            if (!canRead)
            {
                // we've found the word get, if its the beginning then ignore if not delete everything before it 
                Console.Write("We've found a word and it's {0} in {1},", WORDS_TO_FIND[currentWordIndex], fi.FullName);
                Console.WriteLine("The position is {0}.", fs.Position - (WORDS_TO_FIND[currentWordIndex].Length - 1) * 2);
            }
            return text2Delete;
        }
    }
}
