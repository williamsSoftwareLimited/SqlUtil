using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sqlApi;

namespace sqlApi_test {
    [TestClass]
    public class findNextWord_test {

        [TestMethod]
        public void findPosition_cstr_test() {
            var findPos = new findNextWord("findNextWord_test_sp.sql");
            Assert.IsNotNull(findPos);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void findPosition_cstr_throws_test() {
            var findPos = new findNextWord("apples.sql");
            Assert.IsNotNull(findPos);
        }

        [TestMethod]
        public void findPosition_find_name_test() {
            string expected= "dbo.test_sp", actual="";

            var findPos = new findNextWord("findNextWord_test_sp.sql");

            actual = findPos.find("procedure");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void findPosition_cant_find_name_test() {
            string expected = "**Not_Found**", actual = "";

            var findPos = new findNextWord("findNextWord_test_sp.sql");

            actual = findPos.find("asdfasfasdf");
            Assert.AreEqual(expected, actual);
        }
    }
}
