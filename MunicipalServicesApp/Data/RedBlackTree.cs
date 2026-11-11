using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// Sort key for the red-black tree. A higher priority comes first, then an earlier
    /// submission, then the reference. Inorder of the tree is therefore most-urgent first.
    /// </summary>
    public sealed class UrgencyKey : IComparable<UrgencyKey>
    {
        public int Priority { get; set; }

        public DateTime SubmittedAt { get; set; }

        public string Reference { get; set; }

        public int CompareTo(UrgencyKey other)
        {
            if (other == null)
            {
                return 1;
            }

            int compare = other.Priority.CompareTo(Priority);
            if (compare != 0)
            {
                return compare;
            }

            compare = SubmittedAt.CompareTo(other.SubmittedAt);
            if (compare != 0)
            {
                return compare;
            }

            return string.Compare(Reference, other.Reference, StringComparison.Ordinal);
        }
    }

    /// <summary>
    /// Red-black tree of urgency keys. Insert and fixup only. The store builds a new
    /// tree when a request is resolved, instead of deleting a node.
    /// </summary>
    public class RedBlackTree
    {
        private sealed class Node
        {
            public UrgencyKey Key;
            public Node Left;
            public Node Right;
            public Node Parent;
            public bool Red;
        }

        private Node root;

        public void Insert(UrgencyKey key)
        {
            if (key == null || string.IsNullOrEmpty(key.Reference))
            {
                throw new ArgumentException("An urgency key and reference are required.", "key");
            }

            Node node = new Node { Key = key, Red = true };
            Node parent = null;
            Node current = root;
            while (current != null)
            {
                parent = current;
                int compare = key.CompareTo(current.Key);
                if (compare == 0)
                {
                    return;
                }

                current = compare < 0 ? current.Left : current.Right;
            }

            node.Parent = parent;
            if (parent == null)
            {
                root = node;
            }
            else if (key.CompareTo(parent.Key) < 0)
            {
                parent.Left = node;
            }
            else
            {
                parent.Right = node;
            }

            InsertFixup(node);
        }

        public List<UrgencyKey> InOrder()
        {
            List<UrgencyKey> keys = new List<UrgencyKey>();
            Walk(root, keys);
            return keys;
        }

        public void Validate()
        {
            if (root == null)
            {
                return;
            }

            if (root.Red)
            {
                throw new InvalidOperationException("Red-black root must be black.");
            }

            int ignored = BlackHeight(root);
            if (ignored < 0)
            {
                throw new InvalidOperationException("Red-black black-height differs between branches, or a red node has a red child.");
            }
        }

        private void InsertFixup(Node node)
        {
            while (node.Parent != null && node.Parent.Red)
            {
                Node grandparent = node.Parent.Parent;
                if (grandparent == null)
                {
                    break;
                }

                if (node.Parent == grandparent.Left)
                {
                    Node uncle = grandparent.Right;
                    if (uncle != null && uncle.Red)
                    {
                        node.Parent.Red = false;
                        uncle.Red = false;
                        grandparent.Red = true;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }

                        node.Parent.Red = false;
                        node.Parent.Parent.Red = true;
                        RotateRight(node.Parent.Parent);
                    }
                }
                else
                {
                    Node uncle = grandparent.Left;
                    if (uncle != null && uncle.Red)
                    {
                        node.Parent.Red = false;
                        uncle.Red = false;
                        grandparent.Red = true;
                        node = grandparent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }

                        node.Parent.Red = false;
                        node.Parent.Parent.Red = true;
                        RotateLeft(node.Parent.Parent);
                    }
                }
            }

            if (root != null)
            {
                root.Red = false;
            }
        }

        private void RotateLeft(Node node)
        {
            Node pivot = node.Right;
            node.Right = pivot.Left;
            if (pivot.Left != null)
            {
                pivot.Left.Parent = node;
            }

            pivot.Parent = node.Parent;
            if (node.Parent == null)
            {
                root = pivot;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = pivot;
            }
            else
            {
                node.Parent.Right = pivot;
            }

            pivot.Left = node;
            node.Parent = pivot;
        }

        private void RotateRight(Node node)
        {
            Node pivot = node.Left;
            node.Left = pivot.Right;
            if (pivot.Right != null)
            {
                pivot.Right.Parent = node;
            }

            pivot.Parent = node.Parent;
            if (node.Parent == null)
            {
                root = pivot;
            }
            else if (node == node.Parent.Right)
            {
                node.Parent.Right = pivot;
            }
            else
            {
                node.Parent.Left = pivot;
            }

            pivot.Right = node;
            node.Parent = pivot;
        }

        private static void Walk(Node node, List<UrgencyKey> keys)
        {
            if (node == null)
            {
                return;
            }

            Walk(node.Left, keys);
            keys.Add(node.Key);
            Walk(node.Right, keys);
        }

        /// <summary>Returns the black-height, or -1 when an invariant fails.</summary>
        private static int BlackHeight(Node node)
        {
            if (node == null)
            {
                return 1;
            }

            if (node.Red && ((node.Left != null && node.Left.Red) || (node.Right != null && node.Right.Red)))
            {
                return -1;
            }

            int left = BlackHeight(node.Left);
            int right = BlackHeight(node.Right);
            if (left < 0 || right < 0 || left != right)
            {
                return -1;
            }

            return left + (node.Red ? 0 : 1);
        }
    }
}
