using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace sqlApi
{
    public abstract class entityCore
    {
        protected string _filename;

        public entityCore(string filename)
        {
            _filename = filename;
        }

        public abstract void save();

        protected XmlDocument readXmldoc()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(_filename);
            return xdoc;
        }
    }
}
