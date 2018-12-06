using Microsoft.VisualStudio.TestTools.UnitTesting;
using sqlApi;
using System.Collections.Generic;

namespace sqlApi_test {
    [TestClass]
    public class tree_test {
        [TestMethod]
        public void tree_cstr_test() {
            tree t = new tree();
            Assert.IsNotNull(t);
        }

        [TestMethod]
        public void check_root_node() {
            string expected = "$$ROOT$$", actual;
            tree t = new tree();
            actual = t.Root.Value;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void check_adds_extra_Value() {
            string expected = "xmr", actual;
            tree t = new tree();

            t.Root.Children.Add(new tree.node("ro"));
            t.Root.Children.Add(new tree.node("nl"));
            t.Root.Children.Add(new tree.node("apricot"));

            t.add(expected);


            bool hasValue = false;
            foreach(var node in t.Root.Children) {
                hasValue = expected == node.Value;
                if(hasValue) break;
            }
            Assert.IsTrue(hasValue);
        }

        [TestMethod]
        public void check_finds_match() {
            string expected = "apr", actual;
            tree t = new tree();

            t.Root.Children.Add(new tree.node("ro"));
            t.Root.Children.Add(new tree.node("nl"));
            t.Root.Children.Add(new tree.node("apricot"));

            t.Root.Children[2].Children.Add(new tree.node("rap"));

            t.add(expected);


            bool hasValue = false;
            foreach(var node in t.Root.Children) {
                hasValue = expected == node.Value;
                if(hasValue) break;
            }
            Assert.IsTrue(hasValue);
        }

        [TestMethod]
        public void check_adds_if_new_value_is_bigger() {
            string expected = "apr", actual;
            tree t = new tree();

            t.Root.Children.Add(new tree.node("ro"));
            t.Root.Children.Add(new tree.node("nl"));
            t.Root.Children.Add(new tree.node("apr"));

            t.Root.Children[2].Children.Add(new tree.node("rap"));

            t.add(expected);


            bool hasValue = false;
            foreach(var node in t.Root.Children) {
                hasValue = expected == node.Value;
                if(hasValue) break;
            }
            Assert.IsTrue(hasValue);
        }

        [TestMethod]
        public void check_adds_if_both_are_bigger() {
            string expected = "apri", actual;
            tree t = new tree();

            t.Root.Children.Add(new tree.node("ro"));
            t.Root.Children.Add(new tree.node("nl"));
            t.Root.Children.Add(new tree.node("apricot"));

            t.Root.Children[2].Children.Add(new tree.node("rap"));

            t.add("april");


            bool hasValue = false;
            foreach(var node in t.Root.Children) {
                hasValue = expected == node.Value;
                if(hasValue) break;
            }
            Assert.IsTrue(hasValue);
        }

        [TestMethod]
        public void check_constructs_tree_correctly_1() {
            string expected = "apri", actual;
            tree t = new tree();

            t.add("apricot");

            t.add("april");


            bool hasValue = false;
            foreach(var node in t.Root.Children) {
                hasValue = expected == node.Value;
                if(hasValue) break;
            }
            Assert.IsTrue(hasValue);
        }

        [TestMethod]
        public void check_constructs_tree_correctly_2() {
            string expected = "apri", actual;
            tree t = new tree();

            t.add("apricot");

            t.add("april");

            t.add("apricots");

            //t.add("apricots");

            t.add("apricote");


            bool hasValue = false;
            foreach(var node in t.Root.Children) {
                hasValue = expected == node.Value;
                if(hasValue) break;
            }
            Assert.IsTrue(hasValue);
        }

        [TestMethod]
        public void check_constructs_tree_correctly_3() {
            string expected = "apr", actual;
            tree t = new tree();

            t.add("apricot");

            t.add("april");

            t.add("apricots");

            //t.add("apricots");

            t.add("apricote");

            t.add("apress");


            bool hasValue = false;
            foreach(var node in t.Root.Children) {
                hasValue = expected == node.Value;
                if(hasValue) break;
            }
            Assert.IsTrue(hasValue);
        }

        [TestMethod]
        public void check_duplicate_not_added() {
            string expected = "apri", actual;
            tree t = new tree();

            t.add("apricot");

            t.add("april");

            t.add("apricots");

            t.add("apricots");

            bool hasValue = false;
            foreach(var node in t.Root.Children) {
                hasValue = expected == node.Value;
                if(hasValue) break;
            }
            Assert.IsTrue(hasValue);
        }
    }
}
