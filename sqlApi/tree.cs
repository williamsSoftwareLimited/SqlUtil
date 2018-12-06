using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlApi {
    public class tree {

        readonly string ROOTVALUE="$$ROOT$$";

        public tree() { // this rootValue is ignored
            Root = new node(ROOTVALUE);
        }


        public void add(string value) {

            node currentNode = Root;
            add(currentNode, value, 0);

        }

        public void add(node currentNode, string value, int index) {
            
            if(index >= currentNode.Children.Count()) {
                currentNode.Children.Add(new node(value));
            } else {

                int matchSize = compareStr(currentNode.Children[index].Value, value);

                if(matchSize > 0) {

                    if(!string.IsNullOrWhiteSpace(currentNode.Children[index].Value.Substring(matchSize))) {
                        node newnode = new node(currentNode.Children[index].Value.Substring(0, matchSize)) ;                    
                        newnode.Children.Add(new node(currentNode.Children[index].Value.Substring(matchSize)){ Children = currentNode.Children[index].Children });
                        currentNode.Children[index] = newnode;
                    }

                    add(currentNode.Children[index], value.Substring(matchSize), 0);

                } else { 
                    // nothing in common with current node
                    add(currentNode, value, index + 1);
                }
            }
        }

        

        // returns the index position after, where the two strings match 
        // if no match then returns 0
        public int compareStr(string a, string b) {
            int index = 0;
            Tuple<string, string> tuple = shortestLargest(a, b);
            
            for(; index < tuple.Item1.Length; index++) {
                if(a[index] != b[index]) return index; 
            }

            return index;
        }

        // returns the shortest string as the first item in the tuple
        public Tuple<string, string> shortestLargest(string a, string b) {
            if(b.Length >= a.Length) {
                return new Tuple<string, string>(a, b);
            } 
            return new Tuple<string, string>(b, a);

        }

        // nb the root can have a value but it will be ignored
        public node Root { get; private set; }

        public class node {

            public node(string val) {
                Value = val;
                Children = new List<node>();
            }

            public string Value { get; set; }
            public List<node> Children { get; set; }

        }

     }
}
