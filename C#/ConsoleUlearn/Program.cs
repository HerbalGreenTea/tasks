using System;

namespace ConsoleTest
{
    class QueueItem
    {
        public int Value { get; set; }
        public QueueItem Next { get; set; }
    }

    public class Queue
    {
        QueueItem head;
        QueueItem tail;

        public void Enqueue(int value)
        {
            QueueItem item = new QueueItem { Value = value };

            if (head == null)
            {
                head = tail = item;
            }
            else
            {
                tail.Next = item;
                tail = item;
            }
        }

        public int Dequeue()
        {
            if (head == null)
                throw new IndexOutOfRangeException();
            var result = head.Value;
            head = head.Next;
            if (head == null) tail = null;
            return result;
        }
    }

    class Program
    {
        public static void Main()
        {
            Queue queue = new Queue();

            for (int i = 0; i < 3; i++)
                queue.Enqueue(i);
            for (int i = 0; i < 3; i++)
                Console.WriteLine(queue.Dequeue());
        }
    }
}

