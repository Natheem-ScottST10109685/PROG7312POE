using System;
using System.Collections.Generic;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// AVL Tree (self-balancing BST) for efficient O(log n) operations
    /// Ensures tree remains balanced after insertions
    /// </summary>
    public class AVLTree
    {
        private class AVLNode
        {
            public int Id { get; set; }
            public ServiceRequest Request { get; set; }
            public AVLNode Left { get; set; }
            public AVLNode Right { get; set; }
            public int Height { get; set; }

            public AVLNode(int id, ServiceRequest request)
            {
                Id = id;
                Request = request;
                Height = 1;
            }
        }

        private AVLNode root;

        /// <summary>
        /// Insert a service request into the AVL tree
        /// </summary>
        public void Insert(int id, ServiceRequest request)
        {
            root = InsertRec(root, id, request);
        }

        private AVLNode InsertRec(AVLNode node, int id, ServiceRequest request)
        {
            // Standard BST insertion
            if (node == null)
                return new AVLNode(id, request);

            if (id < node.Id)
                node.Left = InsertRec(node.Left, id, request);
            else if (id > node.Id)
                node.Right = InsertRec(node.Right, id, request);
            else
                return node; // Duplicate not allowed

            // Update height
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // Get balance factor
            int balance = GetBalance(node);

            // Left-Left Case
            if (balance > 1 && id < node.Left.Id)
                return RightRotate(node);

            // Right-Right Case
            if (balance < -1 && id > node.Right.Id)
                return LeftRotate(node);

            // Left-Right Case
            if (balance > 1 && id > node.Left.Id)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right-Left Case
            if (balance < -1 && id < node.Right.Id)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        /// <summary>
        /// Search for a service request by ID - O(log n)
        /// </summary>
        public ServiceRequest Search(int id)
        {
            return SearchRec(root, id);
        }

        private ServiceRequest SearchRec(AVLNode node, int id)
        {
            if (node == null)
                return null;

            if (id == node.Id)
                return node.Request;
            else if (id < node.Id)
                return SearchRec(node.Left, id);
            else
                return SearchRec(node.Right, id);
        }

        /// <summary>
        /// Get all requests in sorted order (in-order traversal)
        /// </summary>
        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(AVLNode node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Request);
                InOrderRec(node.Right, result);
            }
        }

        /// <summary>
        /// Get tree height
        /// </summary>
        private int GetHeight(AVLNode node)
        {
            return node == null ? 0 : node.Height;
        }

        /// <summary>
        /// Get balance factor of node
        /// </summary>
        private int GetBalance(AVLNode node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }

        /// <summary>
        /// Right rotation for balancing
        /// </summary>
        private AVLNode RightRotate(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            // Perform rotation
            x.Right = y;
            y.Left = T2;

            // Update heights
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }

        /// <summary>
        /// Left rotation for balancing
        /// </summary>
        private AVLNode LeftRotate(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;

            // Perform rotation
            y.Left = x;
            x.Right = T2;

            // Update heights
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        /// <summary>
        /// Get total number of nodes
        /// </summary>
        public int GetNodeCount()
        {
            return CountNodes(root);
        }

        private int CountNodes(AVLNode node)
        {
            if (node == null)
                return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }

        /// <summary>
        /// Get the actual tree height
        /// </summary>
        public int GetTreeHeight()
        {
            return GetHeight(root);
        }

        /// <summary>
        /// Check if tree is balanced (for verification)
        /// </summary>
        public bool IsBalanced()
        {
            return CheckBalance(root) != -1;
        }

        private int CheckBalance(AVLNode node)
        {
            if (node == null)
                return 0;

            int leftHeight = CheckBalance(node.Left);
            if (leftHeight == -1)
                return -1;

            int rightHeight = CheckBalance(node.Right);
            if (rightHeight == -1)
                return -1;

            if (Math.Abs(leftHeight - rightHeight) > 1)
                return -1;

            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
}