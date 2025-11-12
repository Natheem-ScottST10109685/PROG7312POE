using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ST10109685Prog7312POE
{
    public partial class ServiceRequestStatusForm : Form
    {
        // Data structures for managing service requests
        private MinHeap<ServiceRequest> priorityHeap;
        private BinarySearchTree bstById;
        private AVLTree avlTree;
        private RedBlackTree redBlackTree;
        private ServiceRequestGraph requestGraph;
        private List<ServiceRequest> allRequests;

        public ServiceRequestStatusForm()
        {
            InitializeComponent();

            // AUTO-LOAD SAMPLE DATA if no issues exist
            if (IssueManager.GetIssueCount() == 0)
            {
                LoadSampleDataForTesting();
            }

            InitializeDataStructures();
            LoadServiceRequests();
            DisplayStatistics();
            RefreshListView();
            UpdateHeapTopDisplay();
        }

        /// <summary>
        /// Load sample data automatically for testing purposes
        /// </summary>
        private void LoadSampleDataForTesting()
        {
            Random rand = new Random(42);

            string[] locations = new string[]
            {
                "Main Street & 5th Avenue", "Hillside Park", "Downtown Mall",
                "Riverside Estate", "Industrial Zone A", "Greenwood Suburb",
                "Central Business District", "Oakwood Residential",
                "Lakeside Drive", "University Campus"
            };

            string[] categories = new string[]
            {
                "Water & Sanitation", "Roads & Transport", "Electricity",
                "Public Safety", "Waste Management", "Street Lighting",
                "Parks & Recreation", "Health Services"
            };

            string[] descriptions = new string[]
            {
                "Burst water pipe causing flooding. Urgent repair needed.",
                "Large pothole creating traffic hazard. Multiple complaints.",
                "Street light out for over a week. Safety concern.",
                "Illegal dumping site needs immediate cleanup.",
                "Power outage affecting 50+ households.",
                "Playground equipment damaged and unsafe.",
                "Graffiti on public building requires removal.",
                "Stray animals in residential area. Control needed.",
                "Traffic light malfunction causing dangerous situation.",
                "Damaged park bench needs replacement.",
                "Sewer blockage causing bad odor.",
                "Overgrown trees blocking street signs.",
                "Abandoned vehicle on public property.",
                "Water quality complaint from residents.",
                "Road marking faded and needs repainting."
            };

            // Create 15 sample issues
            for (int i = 0; i < 15; i++)
            {
                string location = locations[rand.Next(locations.Length)];
                string category = categories[rand.Next(categories.Length)];
                string description = descriptions[rand.Next(descriptions.Length)];

                List<string> attachments = new List<string>();
                if (rand.Next(100) > 60)
                {
                    int attachmentCount = rand.Next(1, 4);
                    for (int j = 0; j < attachmentCount; j++)
                    {
                        attachments.Add($"photo_{i}_{j}.jpg");
                    }
                }

                IssueManager.AddIssue(location, category, description, attachments);

                // Add small delay to create varied timestamps
                System.Threading.Thread.Sleep(10);
            }
        }

        /// <summary>
        /// Initialize all advanced data structures
        /// </summary>
        private void InitializeDataStructures()
        {
            priorityHeap = new MinHeap<ServiceRequest>();
            bstById = new BinarySearchTree();
            avlTree = new AVLTree();
            redBlackTree = new RedBlackTree();
            requestGraph = new ServiceRequestGraph();
            allRequests = new List<ServiceRequest>();
        }

        /// <summary>
        /// Load service requests from IssueManager and populate data structures
        /// </summary>
        private void LoadServiceRequests()
        {
            var reportedIssues = IssueManager.GetAllIssues();

            foreach (var issue in reportedIssues)
            {
                ServiceRequest request = new ServiceRequest
                {
                    Id = issue.IssueId,
                    Location = issue.Location,
                    Category = issue.Category,
                    Description = issue.Description,
                    Status = DetermineStatus(issue),
                    ReportedDate = issue.ReportedDate,
                    Priority = CalculatePriority(issue),
                    AttachedFiles = issue.AttachedFiles
                };

                allRequests.Add(request);
                priorityHeap.Insert(request);
                bstById.Insert(request.Id, request);
                avlTree.Insert(request.Id, request);
                redBlackTree.Insert(request.Id, request);
                requestGraph.AddRequest(request);
            }

            BuildRequestRelationships();
        }

        /// <summary>
        /// Build edges in graph representing related service requests
        /// </summary>
        private void BuildRequestRelationships()
        {
            for (int i = 0; i < allRequests.Count; i++)
            {
                for (int j = i + 1; j < allRequests.Count; j++)
                {
                    ServiceRequest req1 = allRequests[i];
                    ServiceRequest req2 = allRequests[j];

                    if (req1.Category == req2.Category)
                    {
                        requestGraph.AddEdge(req1.Id, req2.Id, 1);
                    }

                    if (req1.Location.Equals(req2.Location, StringComparison.OrdinalIgnoreCase))
                    {
                        requestGraph.AddEdge(req1.Id, req2.Id, 2);
                    }
                }
            }
        }

        private string DetermineStatus(ReportedIssue issue)
        {
            if (!string.IsNullOrEmpty(issue.Status) && issue.Status != "Submitted")
                return issue.Status;

            int daysOld = (DateTime.Now - issue.ReportedDate).Days;

            if (daysOld == 0)
                return "Submitted";
            else if (daysOld <= 2)
                return "In Review";
            else if (daysOld <= 5)
                return "In Progress";
            else if (daysOld <= 10)
                return "Pending";
            else
                return "Delayed";
        }

        private int CalculatePriority(ReportedIssue issue)
        {
            int priority = 50;
            string categoryLower = issue.Category.ToLower();

            if (categoryLower.Contains("water") || categoryLower.Contains("electricity"))
                priority -= 20;
            else if (categoryLower.Contains("road") || categoryLower.Contains("safety"))
                priority -= 15;
            else if (categoryLower.Contains("sanitation") || categoryLower.Contains("health"))
                priority -= 10;

            int daysOld = (DateTime.Now - issue.ReportedDate).Days;
            priority -= daysOld * 2;

            return Math.Max(1, priority);
        }

        private void DisplayStatistics()
        {
            int total = allRequests.Count;
            int submitted = allRequests.Count(r => r.Status == "Submitted");
            int inReview = allRequests.Count(r => r.Status == "In Review");
            int inProgress = allRequests.Count(r => r.Status == "In Progress");
            int pending = allRequests.Count(r => r.Status == "Pending");
            int delayed = allRequests.Count(r => r.Status == "Delayed");

            lblStats.Text = $"Total: {total} | Submitted: {submitted} | In Review: {inReview} | " +
                           $"In Progress: {inProgress} | Pending: {pending} | Delayed: {delayed}";
        }

        private void RefreshListView()
        {
            lvIssues.Items.Clear();

            if (allRequests.Count == 0)
            {
                ListViewItem emptyItem = new ListViewItem("No requests");
                emptyItem.SubItems.Add("N/A");
                emptyItem.SubItems.Add("N/A");
                emptyItem.SubItems.Add("N/A");
                emptyItem.SubItems.Add("N/A");
                emptyItem.SubItems.Add("N/A");
                emptyItem.ForeColor = Color.Gray;
                lvIssues.Items.Add(emptyItem);
                return;
            }

            List<ServiceRequest> sortedRequests = new List<ServiceRequest>();

            switch (cmbSortOrder.SelectedIndex)
            {
                case 0: // BST (Standard)
                    sortedRequests = bstById.InOrderTraversal();
                    break;
                case 1: // AVL Tree (Balanced)
                    sortedRequests = avlTree.InOrderTraversal();
                    break;
                case 2: // Red-Black Tree (Balanced)
                    sortedRequests = redBlackTree.InOrderTraversal();
                    break;
                case 3: // Priority (Heap Order)
                    sortedRequests = priorityHeap.GetAllSorted();
                    break;
                case 4: // Date (Newest First)
                    sortedRequests = allRequests.OrderByDescending(r => r.ReportedDate).ToList();
                    break;
                case 5: // Status (Alphabetical)
                    sortedRequests = allRequests.OrderBy(r => r.Status).ToList();
                    break;
                default:
                    sortedRequests = allRequests;
                    break;
            }

            foreach (var request in sortedRequests)
            {
                ListViewItem item = new ListViewItem(request.Id.ToString());
                item.SubItems.Add(request.Location);
                item.SubItems.Add(request.Category);
                item.SubItems.Add(request.Status);
                item.SubItems.Add(request.ReportedDate.ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(request.Priority.ToString());
                item.Tag = request;

                switch (request.Status)
                {
                    case "Delayed":
                        item.BackColor = Color.FromArgb(255, 230, 230);
                        item.ForeColor = Color.DarkRed;
                        break;
                    case "Pending":
                        item.BackColor = Color.FromArgb(255, 250, 205);
                        break;
                    case "In Progress":
                        item.BackColor = Color.FromArgb(230, 255, 230);
                        break;
                    case "In Review":
                        item.BackColor = Color.FromArgb(230, 240, 255);
                        break;
                    case "Submitted":
                        item.BackColor = Color.FromArgb(245, 245, 245);
                        break;
                }

                lvIssues.Items.Add(item);
            }
        }

        private void UpdateHeapTopDisplay()
        {
            if (priorityHeap.Count > 0)
            {
                ServiceRequest topPriority = priorityHeap.Peek();
                lblHeapTop.Text = $"🔥 Top Priority: ID {topPriority.Id} - {topPriority.Category} " +
                                 $"at {topPriority.Location} (Priority: {topPriority.Priority})";
            }
            else
            {
                lblHeapTop.Text = "🔥 Top Priority: No service requests in system";
                lblHeapTop.ForeColor = Color.Gray;
            }
        }

        private void CmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchId.Text, out int searchId))
            {
                ServiceRequest found = bstById.Search(searchId);

                if (found != null)
                {
                    lvIssues.SelectedItems.Clear();
                    foreach (ListViewItem item in lvIssues.Items)
                    {
                        if (((ServiceRequest)item.Tag).Id == searchId)
                        {
                            item.Selected = true;
                            item.EnsureVisible();
                            item.BackColor = Color.LightBlue;
                            break;
                        }
                    }

                    var relatedIds = requestGraph.GetRelatedRequests(searchId);
                    string relatedInfo = relatedIds.Count > 0
                        ? $"\n\n📊 Related Requests (same category/location):\n   IDs: {string.Join(", ", relatedIds)}"
                        : "\n\n📊 No related requests found";

                    string attachmentInfo = found.AttachedFiles != null && found.AttachedFiles.Count > 0
                        ? $"\n📎 Attachments: {found.AttachedFiles.Count} file(s)"
                        : "\n📎 No attachments";

                    MessageBox.Show(
                        $"✅ Service Request Found!\n\n" +
                        $"ID: {found.Id}\n" +
                        $"Location: {found.Location}\n" +
                        $"Category: {found.Category}\n" +
                        $"Status: {found.Status}\n" +
                        $"Priority: {found.Priority} (lower = higher priority)\n" +
                        $"Reported: {found.ReportedDate:yyyy-MM-dd HH:mm}\n" +
                        $"Description: {found.Description.Substring(0, Math.Min(100, found.Description.Length))}..." +
                        attachmentInfo +
                        relatedInfo,
                        "Request Details - BST Search Result",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        $"❌ Service Request ID {searchId} not found in the system.\n\n" +
                        $"Total requests in system: {allRequests.Count}",
                        "Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(
                    "⚠️ Please enter a valid numeric ID.\n\n" +
                    "Example: Enter '1' or '42' to search for that request ID.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtnShowNetwork_Click(object sender, EventArgs e)
        {
            if (lvIssues.SelectedItems.Count == 0)
            {
                MessageBox.Show(
                    "⚠️ Please select a service request from the list first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ServiceRequest selected = (ServiceRequest)lvIssues.SelectedItems[0].Tag;
            var stats = requestGraph.GetStatistics(selected.Id);

            string bfsOrder = string.Join(" → ", stats.BFSTraversal.Select(id => $"ID {id}"));
            string dfsOrder = string.Join(" → ", stats.DFSTraversal.Select(id => $"ID {id}"));

            MessageBox.Show(
                $"📊 GRAPH TRAVERSAL ANALYSIS\n" +
                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                $"Selected Request: ID {selected.Id}\n" +
                $"Total Network Nodes: {stats.TotalNodes}\n" +
                $"Total Connections: {stats.TotalEdges}\n\n" +
                $"🔄 BFS Traversal ({stats.BFSReachableNodes} nodes):\n" +
                $"{bfsOrder}\n\n" +
                $"🌳 DFS Traversal ({stats.DFSReachableNodes} nodes):\n" +
                $"{dfsOrder}\n\n" +
                $"💡 BFS explores level-by-level (breadth-first)\n" +
                $"💡 DFS explores deep paths first (depth-first)",
                "Related Request Network - Graph Traversal",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnOptimalOrder_Click(object sender, EventArgs e)
        {
            if (allRequests.Count == 0)
            {
                MessageBox.Show(
                    "⚠️ No service requests to process.",
                    "No Data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var mst = requestGraph.GetMinimumSpanningTree();
            var processingGroups = requestGraph.GetOptimalProcessingOrder();

            string mstInfo = "🌳 MINIMUM SPANNING TREE (Key Connections):\n";
            mstInfo += "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";

            if (mst.Count > 0)
            {
                foreach (var edge in mst.Take(10))
                {
                    mstInfo += $"ID {edge.FromId} ↔ ID {edge.ToId} " +
                              $"(Weight: {edge.Weight}, " +
                              $"{(edge.Weight == 2 ? "Same Location" : "Same Category")})\n";
                }
                if (mst.Count > 10)
                    mstInfo += $"... and {mst.Count - 10} more connections\n";
            }
            else
            {
                mstInfo += "No connections found (all requests are isolated)\n";
            }

            mstInfo += $"\n📋 OPTIMAL PROCESSING GROUPS ({processingGroups.Count} groups):\n";
            mstInfo += "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";

            for (int i = 0; i < Math.Min(5, processingGroups.Count); i++)
            {
                var group = processingGroups[i];
                mstInfo += $"Group {i + 1} ({group.Count} requests): ";
                mstInfo += string.Join(", ", group.Select(id => $"ID {id}")) + "\n";
            }

            if (processingGroups.Count > 5)
                mstInfo += $"... and {processingGroups.Count - 5} more groups\n";

            mstInfo += "\n💡 MST groups related requests for efficient batch processing\n";
            mstInfo += "💡 Process requests in same group together to save resources";

            MessageBox.Show(
                mstInfo,
                "Optimal Processing Order - Minimum Spanning Tree",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnComparePerformance_Click(object sender, EventArgs e)
        {
            if (allRequests.Count == 0)
            {
                MessageBox.Show(
                    "⚠️ No service requests to analyze.",
                    "No Data",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string comparison = "🔬 TREE STRUCTURE PERFORMANCE COMPARISON\n";
            comparison += "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n";

            comparison += "📊 Binary Search Tree (BST):\n";
            comparison += $"   • Nodes: {bstById.InOrderTraversal().Count}\n";
            comparison += $"   • Type: Unbalanced\n";
            comparison += $"   • Search Time: O(n) worst case\n";
            comparison += $"   • Balance: Not guaranteed\n\n";

            comparison += "📊 AVL Tree:\n";
            comparison += $"   • Nodes: {avlTree.GetNodeCount()}\n";
            comparison += $"   • Height: {avlTree.GetTreeHeight()}\n";
            comparison += $"   • Type: Strictly Balanced\n";
            comparison += $"   • Search Time: O(log n) guaranteed\n";
            comparison += $"   • Balance Factor: Max ±1\n";
            comparison += $"   • Balanced: {(avlTree.IsBalanced() ? "✅ Yes" : "❌ No")}\n\n";

            comparison += "📊 Red-Black Tree:\n";
            comparison += $"   • Nodes: {redBlackTree.GetNodeCount()}\n";
            comparison += $"   • Black Height: {redBlackTree.GetBlackHeight()}\n";
            comparison += $"   • Type: Approximately Balanced\n";
            comparison += $"   • Search Time: O(log n) guaranteed\n";
            comparison += $"   • Properties: {(redBlackTree.VerifyProperties() ? "✅ Valid" : "❌ Invalid")}\n\n";

            comparison += "💡 RECOMMENDATIONS:\n";
            comparison += "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            comparison += "• AVL Tree: Best for search-heavy operations\n";
            comparison += "• Red-Black Tree: Best for frequent insertions\n";
            comparison += "• BST: Simplest but can degrade to O(n)\n\n";
            comparison += $"Current dataset size: {allRequests.Count} requests";

            MessageBox.Show(
                comparison,
                "Tree Performance Analysis",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            InitializeDataStructures();
            LoadServiceRequests();
            DisplayStatistics();
            RefreshListView();
            UpdateHeapTopDisplay();

            MessageBox.Show(
                $"✅ Service requests refreshed!\n\n" +
                $"Total requests: {allRequests.Count}\n" +
                $"Data structures updated:\n" +
                $"  • Binary Search Tree (BST)\n" +
                $"  • AVL Tree (Balanced)\n" +
                $"  • Red-Black Tree (Balanced)\n" +
                $"  • Min Heap (Priority Queue)\n" +
                $"  • Graph (Relationships + MST)",
                "Refresh Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnLoadSample_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "This will clear all existing issues and load 15 sample service requests.\n\n" +
                "Do you want to continue?",
                "Load Sample Data",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                IssueManager.ClearAllIssues();
                LoadSampleDataForTesting();

                InitializeDataStructures();
                LoadServiceRequests();
                DisplayStatistics();
                RefreshListView();
                UpdateHeapTopDisplay();

                var stats = IssueManager.GetCategoryStatistics();
                string summary = $"✅ Sample Data Loaded!\n\n";
                summary += $"Total Issues: {allRequests.Count}\n\n";
                summary += "Category Breakdown:\n";

                foreach (var stat in stats)
                {
                    summary += $"  • {stat.Key}: {stat.Value}\n";
                }

                summary += "\n🎯 Ready to test:\n";
                summary += "  • Try different sort options\n";
                summary += "  • Click 'Show Network' for BFS/DFS\n";
                summary += "  • Click 'Optimal Order' for MST\n";
                summary += "  • Click 'Compare Trees' for analysis";

                MessageBox.Show(summary, "Sample Data Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LvIssues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvIssues.SelectedItems.Count > 0)
            {
                ServiceRequest selected = (ServiceRequest)lvIssues.SelectedItems[0].Tag;
                var relatedIds = requestGraph.GetRelatedRequests(selected.Id);

                string relatedInfo = relatedIds.Count > 0
                    ? $" | 🔗 Related: {relatedIds.Count} request(s)"
                    : "";

                lblHeapTop.Text = $"🔍 Selected: ID {selected.Id} - {selected.Status} " +
                                 $"(Priority: {selected.Priority}){relatedInfo}";
                lblHeapTop.ForeColor = Color.FromArgb(0, 123, 255);
            }
            else
            {
                UpdateHeapTopDisplay();
            }
        }
    }

    // ==================== SERVICE REQUEST CLASS ====================
    /// <summary>
    /// Wrapper class for service request with priority support
    /// </summary>
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime ReportedDate { get; set; }
        public int Priority { get; set; }
        public List<string> AttachedFiles { get; set; }

        public int CompareTo(ServiceRequest other)
        {
            return this.Priority.CompareTo(other.Priority);
        }
    }

    // ==================== MIN HEAP IMPLEMENTATION ====================
    /// <summary>
    /// Min Heap for priority queue (lowest value = highest priority)
    /// </summary>
    public class MinHeap<T> where T : IComparable<T>
    {
        private List<T> heap;

        public int Count => heap.Count;

        public MinHeap()
        {
            heap = new List<T>();
        }

        public void Insert(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }

        public T Peek()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");
            return heap[0];
        }

        public List<T> GetAllSorted()
        {
            return new List<T>(heap.OrderBy(x => x));
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].CompareTo(heap[parentIndex]) >= 0)
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }

    // ==================== BINARY SEARCH TREE ====================
    /// <summary>
    /// Binary Search Tree for efficient O(log n) searching by ID
    /// </summary>
    public class BinarySearchTree
    {
        private class Node
        {
            public int Id { get; set; }
            public ServiceRequest Request { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(int id, ServiceRequest request)
            {
                Id = id;
                Request = request;
            }
        }

        private Node root;

        public void Insert(int id, ServiceRequest request)
        {
            root = InsertRec(root, id, request);
        }

        private Node InsertRec(Node node, int id, ServiceRequest request)
        {
            if (node == null)
                return new Node(id, request);

            if (id < node.Id)
                node.Left = InsertRec(node.Left, id, request);
            else if (id > node.Id)
                node.Right = InsertRec(node.Right, id, request);

            return node;
        }

        public ServiceRequest Search(int id)
        {
            return SearchRec(root, id);
        }

        private ServiceRequest SearchRec(Node node, int id)
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

        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(Node node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Request);
                InOrderRec(node.Right, result);
            }
        }
    }
}