using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlApi
{
    public class editor
    {
        private List<string> _files;
        private string _directory;

        public editor(List<string> files, string directory)
        {
            _files = files;
            _directory = directory;
        }

        public List<string> change(string from, string to)
        {
            return addGeneric(from, to);
        }

        public List<string> add(string after, string addition)
        {
            return addGeneric(after, after + "\r\n" + addition);
        }

        List<string> addGeneric(string txt2find, string txt2cx2)
        {
            List<string> filescxd = new List<string>();

            _files.ForEach(p =>
            {
                var sql = System.IO.File.ReadAllText(_directory + "\\" + p);

                if (sql.Contains(txt2find))
                {
                    sql = sql.Replace(txt2find, txt2cx2);
                    System.IO.File.WriteAllText(_directory + "\\" + p, sql); // pretty basic
                    filescxd.Add(p + " -> changed");
                }
                else
                {
                    filescxd.Add(p + " -> unchanged");
                }

            });
            return filescxd;
        }

    }
}
