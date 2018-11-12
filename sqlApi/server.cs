using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace sqlApi
{
    public class server : entityCore
    {
        public string Name { get; private set; }
        public string Db { get; private set; }
        public string connectionString { get { return string.Format($"Data Source = {Name}; Initial Catalog = {Db}; Integrated Security = True"); } }

        public server(string filename):base(filename)
        {
            var root = readXmldoc().FirstChild; // data
            Name = root["server"]["name"].InnerText;
            Db = root["server"]["Db"].InnerText;
        }

        public server (string filename, string name, string db) : this(filename)
        {
            Name = string.IsNullOrWhiteSpace(name) ? Name : name;
            Db = string.IsNullOrWhiteSpace(db) ? Db : db;
        }

        public override void save()
        {
            var xdoc = readXmldoc();
            var root = xdoc.FirstChild; // data
            root["server"]["name"].InnerText = Name;
            root["server"]["Db"].InnerText = Db;
            xdoc.Save(_filename);
        }
    }
}
