using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace sqlApi
{
    public class dir : entityCore
    {
        public string Dirpath { get; private set; }
        public bool Recursive { get { return "True" == _recursive; } }

        private string _recursive; // note this can never be changed directly as the tostring returns with the first letter a captital
        private readonly string FILES_NODE="files";

        public dir(string filename) : base(filename)
        {
            var root = readXmldoc().FirstChild; // data
            Dirpath = root["dir"]["path"].InnerText;
            _recursive = root["dir"]["recursive"].InnerText;
        }

        public dir(string filename, string dirpath, bool recursive) : this(filename)
        {
            Dirpath = dirpath;
            _recursive = recursive.ToString();
        }
        public dir(string filename, string dirpath) : this(filename)
        {
            Dirpath = dirpath;
        }
        public dir(string filename, bool recursive) : this(filename)
        {
            _recursive = recursive.ToString();
        }

        public override void save()
        {
            var xdoc = readXmldoc();
            var root = xdoc.FirstChild; // data
            root["dir"]["path"].InnerText = Dirpath;
            root["dir"]["recursive"].InnerText = _recursive;
            xdoc.Save(_filename);
        }

        public List<string> loadSql(string[] args)
        {
            var files = new DirectoryInfo(Dirpath)
                            .EnumerateFiles("*.sql", Recursive?SearchOption.AllDirectories:SearchOption.TopDirectoryOnly)
                            .Select(p => p.FullName);
            var xdoc = readXmldoc();
            var root = xdoc.FirstChild; // data
            var returnFiles = new List<string>();

            // switch --add
            if(args.Count() == 1 || args[1] != "add")
                root["files"].InnerText = string.Empty; // this clears all the loaded sql in the XML file

            // get the list of filenames
            var filenames = getfilenames(FILES_NODE);

            files.ToList().ForEach(p =>
            {
                var filename = p.Replace(Dirpath, String.Empty);
                if(!exists(filename, filenames)) {
                    root["files"].PrependChild(xdoc.CreateElement("file")).InnerText = filename;
                    returnFiles.Add(filename);
                }
                filenames.Reset();
            });
            xdoc.Save(_filename);

            if(returnFiles.Count() == 0) returnFiles.Add("No files were added.");
            return returnFiles;
        }

        public List<string> addSql(string[] args)
        {
            HashSet<string> retList = new HashSet<string>();
            if (args.Length < 2) return retList.ToList();
            var xdoc = readXmldoc();
            var root = xdoc.FirstChild; // data

            listSql().ForEach(p => retList.Add(p));

            args.Skip(1).Where(q => !retList.Contains(q)).ToList().ForEach(p => root["files"].PrependChild(xdoc.CreateElement("file")).InnerText = p);

            xdoc.Save(_filename);

            return listSql();
        }

        public List<string> leaveSql(string[] args)
        {
            var xdoc = readXmldoc();
            var root = xdoc.FirstChild; // data
            var hs = new HashSet<string>();


            if (args.Length < 2) return hs.ToList();

            root["files"].InnerText = string.Empty;

            if (args[1].ToLowerInvariant() != "all")
            {
                listSql().ForEach(p =>hs.Add(p));

                args.Skip(1).Where(q=> hs.Contains(q)).ToList().ForEach(p =>hs.Remove(p));

                hs.ToList().ForEach(p => root["files"].PrependChild(xdoc.CreateElement("file")).InnerText = p);

            }
            xdoc.Save(_filename);

            return hs.ToList();
        }

        public List<string> listSql()
        {
            var loadList = new List<string>();
            var x = getfilenames(FILES_NODE);
            while (x.MoveNext())
            {
                loadList.Add(((XmlElement)x.Current).InnerText);
            }

            return loadList;
        }

        public bool exists(string filename) {
            return this.exists(filename, getfilenames(FILES_NODE));
        }
        public bool exists(string filename, IEnumerator x) {
            if(filename[0] == '\\') filename = filename.Substring(1);

            while(x.MoveNext()) {
                string y = ((XmlElement)x.Current).InnerText;
                if(y[0] == '\\') y = ((XmlElement)x.Current).InnerText.Substring(1);
                if(filename == y) return true;
            }
            return false;
        }

        private IEnumerator getfilenames(string nodeName) {
            var xdoc = readXmldoc();
            var root = xdoc.FirstChild;
            return root[nodeName].ChildNodes.GetEnumerator();
        }
    }
}
