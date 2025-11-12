using System;
using System.Collections.Generic;

namespace ST10109685Prog7312POE
{
    // Minimal binary min-heap for ReportedIssue prioritized by IssueId (lower id = higher priority)
    public class MinHeap
    {
        private List<ReportedIssue> heap = new List<ReportedIssue>();

        private int Parent(int i) => (i - 1) / 2;
        private int Left(int i) => 2 * i + 1;
        private int Right(int i) => 2 * i + 2;

        private void Swap(int i, int j)
        {
            var tmp = heap[i];
            heap[i] = heap[j];
            heap[j] = tmp;
        }

        public void Insert(ReportedIssue issue)
        {
            if (issue == null) return;
            heap.Add(issue);
            int i = heap.Count - 1;
            while (i != 0 && heap[Parent(i)].IssueId > heap[i].IssueId)
            {
                Swap(i, Parent(i));
                i = Parent(i);
            }
        }

        public ReportedIssue Peek()
        {
            if (heap.Count == 0) return null;
            return heap[0];
        }

        public ReportedIssue ExtractMin()
        {
            if (heap.Count == 0) return null;
            if (heap.Count == 1)
            {
                var only = heap[0];
                heap.Clear();
                return only;
            }
            var root = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            Heapify(0);
            return root;
        }

        private void Heapify(int i)
        {
            int l = Left(i);
            int r = Right(i);
            int smallest = i;
            if (l < heap.Count && heap[l].IssueId < heap[smallest].IssueId)
                smallest = l;
            if (r < heap.Count && heap[r].IssueId < heap[smallest].IssueId)
                smallest = r;
            if (smallest != i)
            {
                Swap(i, smallest);
                Heapify(smallest);
            }
        }

        public int Count => heap.Count;

        public List<ReportedIssue> ToList()
        {
            return new List<ReportedIssue>(heap);
        }
    }
}
