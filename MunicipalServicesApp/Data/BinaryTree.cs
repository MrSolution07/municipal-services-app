using System.Collections.Generic;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// A binary node used as a left-child / right-sibling encoding of the category tree.
    /// Left is the first subcategory. Right is the next sibling.
    /// </summary>
    public class BinaryNode
    {
        public string Name { get; set; }

        public BinaryNode Left { get; set; }

        public BinaryNode Right { get; set; }
    }

    /// <summary>Converts the n-ary category tree into a binary tree and walks it.</summary>
    public static class BinaryEncoding
    {
        public static BinaryNode FromCategory(CategoryNode root)
        {
            if (root == null)
            {
                return null;
            }

            return Encode(root);
        }

        public static string Preorder(BinaryNode node)
        {
            List<string> names = new List<string>();
            Walk(node, names);
            return string.Join(", ", names.ToArray());
        }

        /// <summary>Nodes on the longest root-to-leaf path. An empty tree has height 0.</summary>
        public static int Height(BinaryNode node)
        {
            if (node == null)
            {
                return 0;
            }

            int left = Height(node.Left);
            int right = Height(node.Right);
            return 1 + (left > right ? left : right);
        }

        private static BinaryNode Encode(CategoryNode node)
        {
            BinaryNode binary = new BinaryNode { Name = node.Name };
            binary.Left = EncodeChildren(node.Children, 0);
            return binary;
        }

        private static BinaryNode EncodeChildren(IList<CategoryNode> children, int index)
        {
            if (children == null || index >= children.Count)
            {
                return null;
            }

            BinaryNode binary = new BinaryNode { Name = children[index].Name };
            binary.Left = EncodeChildren(children[index].Children, 0);
            binary.Right = EncodeChildren(children, index + 1);
            return binary;
        }

        private static void Walk(BinaryNode node, List<string> names)
        {
            if (node == null)
            {
                return;
            }

            names.Add(node.Name);
            Walk(node.Left, names);
            Walk(node.Right, names);
        }
    }
}
