using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sql;
using sqlApi;

namespace sqlApi_test
{
    [TestClass]
    public class dir_test
    {
        private dir _dir;

        public dir_test()
        {
            _dir = new dir(constantinople.XML_FILEPATH);
        }
        [TestMethod]
        public void cstr_test_1_param()
        {
            Assert.IsNotNull(_dir);
        }

        [TestMethod]
        public void cstr_test_can_read_params()
        {
            // this is dependent on there being values in the xml file
            Assert.IsNotNull(_dir.Dirpath);
            Assert.IsNotNull(_dir.Recursive);
        }

        [TestMethod]
        public void cstr_test_3_params()
        {
            string dirExpected = @"c:\somewhere\somewhere", dirActual;
            bool recursiveExpected = true, recursiveActual; // 50:50 chance of passing anyway
            _dir = new dir(constantinople.XML_FILEPATH, dirExpected, recursiveExpected);

            dirActual = _dir.Dirpath;
            recursiveActual = _dir.Recursive;

            Assert.AreEqual(dirExpected, dirActual);
            Assert.AreEqual(recursiveExpected, recursiveActual);
        }

        [TestMethod]
        public void save_can_save_params_and_retrieve()
        {
            string dirExpected = @"c:\somewhere\somewhere", dirActual, dirOriginal;
            bool recursiveExpected = true, recursiveActual, recusiveOriginal;

            _dir = new dir(constantinople.XML_FILEPATH);

            // keep the originals
            dirOriginal = _dir.Dirpath;
            recusiveOriginal = _dir.Recursive;

            _dir = new dir(constantinople.XML_FILEPATH, dirExpected, recursiveExpected);

            _dir.save();

            _dir = new dir(constantinople.XML_FILEPATH);

            dirActual = _dir.Dirpath;
            recursiveActual = _dir.Recursive;

            Assert.AreEqual(dirExpected, dirActual);
            Assert.AreEqual(recursiveExpected, recursiveActual);

            _dir = new dir(constantinople.XML_FILEPATH, dirOriginal, recusiveOriginal);

            _dir.save();

        }

        //[TestMethod]
        //public void leaveSql_can_work()
        //{
        //    _dir = new dir(constantinople.XML_FILEPATH);
        //    string[] args = { "leave", "test1", "test2", "test3" };
        //    var expectedCount = 3;
        //    var actualCount = _dir.leaveSql(args).Count;
        //    //Assert.AreEqual(expectedCount, actualCount);
        //}

        [TestMethod]
        public void listSql_not_a_test()
        {
            _dir = new dir(constantinople.XML_FILEPATH);
            var x = _dir.listSql();
        }
    }
}
