using System;
using System.Collections.Generic;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Data
{
    /// <summary>
    /// A small binary-heap priority queue for LocalEvent items. .NET Framework 4.8 does not
    /// include System.Collections.Generic.PriorityQueue, so this class provides the same
    /// behaviour using a List and heap up/down operations.
    /// </summary>
    public class SimplePriorityQueue
    {
        private readonly List<LocalEvent> heap = new List<LocalEvent>();

        public int Count
        {
            get { return heap.Count; }
        }

        public void Enqueue(LocalEvent item)
        {
            heap.Add(item);
            HeapUp(heap.Count - 1);
        }

        public LocalEvent Dequeue()
        {
            if (heap.Count == 0)
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }

            LocalEvent top = heap[0];
            LocalEvent last = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
            {
                heap[0] = last;
                HeapDown(0);
            }

            return top;
        }

        public LocalEvent Peek()
        {
            if (heap.Count == 0)
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }

            return heap[0];
        }

        public void Clear()
        {
            heap.Clear();
        }

        /// <summary>Returns all items still in the heap, highest priority first, without removing them.</summary>
        public IEnumerable<LocalEvent> Snapshot()
        {
            SimplePriorityQueue copy = new SimplePriorityQueue();
            foreach (LocalEvent item in heap)
            {
                copy.Enqueue(item);
            }

            while (copy.Count > 0)
            {
                yield return copy.Dequeue();
            }
        }

        private void HeapUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (HasHigherPriority(heap[index], heap[parent]))
                {
                    Swap(index, parent);
                    index = parent;
                }
                else
                {
                    break;
                }
            }
        }

        private void HeapDown(int index)
        {
            int count = heap.Count;
            while (index < count)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int best = index;

                if (left < count && HasHigherPriority(heap[left], heap[best]))
                {
                    best = left;
                }

                if (right < count && HasHigherPriority(heap[right], heap[best]))
                {
                    best = right;
                }

                if (best == index)
                {
                    break;
                }

                Swap(index, best);
                index = best;
            }
        }

        /// <summary>
        /// Higher priority wins. If priorities are equal, the earlier event wins so the queue is
        /// stable by date.
        /// </summary>
        private static bool HasHigherPriority(LocalEvent a, LocalEvent b)
        {
            if (a.Priority != b.Priority)
            {
                return a.Priority > b.Priority;
            }

            return a.EventDate < b.EventDate;
        }

        private void Swap(int i, int j)
        {
            LocalEvent temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
