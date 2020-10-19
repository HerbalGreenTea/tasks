using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public LimitedSizeStack<TItem> lastComands { get; }
        Dictionary<TItem, Command> allComands = new Dictionary<TItem, Command>();
        Dictionary<TItem, int> allIndex = new Dictionary<TItem, int>();
        int limit;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            lastComands = new LimitedSizeStack<TItem>(limit);
            this.limit = limit;
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            lastComands.Push(item);
            if (!allComands.ContainsKey(item))
            {
                allComands.Add(item, Command.Add);
                allIndex.Add(item, Items.Count - 1);
            }
        }

        public void RemoveItem(int index)
        {
            lastComands.Push(Items[index]);
            allComands[Items[index]] = Command.Remove;
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return lastComands.Count != 0;
        }

        public void Undo()
        {
            if (CanUndo())
            {
                var command = lastComands.Pop();
                switch(allComands[command])
                {
                    case Command.Add:
                        Items.Remove(command);
                        break;
                    case Command.Remove:
                        Items.Insert(allIndex[command], command);
                        break;
                }
            }
        }

        enum Command
        {
            Remove,
            Add
        }
    }
}
