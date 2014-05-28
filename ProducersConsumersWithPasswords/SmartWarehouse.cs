using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProducersConsumersWithPasswords
{
    public class SmartWarehouse<T>
    {
        private object synchronizationObject = new object();
        private SemaphoreSlim synchronizationSemaphore;

        private Queue<T> queue = new Queue<T>();
        private int capacity;

        public int Capacity
        {
            get { return capacity; }
        }

        public int Count
        {
            get { return queue.Count; }
        }

        public SmartWarehouse(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity has to be greater than zero.");

            this.capacity = capacity;
            synchronizationSemaphore = new SemaphoreSlim(0, capacity);
        }

        public void Add(T element)
        {
            lock (synchronizationObject)
            {
                if (Count >= Capacity)
                    Monitor.Wait(synchronizationObject);

                queue.Enqueue(element);
            }

            synchronizationSemaphore.Release();
        }

        public bool TryAdd(T element, Int32 timeout)
        {
            lock (synchronizationObject)
            {
                while (Count >= Capacity)
                {
                    if (!Monitor.Wait(synchronizationObject, timeout))
                        return false;
                }
                queue.Enqueue(element);
            }

            synchronizationSemaphore.Release();
            return true;
        }

        public T Take()
        {
            synchronizationSemaphore.Wait();
            T element;
            lock (synchronizationObject)
            {
                element = queue.Dequeue();
                Monitor.Pulse(synchronizationObject);
            }

            return element;
        }

        public bool TryTake(out T element, Int32 timeout)
        {
            bool success = synchronizationSemaphore.Wait(timeout);
            if (success)
            {
                lock (synchronizationObject)
                {
                    element = queue.Dequeue();
                    Monitor.Pulse(synchronizationObject);
                }
            }
            else
                element = default(T);

            return success;
        }
    }
}
