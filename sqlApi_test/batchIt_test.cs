using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sql;
using sqlApi;

namespace sqlApi_test
{
    [TestClass]
    public class batchIt_test
    {
        [TestMethod]
        public void cstr_test()
        {
            server _server = new server(constantinople.XML_FILEPATH);
            dir _dir = new dir(constantinople.XML_FILEPATH);
            batchIt bi = new batchIt(_dir, new runSql(_server.connectionString, _dir));
            //bi.process();
            Assert.IsNotNull(bi);
        }

        [TestMethod]
        public void remove_test_logic()
        {
            server _server = new server(constantinople.XML_FILEPATH);
            dir _dir = new dir(constantinople.XML_FILEPATH);
            batchIt bi = new batchIt(_dir, new runSql(_server.connectionString, _dir));
            bi.remove(new System.IO.FileInfo("test_sp.sql"));
        }

        [TestMethod]
        public void process_test_logic()
        {
            server _server = new server(constantinople.XML_FILEPATH);
            dir _dir = new dir(constantinople.XML_FILEPATH);
            batchIt bi = new batchIt(_dir, new runSql(_server.connectionString, _dir));
            bi.process();
        } 
    }
}
