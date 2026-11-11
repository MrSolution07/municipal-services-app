using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// Max-heap of service requests. The most urgent request sits at the top:
    /// higher priority, then older submission, then reference.
    /// The store rebuilds the heap. Callers peek. They do not dequeue.
    /// </summary>
    public class MaxHeap
    {
        private readonly List<ServiceRequest> heap = new List<ServiceRequest>();

        public int Count
        {
            get { return heap.Count; }
        }

        public void Clear()
        {
            heap.Clear();
        }

        public void Insert(ServiceRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            heap.Add(request);
            Rise(heap.Count - 1);
        }

        public ServiceRequest Peek()
        {
            return heap.Count == 0 ? null : heap[0];
        }

        private void Rise(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (!MoreUrgent(heap[index], heap[parent]))
                {
                    break;
                }

                Swap(index, parent);
                index = parent;
            }
        }

        private static bool MoreUrgent(ServiceRequest left, ServiceRequest right)
        {
            if (left.Priority != right.Priority)
            {
                return left.Priority > right.Priority;
            }

            int submitted = left.SubmittedAt.CompareTo(right.SubmittedAt);
            if (submitted != 0)
            {
                return submitted < 0;
            }

            return string.Compare(left.Reference, right.Reference, StringComparison.Ordinal) < 0;
        }

        private void Swap(int i, int j)
        {
            ServiceRequest temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
