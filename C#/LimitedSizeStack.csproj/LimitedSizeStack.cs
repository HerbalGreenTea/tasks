using System;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private int limit;
        private int count;

        class Item
        {
            public T Value { get; set; }
            public Item Next { get; set; }
            public Item Previous { get; set; }
        }

        Item head;
        Item tail;

        public LimitedSizeStack(int limit)
        {
            this.limit = limit;
        }

        public void Push(T item)
        {
            if (Count < limit)
            {
                Create(item);
            }
            else
            {
                Delete();
                Create(item);
            }
        }

        public T Pop()
        {
            if (tail == null)
                throw new IndexOutOfRangeException();

            var result = tail.Value;
            tail = tail.Previous;

            if (tail == null) 
                head = null;

            count--;
            return result;
        }

        void Create(T item)
        {
            var obj = new Item { Value = item };

            if (head == null)
            {
                head = tail = obj;
            }
            else
            {
                obj.Previous = tail;
                tail.Next = obj;
                tail = obj;
            }
            count++;
        }

        void Delete()
        {
            if (head == null)
                throw new IndexOutOfRangeException();

            head = head.Next;
            head.Previous = null;

            if (head == null) 
                tail = null;

            count--;
        }

        public int Count
        {
            get { return count; }
        }
    }
}
