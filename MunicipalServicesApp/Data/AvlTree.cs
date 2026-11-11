using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// AVL tree of request references. Insert rebalances with rotations.
    /// There is no delete: the store builds a new tree when the set changes.
    /// Height counts nodes on the longest path. An empty tree has height 0.
    /// </summary>
    public class AvlTree
    {
        private sealed class Node
        {
            public string Key;
            public Node Left;
            public Node Right;
            public int Height;
        }

        private Node root;

        public int Height
        {
            get { return HeightOf(root); }
        }

        public void Insert(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("A reference is required.", "key");
            }

            root = Insert(root, key);
        }

        public List<string> InOrder()
        {
            List<string> keys = new List<string>();
            Walk(root, keys);
            return keys;
        }

        public void Validate()
        {
            ValidateNode(root);
        }

        private static int HeightOf(Node node)
        {
            return node == null ? 0 : node.Height;
        }

        private static void Update(Node node)
        {
            int left = HeightOf(node.Left);
            int right = HeightOf(node.Right);
            node.Height = 1 + (left > right ? left : right);
        }

        private static int Balance(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return HeightOf(node.Left) - HeightOf(node.Right);
        }

        private static Node Insert(Node node, string key)
        {
            if (node == null)
            {
                return new Node { Key = key, Height = 1 };
            }

            int compare = string.Compare(key, node.Key, StringComparison.Ordinal);
            if (compare < 0)
            {
                node.Left = Insert(node.Left, key);
            }
            else if (compare > 0)
            {
                node.Right = Insert(node.Right, key);
            }
            else
            {
                return node;
            }

            Update(node);
            int balance = Balance(node);

            if (balance > 1 && string.Compare(key, node.Left.Key, StringComparison.Ordinal) < 0)
            {
                return RotateRight(node);
            }

            if (balance < -1 && string.Compare(key, node.Right.Key, StringComparison.Ordinal) > 0)
            {
                return RotateLeft(node);
            }

            if (balance > 1 && string.Compare(key, node.Left.Key, StringComparison.Ordinal) > 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && string.Compare(key, node.Right.Key, StringComparison.Ordinal) < 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        private static Node RotateLeft(Node node)
        {
            Node pivot = node.Right;
            node.Right = pivot.Left;
            pivot.Left = node;
            Update(node);
            Update(pivot);
            return pivot;
        }

        private static Node RotateRight(Node node)
        {
            Node pivot = node.Left;
            node.Left = pivot.Right;
            pivot.Right = node;
            Update(node);
            Update(pivot);
            return pivot;
        }

        private static void Walk(Node node, List<string> keys)
        {
            if (node == null)
            {
                return;
            }

            Walk(node.Left, keys);
            keys.Add(node.Key);
            Walk(node.Right, keys);
        }

        private static int ValidateNode(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            int left = ValidateNode(node.Left);
            int right = ValidateNode(node.Right);
            int expected = 1 + (left > right ? left : right);
            if (node.Height != expected)
            {
                throw new InvalidOperationException("AVL height is out of date at " + node.Key + ".");
            }

            int balance = left - right;
            if (balance > 1 || balance < -1)
            {
                throw new InvalidOperationException("AVL node " + node.Key + " has balance factor " + balance + ".");
            }

            return node.Height;
        }
    }
}
