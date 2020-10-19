using System;
using System.Collections.Generic;
using System.Text;

namespace Delegates.Observers
{
	public class StackOperationsLogger
	{
		private readonly Observer observer = new Observer();
		public void SubscribeOn<T>(ObservableStack<T> stack)
		{
			stack.Add(observer.HandleEvent);
		}

		public string GetLog() => observer.Log.ToString();
	}

	public class Observer
	{
		public StringBuilder Log = new StringBuilder();
		public Action<object> HandleEvent;

		public Observer()
        {
			HandleEvent += (data) => Log.Append(data);
        }
	}

	public class ObservableStack<T>
	{
		public event Action<StackEventData<T>> Call;

		public void Add(Action<object> observer)
		{
			Call += (data) => observer(data);
		}

        public void Remove(Action<object> observer)
		{
			Call -= (data) => observer(data);
		}

		List<T> data = new List<T>();

		public void Push(T obj)
		{
			data.Add(obj);
			Call?.Invoke(new StackEventData<T> { IsPushed = true, Value = obj });
		}

		public T Pop()
		{
			if (data.Count == 0)
				throw new InvalidOperationException();
			var result = data[data.Count - 1];
			Call?.Invoke(new StackEventData<T> { IsPushed = false, Value = result });
			return result;
		}
	}
}
