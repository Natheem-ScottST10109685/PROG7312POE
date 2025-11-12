using System;
using System.Collections.Generic;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Red-Black Tree (self-balancing BST) for efficient O(log n) operations
    /// Maintains balance through color properties and rotations
    /// </summary>
    public class RedBlackTree
    {
        private class RBNode
        {
            public int Id { get; set; }
            public ServiceRequest Request { get; set; }
            public RBNode Left { get; set; }
            public RBNode Right { get; set; }
            public RBNode Parent { get; set; }
            public bool IsRed { get; set; }

            public RBNode(int id, ServiceRequest request, bool isRed = true)
            {
                Id = id;
                Request = request;
                IsRed = isRed;
            }
        }

        private RBNode root;
        private readonly RBNode nil; // Sentinel node

        public RedBlackTree()
        {
            nil = new RBNode(0, null, false); // Black sentinel
            root = nil;
        }

        /// <summary>
        /// Insert a service request into the Red-Black tree
        /// </summary>
        public void Insert(int id, ServiceRequest request)
        {
            RBNode newNode = new RBNode(id, request);
            newNode.Left = nil;
            newNode.Right = nil;

            RBNode parent = nil;
            RBNode current = root;

            // Find position for new node
            while (current != nil)
            {
                parent = current;
                if (newNode.Id < current.Id)
                    current = current.Left;
                else if (newNode.Id > current.Id)
                    current = current.Right;
                else
                    return; // Duplicate not allowed
            }

            newNode.Parent = parent;

            if (parent == nil)
                root = newNode;
            else if (newNode.Id < parent.Id)
                parent.Left = newNode;
            else
                parent.Right = newNode;

            // Fix Red-Black properties
            InsertFixup(newNode);
        }

        /// <summary>
        /// Fix Red-Black tree properties after insertion
        /// </summary>
        private void InsertFixup(RBNode node)
        {
            while (node.Parent.IsRed)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    RBNode uncle = node.Parent.Parent.Right;

                    if (uncle.IsRed)
                    {
                        // Case 1: Uncle is red
                        node.Parent.IsRed = false;
                        uncle.IsRed = false;
                        node.Parent.Parent.IsRed = true;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            // Case 2: Uncle is black, node is right child
                            node = node.Parent;
                            LeftRotate(node);
                        }
                        // Case 3: Uncle is black, node is left child
                        node.Parent.IsRed = false;
                        node.Parent.Parent.IsRed = true;
                        RightRotate(node.Parent.Parent);
                    }
                }
                else
                {
                    RBNode uncle = node.Parent.Parent.Left;

                    if (uncle.IsRed)
                    {
                        node.Parent.IsRed = false;
                        uncle.IsRed = false;
                        node.Parent.Parent.IsRed = true;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        node.Parent.IsRed = false;
                        node.Parent.Parent.IsRed = true;
                        LeftRotate(node.Parent.Parent);
                    }
                }
            }
            root.IsRed = false;
        }

        /// <summary>
        /// Search for a service request by ID - O(log n)
        /// </summary>
        public ServiceRequest Search(int id)
        {
            RBNode node = SearchNode(root, id);
            return node == nil ? null : node.Request;
        }

        private RBNode SearchNode(RBNode node, int id)
        {
            if (node == nil || id == node.Id)
                return node;

            if (id < node.Id)
                return SearchNode(node.Left, id);
            else
                return SearchNode(node.Right, id);
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

        private void InOrderRec(RBNode node, List<ServiceRequest> result)
        {
            if (node != nil)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Request);
                InOrderRec(node.Right, result);
            }
        }

        /// <summary>
        /// Left rotation
        /// </summary>
        private void LeftRotate(RBNode x)
        {
            RBNode y = x.Right;
            x.Right = y.Left;

            if (y.Left != nil)
                y.Left.Parent = x;

            y.Parent = x.Parent;

            if (x.Parent == nil)
                root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }

        /// <summary>
        /// Right rotation
        /// </summary>
        private void RightRotate(RBNode y)
        {
            RBNode x = y.Left;
            y.Left = x.Right;

            if (x.Right != nil)
                x.Right.Parent = y;

            x.Parent = y.Parent;

            if (y.Parent == nil)
                root = x;
            else if (y == y.Parent.Right)
                y.Parent.Right = x;
            else
                y.Parent.Left = x;

            x.Right = y;
            y.Parent = x;
        }

        /// <summary>
        /// Get total number of nodes
        /// </summary>
        public int GetNodeCount()
        {
            return CountNodes(root);
        }

        private int CountNodes(RBNode node)
        {
            if (node == nil)
                return 0;
            return 1 + CountNodes(node.Left) + CountNodes(node.Right);
        }

        /// <summary>
        /// Get black height (for verification)
        /// </summary>
        public int GetBlackHeight()
        {
            return CalculateBlackHeight(root);
        }

        private int CalculateBlackHeight(RBNode node)
        {
            if (node == nil)
                return 0;

            int leftHeight = CalculateBlackHeight(node.Left);
            if (!node.IsRed)
                leftHeight++;

            return leftHeight;
        }

        /// <summary>
        /// Verify Red-Black tree properties
        /// </summary>
        public bool VerifyProperties()
        {
            // Property 1: Root is black
            if (root != nil && root.IsRed)
                return false;

            // Property 2: All leaves (nil) are black (automatically satisfied)
            // Property 3: Red node has black children
            // Property 4: All paths have same black height
            return VerifyNode(root) != -1;
        }

        private int VerifyNode(RBNode node)
        {
            if (node == nil)
                return 0;

            // Check for consecutive red nodes
            if (node.IsRed)
            {
                if (node.Left.IsRed || node.Right.IsRed)
                    return -1;
            }

            int leftBlackHeight = VerifyNode(node.Left);
            int rightBlackHeight = VerifyNode(node.Right);

            if (leftBlackHeight == -1 || rightBlackHeight == -1)
                return -1;

            if (leftBlackHeight != rightBlackHeight)
                return -1;

            return leftBlackHeight + (node.IsRed ? 0 : 1);
        }
    }
}