using System;
using System.Collections.Generic;

namespace ST10109685Prog7312POE
{
    /// <summary>
    /// Binary Search Tree implementation for efficient service request lookup by IssueId.
    /// Provides O(log n) average search time for balanced trees.
    /// </summary>
    public class BinarySearchTree
    {
        private class Node
        {
            public ReportedIssue Issue;
            public Node Left;
            public Node Right;

            public Node(ReportedIssue issue)
            {
                Issue = issue;
            }
        }

        private Node root;
        private int count;

        public BinarySearchTree()
        {
            root = null;
            count = 0;
        }

        /// <summary>
        /// Inserts a new issue into the BST based on IssueId.
        /// Time Complexity: O(log n) average, O(n) worst case
        /// </summary>
        public void Insert(ReportedIssue issue)
        {
            if (issue == null) return;
            root = Insert(root, issue);
            count++;
        }

        private Node Insert(Node node, ReportedIssue issue)
        {
            if (node == null) return new Node(issue);

            if (issue.IssueId < node.Issue.IssueId)
                node.Left = Insert(node.Left, issue);
            else if (issue.IssueId > node.Issue.IssueId)
                node.Right = Insert(node.Right, issue);
            // If equal, we don't insert duplicate IDs

            return node;
        }

        /// <summary>
        /// Searches for an issue by IssueId using binary search.
        /// Time Complexity: O(log n) average, O(n) worst case
        /// </summary>
        public ReportedIssue Search(int issueId)
        {
            Node cur = root;
            while (cur != null)
            {
                if (issueId == cur.Issue.IssueId)
                    return cur.Issue;
                cur = issueId < cur.Issue.IssueId ? cur.Left : cur.Right;
            }
            return null;
        }

        /// <summary>
        /// Returns the search path taken to find an issue (for visualization).
        /// Useful for demonstrating BST traversal efficiency.
        /// </summary>
        public List<int> GetSearchPath(int issueId)
        {
            var path = new List<int>();
            Node cur = root;

            while (cur != null)
            {
                path.Add(cur.Issue.IssueId);
                if (issueId == cur.Issue.IssueId)
                    break;
                cur = issueId < cur.Issue.IssueId ? cur.Left : cur.Right;
            }

            return path;
        }

        /// <summary>
        /// Returns all issues in sorted order (in-order traversal).
        /// Time Complexity: O(n)
        /// </summary>
        public List<ReportedIssue> InOrder()
        {
            var list = new List<ReportedIssue>();
            InOrder(root, list);
            return list;
        }

        private void InOrder(Node node, List<ReportedIssue> list)
        {
            if (node == null) return;
            InOrder(node.Left, list);
            list.Add(node.Issue);
            InOrder(node.Right, list);
        }

        /// <summary>
        /// Returns all issues in pre-order (root, left, right).
        /// </summary>
        public List<ReportedIssue> PreOrder()
        {
            var list = new List<ReportedIssue>();
            PreOrder(root, list);
            return list;
        }

        private void PreOrder(Node node, List<ReportedIssue> list)
        {
            if (node == null) return;
            list.Add(node.Issue);
            PreOrder(node.Left, list);
            PreOrder(node.Right, list);
        }

        /// <summary>
        /// Returns all issues in post-order (left, right, root).
        /// </summary>
        public List<ReportedIssue> PostOrder()
        {
            var list = new List<ReportedIssue>();
            PostOrder(root, list);
            return list;
        }

        private void PostOrder(Node node, List<ReportedIssue> list)
        {
            if (node == null) return;
            PostOrder(node.Left, list);
            PostOrder(node.Right, list);
            list.Add(node.Issue);
        }

        /// <summary>
        /// Finds the minimum issue ID in the tree.
        /// </summary>
        public ReportedIssue FindMin()
        {
            if (root == null) return null;
            Node cur = root;
            while (cur.Left != null)
                cur = cur.Left;
            return cur.Issue;
        }

        /// <summary>
        /// Finds the maximum issue ID in the tree.
        /// </summary>
        public ReportedIssue FindMax()
        {
            if (root == null) return null;
            Node cur = root;
            while (cur.Right != null)
                cur = cur.Right;
            return cur.Issue;
        }

        /// <summary>
        /// Returns the number of nodes in the BST.
        /// </summary>
        public int Count()
        {
            return count;
        }

        /// <summary>
        /// Calculates the height of the BST.
        /// Height is used to assess tree balance.
        /// </summary>
        public int Height()
        {
            return Height(root);
        }

        private int Height(Node node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        /// <summary>
        /// Checks if the tree is empty.
        /// </summary>
        public bool IsEmpty()
        {
            return root == null;
        }
    }
}