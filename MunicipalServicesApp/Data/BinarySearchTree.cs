using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// Binary search tree of request references. Insert and search only.
    /// Height is the number of nodes on the longest root-to-leaf path.
    /// </summary>
    public class BinarySearchTree
    {
        private sealed class Node
        {
            public string Key;
            public Node Left;
            public Node Right;
        }

        private Node root;

        public int Count { get; private set; }

        public void Insert(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("A reference is required.", "key");
            }

            bool added = false;
            root = Insert(root, key, ref added);
            if (added)
            {
                Count++;
            }
        }

        public bool Contains(string key)
        {
            ReferenceSearch search = Search(key);
            return search.Found;
        }

        /// <summary>Every node compared, in order, including the match when it is found.</summary>
        public ReferenceSearch Search(string key)
        {
            ReferenceSearch result = new ReferenceSearch();
            Node current = root;
            while (current != null)
            {
                result.Visited.Add(current.Key);
                int compare = string.Compare(key, current.Key, StringComparison.Ordinal);
                if (compare == 0)
                {
                    result.Found = true;
                    return result;
                }

                current = compare < 0 ? current.Left : current.Right;
            }

            return result;
        }

        public int Height
        {
            get { return HeightOf(root); }
        }

        private static int HeightOf(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            int left = HeightOf(node.Left);
            int right = HeightOf(node.Right);
            return 1 + (left > right ? left : right);
        }

        private static Node Insert(Node node, string key, ref bool added)
        {
            if (node == null)
            {
                added = true;
                return new Node { Key = key };
            }

            int compare = string.Compare(key, node.Key, StringComparison.Ordinal);
            if (compare < 0)
            {
                node.Left = Insert(node.Left, key, ref added);
            }
            else if (compare > 0)
            {
                node.Right = Insert(node.Right, key, ref added);
            }

            return node;
        }
    }

    /// <summary>The path a binary search walked, and whether the reference was found.</summary>
    public class ReferenceSearch
    {
        public ReferenceSearch()
        {
            Visited = new List<string>();
        }

        public bool Found { get; set; }

        public List<string> Visited { get; private set; }
    }
}
