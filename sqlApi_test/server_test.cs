using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sql;
using sqlApi;

namespace sqlApi_test
{
    [TestClass]
    public class server_test
    {
        server _server;

        [TestMethod]
        public void cstr_test()
        {
            _server = new server(constantinople.XML_FILEPATH);

            Assert.IsNotNull(_server);
        }

        [TestMethod]
        public void cstr_with_3_params_test()
        {
            _server = new server(constantinople.XML_FILEPATH, "newServerName", "newDbName");

            Assert.IsNotNull(_server);
        }

        [TestMethod]
        public void cstr_is_reading_from_xml_file()
        {
            // this is reliant on there being data in the XML file
            _server = new server(constantinople.XML_FILEPATH);

            Assert.IsNotNull(_server.Name);
            Assert.IsNotNull(_server.Db);
        }

        [TestMethod]
        public void cstr_changes_servername_and_not_dbname()
        {
            string oldServername, newServername = "newServerName",
                   oldDbname, newDbname = "newDbName";

            _server = new server(constantinople.XML_FILEPATH);
            oldServername = _server.Name; // this could be null or empty
            oldDbname = _server.Db;// this could be null or empty

            _server = new server(constantinople.XML_FILEPATH, name:newServername, db:string.Empty);

            newDbname = _server.Db; // this shouln't be changed

            Assert.AreEqual(oldDbname, newDbname);

            Assert.AreEqual(newServername, _server.Name);
        }

        [TestMethod]
        public void cstr_changes_dbname_and_not_servername()
        {
            string oldServername, newServername = "newServerName",
                   oldDbname, newDbname = "newDbName";

            _server = new server(constantinople.XML_FILEPATH);
            oldServername = _server.Name; // this could be null or empty
            oldDbname = _server.Db;// this could be null or empty

            _server = new server(constantinople.XML_FILEPATH, name: string.Empty, db: newDbname);

            newServername = _server.Name; // this shouldn't be changed

            Assert.AreEqual(oldServername, newServername);

            Assert.AreEqual(newDbname, _server.Db);
        }

        // this is not necessary a unit test (more integration)
        [TestMethod]
        public void save_saves_to_disk()
        {
            string oldServername, newServername = "newServerName",
                   oldDbname, newDbname = "newDbName";

            _server = new server(constantinople.XML_FILEPATH);
            oldServername = _server.Name; // this could be null or empty
            oldDbname = _server.Db;// this could be null or empty

            _server = new server(constantinople.XML_FILEPATH, name: newServername, db: newDbname);

            _server.save();

            // reload
            _server = new server(constantinople.XML_FILEPATH);
            // if this has gone wrong then the setting on the XML file have been lost forever

            Assert.AreEqual(newServername, _server.Name);
            Assert.AreEqual(newDbname, _server.Db);

            // normalise - if possible
            _server = new server(constantinople.XML_FILEPATH, name: oldServername, db: oldDbname);
            _server.save();
        }
    }
}
